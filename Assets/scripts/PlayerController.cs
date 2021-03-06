﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour {
	
	//Associated objects on Player (always on)
	private GameObject rightArm;
	private GameObject leftArm;
	private GameObject body;
	private GameObject head;
	private GameObject rightFoot;
	private GameObject leftFoot;
	private GameObject right_hand_weapon;
	private GameObject left_hand_weapon;

	//Active unit Attributes
	public int playerLevel;
	private float currentExperiencePoints;
	private int currentHealthPoints;
	private int currentManaPoints;
	private int maxHealthPoints;
	private int maxManaPoints;
	private float maxExperiencePoints;
	private int armorPoints;

	//Joystick check
	private bool rightJoystick;
	private Vector2 directionRightJoystick;
	private float rotZRightJoystick;
	private bool leftJoystick;
	private Vector2 directionLeftJoystick;
	private float rotZLeftJoystick;

	//Horizontal movement speed
	public float speed;
	//Direction variable
	private int facingRight; // 1 = right, -1 = left
	private bool isFalling;
	//Jumping Variables
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	private bool isGrounded;
	//Jump vector
	public float jumpHeight;
	//Ducking Variables
	private bool isDucking;
	//Dashing Variables
	public float dashSpeed;
	public double secondsInDash;
	public double secondsBetweenDashes;
	private bool canDash;
	private float lastDashTime;
	private bool isDashing;
	//speed modifiers
	public float crouchRate;
	public float sprintRate;
	public float maxSpeed;
	//Attacking Variables
		//List components
	private List<char> attack = new List<char> ();
	private List<char> nextAttack = new List<char> ();

	private int finalAttack; // TODO make enum for all attacks, this is a placeholder

	public double secondsInCombo;
	private float lastAttackTime;
	private bool inCombo;
	private bool willAttackNextFrame;
	//Shield Variables
	public int shieldPower;
	//Longrange variables
	public GameObject shot;
	public int shotSpeed;
	public double secondsBetweenShots;
	private float lastShotTime;
	//Aiming and shielding are one and the same
	private bool isAimingAndShielding;
	private bool canShoot;
	//Aiming
	private Quaternion leftArmAim;

	//Particles
	public ParticleSystem triggerParticles; //Shooting particle

	//UI Elements
	public Slider healthSlider;
	public Slider manaSlider;
	public Slider expSlider;
	public Text levelText;
	public Text armorText;
	public Text healthText;
	public Text manaText;
	public Text experienceText;

	//Menu variables
	private bool isPaused;

	private Animator animator;
	private Rigidbody2D myRigidbody2D;
	private GameManager gameManager;
	private PS3Controller PS3;
	private InventoryController inventory;

	//Event Subscriptions
	void OnEnable(){

	}

	void OnDisable(){

	}

	// Use this for initialization
	void Start () {
		//Getting animator, RigidBody, Game Manager, ps3 controller
		animator = GetComponent<Animator> ();
		myRigidbody2D = GetComponent<Rigidbody2D> ();
		PS3 = GetComponent<PS3Controller>();
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		inventory = GetComponent<InventoryController>();


		//Starting at level 1, exp 0
		currentExperiencePoints = 0;
		maxExperiencePoints = 100;
		levelUp ();


		//Getting player components
		leftArm = GameObject.Find ("left_arm");
		rightArm = GameObject.Find ("right_arm");
		leftFoot = GameObject.Find ("left_leg");
		rightFoot = GameObject.Find ("right_leg");
		head = GameObject.Find ("head_");
		body = GameObject.Find ("body");
		right_hand_weapon = GameObject.Find ("right_weapon");
		left_hand_weapon = GameObject.Find ("left_weapon");

		//Rendering initially equiped weapons 


		//Spawning full health etc
		currentHealthPoints = maxHealthPoints;
		currentManaPoints = maxManaPoints;

		//Setting up HUD/Updating UI elements
		healthSlider.maxValue = maxHealthPoints;
		healthSlider.minValue = 0;
		healthSlider.value = currentHealthPoints;
		manaSlider.maxValue = maxManaPoints;
		manaSlider.minValue = 0;
		manaSlider.value = currentManaPoints;
		expSlider.maxValue = maxExperiencePoints;
		expSlider.minValue = 0;
		expSlider.value = currentExperiencePoints;

		healthText.text = "Health: " + currentHealthPoints + " / " + maxHealthPoints;
		healthSlider.value = currentHealthPoints;
		manaText.text = "Mana: " + currentManaPoints + " / " + maxManaPoints;
		manaSlider.value = currentManaPoints;
		experienceText.text = "EXP: " + currentExperiencePoints + " / " + maxExperiencePoints; 
		expSlider.value = currentExperiencePoints;
		levelText.text = "Level: " + playerLevel;
		armorText.text = "Armor: " + armorPoints;

	}

	//Pause
	private void togglePause(){
		isPaused = !isPaused;

		if (isPaused) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}

	private void joystickCheck(bool isRightStick, bool isLeftStick){
		if (isRightStick){
			//Calculating angle of joysticks
			directionRightJoystick = new Vector2 (PS3.rightAnologHorizontal, PS3.rightAnologVertical).normalized;
			rotZRightJoystick = Mathf.Atan2 (-PS3.rightAnologHorizontal, PS3.rightAnologVertical) * Mathf.Rad2Deg;
		} else {
			directionRightJoystick = new Vector2(facingRight, 0f);
			//headfirst rotation of player when rightjoystick isn't touched (for dashing)
			rotZRightJoystick = -90f;
		}
		if (isLeftStick){
			//Calculating angle of joysticks
			directionLeftJoystick = new Vector2 (PS3.leftAnologHorizontal, -PS3.leftAnologVertical).normalized;
			rotZLeftJoystick = Mathf.Atan2 (-PS3.leftAnologHorizontal, -PS3.leftAnologVertical) * Mathf.Rad2Deg;
		} else {
			directionLeftJoystick = new Vector2(facingRight, 0f);
			//headfirst rotation of player when rightjoystick isn't touched (for dashing)
			rotZLeftJoystick = facingRight*-90f;
		}
	}
	
	private void jump(){
		myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, jumpHeight);
		Instantiate(triggerParticles, groundCheck.position, Quaternion.identity);
	}

	private void movePlayerHorizontal(float translationX){
		if (isDucking && isGrounded) {
			myRigidbody2D.velocity = new Vector2 (translationX / crouchRate, myRigidbody2D.velocity.y);
		} else if (PS3.rightTrigger && isGrounded) {
			myRigidbody2D.velocity = new Vector2 (translationX * sprintRate, myRigidbody2D.velocity.y);
		} else {
			myRigidbody2D.velocity = new Vector2 (translationX, myRigidbody2D.velocity.y);
		}
	}

	private void dashPlayer(){
		//Rotating character
		transform.rotation = Quaternion.Euler (0f, 0f, rotZLeftJoystick);
		myRigidbody2D.velocity = directionLeftJoystick * dashSpeed;
		Instantiate(triggerParticles, transform.position, Quaternion.identity);
	}

	private void rotatePlayerLeftArm(){
		leftArmAim = Quaternion.Euler (0f, 0f, (rotZRightJoystick + 90));
	}
	
	// Update is called once per frame
	void Update () {

		if (PS3.start) {
			togglePause();
		}

		//Checking joystick inputs
		rightJoystick = (Mathf.Abs (PS3.rightAnologHorizontal) != 0) || (Mathf.Abs (PS3.rightAnologVertical) != 0);
		leftJoystick = (Mathf.Abs (PS3.leftAnologHorizontal) != 0) || (Mathf.Abs (PS3.leftAnologVertical) != 0);
		joystickCheck(rightJoystick, leftJoystick);

		//Checking is player is grounded
		isGrounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);
		//Checking is player is falling
		isFalling = (myRigidbody2D.velocity.y < -0.1);

		// Ducking mechanism
		if (PS3.leftAnologClick && isGrounded) {
			isDucking = true;
		} else {
			isDucking = false;
		}

		//Dash
		//if the time since the last dash is less then the seconds in the dash (duration of the dash), then we know the player is dashing 
		if (((Time.time - lastDashTime) < secondsInDash) && (lastDashTime !=0)) {
			isDashing = true;
		} else {
			isDashing = false;
		}
		//if the time since the last dash is greater then the wait period and player is currently not dashing
		if (((Time.time - lastDashTime) > secondsBetweenDashes) && !isDashing) {
			canDash = true;
		} else {
			canDash = false;
		}
		
		//Shooting
		//if the time since the last shot is greater then the seconds between shots, the player can shoot
		if (((Time.time - lastShotTime) > secondsBetweenShots)) {
			canShoot = true;
		} else {
			canShoot = false;
		}

		//Attacking Mecahnism
		if (!isAimingAndShielding && !isDashing) {//If the player isn't in the middle of another action, the player can try to attack
			//if the combo hasn't started, start combo with initial settings cleared when any of the attack buttons are pressed
			if (PS3.circle || PS3.triangle || PS3.square){
				if (!inCombo) {
					lastAttackTime = Time.time;
				}
				if (PS3.circle){
					attack.Add('C');
				}
				if (PS3.square){
					attack.Add('S');
				}
				if (PS3.triangle){
					attack.Add('T');
				}

				string combo_list = "";
				for (int i = 0; i < attack.Count; i++){
					combo_list = combo_list + attack[i];
				}

				gameManager.instantiateText (combo_list, new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z), Pickup.Experience);
			}
		}

		//Close quarters combo
		//if the time since the last attack is less then the combo period and the player
		if (((Time.time - lastAttackTime) < secondsInCombo)) {
			inCombo = true;
		} else {//When !incombo, and attack > 0, we know the player attacked
			inCombo = false;
			// The player attack will trigger when the player is not setting up a combo (duration expires) and the attack list is not empty
			if (attack.Count > 0){
				willAttackNextFrame = true;
			} else {
				willAttackNextFrame = false;
			}
		}

		//if the combo time period is over, the players next attack will be set to the final attack value before getting reset.
		if (willAttackNextFrame) {

			for (int i = 0; i < attack.Count; i++){
				nextAttack.Add(attack[i]);
			}

			Instantiate(triggerParticles, rightArm.transform.position, Quaternion.identity);
			attack.Clear();
			willAttackNextFrame = false;
		} else {
			nextAttack.Clear();

		}

		// Horizontal control of player
		// Ducking/crouching slows player down
		// holding R2/sprinting speeds player up
		float translationX = 0f;
		if (!isDashing) {
			translationX = PS3.leftAnologHorizontal * speed;
			movePlayerHorizontal(translationX);
		}
		
		//Dashing Mechanism
		if (PS3.leftTrigger) {
			if (!isDashing && canDash) {
				lastDashTime = Time.time;
			}
		}
		if (isDashing){
			dashPlayer();
		} else {
			transform.rotation = Quaternion.Euler (0f, 0f, 0f);
		}

		//Jumping Mechanism
		if (PS3.x && isGrounded) {
			jump();
		}

		//Aiming and Shielding mechanism
		if (PS3.leftBumper) {
			isAimingAndShielding = true;
		} else {
			isAimingAndShielding = false;
		}

		
		//Aiming magic and gun within a reasonable angle
		if (isAimingAndShielding) {
			//TODO CAPPING LEFT AND RIGHT AIMING TO NOT BE ABLE TO AIM AND SHOOT BACKWARDS
			//Rotating left arm
			rotatePlayerLeftArm();
		}

		if (PS3.rightBumper && isAimingAndShielding && canShoot){
			GameObject shotTemp = Instantiate(shot, transform.position, leftArmAim) as GameObject;
			shotTemp.GetComponent<Rigidbody2D>().velocity = directionRightJoystick*shotSpeed;

			lastShotTime = Time.time;
		
			//Particles
			Instantiate(triggerParticles, new Vector2 (leftArm.transform.position.x+1*facingRight,leftArm.transform.position.y) , Quaternion.identity);
		}

		//TEMP equip item
		if (PS3.dpadDown){
			//from current right handed weapon from linked list in dictionary we are getting the next weapon in the list
			LinkedListNode<EquipableInventoryItem> temp_lln = inventory.equipableInventoryItems[Equipment.right_hand_weapon].First;
			do{
				string temp = temp_lln.Value.name;
				gameManager.instantiateText (temp, new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z), Pickup.Experience);
				temp_lln = temp_lln.Next;
			}while(temp_lln != null);
//			gameManager.instantiateText (temp, new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z), Pickup.Experience);
		}

		//Capping player speed
		if (myRigidbody2D.velocity.magnitude > maxSpeed){
			myRigidbody2D.velocity = myRigidbody2D.velocity.normalized*maxSpeed;
		}

		string tempNextAttack = "";
		//Checking which attack combination was entered
		//TODO MAKE ATTACK STRINGS HERE AND DOCUMENT (ASSOCIATE WITH DECIDED ATTACKS OBV
		for (int i = 0; i < nextAttack.Count; i++){
			tempNextAttack = tempNextAttack + nextAttack[i];
		}
		if (tempNextAttack == "S") {
			finalAttack = 1;
		} else if (tempNextAttack == "T") {
			finalAttack = 2;
		} else {
			finalAttack = 0;
		}

		// Animator controller
		// Setting speed param for animator
		animator.SetFloat ("speed", Mathf.Abs (translationX));
		//Setting grounded param for animator
		animator.SetBool ("isGrounded", isGrounded);
		//ducking param
		animator.SetBool ("isDucking", isDucking);
		//Attacking param
		animator.SetInteger ("attack_num", finalAttack);
		//Dashing param
		animator.SetBool ("isDashing", isDashing);
		//Shielding param
//		animator.SetBool ("isShielding", isAimingAndShielding);
		//Falling param
		animator.SetBool ("isFalling", isFalling);

	}

	void LateUpdate(){

		//rotating left arm to shoot and aim projectiles
		if (isAimingAndShielding){
			// Rotating Left arm towards direction desired
			leftArm.transform.rotation = Quaternion.Euler (0f, 0f, (Mathf.Atan2( directionRightJoystick.y,facingRight*directionRightJoystick.x )*Mathf.Rad2Deg));
		}

		//Controlling which way player is facing -> When Not Aiming!! Otherwise we want the player to face the same direction
		if (!isAimingAndShielding) {
			if (myRigidbody2D.velocity.x < 0) {
				facingRight = -1;
			} else if (myRigidbody2D.velocity.x > 0) {
				facingRight = 1;
			}
			if (facingRight == 1) {
				myRigidbody2D.transform.localScale = new Vector3 (1, 1, 1);
			} else {
				myRigidbody2D.transform.localScale = new Vector3 (-1, 1, 1);
			}
		}

	}

	void OnTriggerEnter2D(Collider2D other){

		//EnemyCollision
		if (other.gameObject.tag.Equals("Enemy")){
			int damage = other.GetComponent<EnemyController>().damagePoints;
			damagePlayer(damage, other.transform);
			Destroy(other.gameObject);
		}

		//Pickups
		if (other.gameObject.tag.Equals("Pickup")) {
			int healthPoints = other.gameObject.GetComponent<PickupController>().healthPoints;
			int manaPoints = other.gameObject.GetComponent<PickupController>().manaPoints;
			int expPoints = other.gameObject.GetComponent<PickupController>().expPoints;
			pickupEffect(healthPoints, manaPoints, expPoints, other.transform);
			Destroy(other.gameObject);
		}

		//InventoryItem
		if (other.gameObject.tag.Equals("InventoryItem")){
			inventory.collectEquipableItem(other.gameObject.GetComponent<EquipableInventoryItem>());
			Destroy(other.gameObject);
		}
	}

	private void damagePlayer(int damagePoints, Transform other){
		int damage = damagePoints - Mathf.FloorToInt(0.1f*armorPoints);
		if (damage <= 0)
			damage = 0;
		gameManager.instantiateText (damage.ToString(), new Vector3 (other.transform.position.x, other.transform.position.y, other.transform.position.z), Pickup.Damage);
		currentHealthPoints -= damage;
		if (currentHealthPoints <= 0){}//Kill Player Here

		//UPDATE UI
		healthText.text = "Health: " + currentHealthPoints + " / " + maxHealthPoints;
		healthSlider.value = currentHealthPoints;

	}
	private void pickupEffect(int healthPoints, int manaPoints, int experiencePoints, Transform other){
		//Capping at max health stat
		if (currentHealthPoints<maxHealthPoints){
			if (healthPoints!=0) gameManager.instantiateText (healthPoints.ToString(), new Vector3 (other.transform.position.x, other.transform.position.y, other.transform.position.z), Pickup.Health);
			if ((currentHealthPoints+healthPoints)>maxHealthPoints){
				currentHealthPoints = maxHealthPoints;
			} else {
				currentHealthPoints += healthPoints;
			}
		}
		//Capping at max mana stat
		if (currentManaPoints<maxManaPoints){
			if (manaPoints!=0) gameManager.instantiateText (manaPoints.ToString(), new Vector3 (other.transform.position.x, other.transform.position.y, other.transform.position.z), Pickup.Mana);
			if ((currentManaPoints+manaPoints)>maxManaPoints){	
				currentManaPoints = maxManaPoints;
			} else {
				currentManaPoints += manaPoints;
			}
		}
		//Exp
		currentExperiencePoints += experiencePoints;
		if (experiencePoints!=0) gameManager.instantiateText (experiencePoints.ToString(), new Vector3 (other.transform.position.x, other.transform.position.y, other.transform.position.z), Pickup.Experience);
		
		if ((currentExperiencePoints) >= maxExperiencePoints)
			levelUp ();


		//UPDATE UI
		healthText.text = "Health: " + currentHealthPoints + " / " + maxHealthPoints;
		healthSlider.value = currentHealthPoints;
		manaText.text = "Mana: " + currentManaPoints + " / " + maxManaPoints;
		manaSlider.value = currentManaPoints;
		experienceText.text = "EXP: " + currentExperiencePoints + " / " + maxExperiencePoints; 
		expSlider.value = currentExperiencePoints;

	}
	private void levelUp(){
		playerLevel += 1;
		maxExperiencePoints = (int)System.Math.Ceiling(maxExperiencePoints*1.1);

		if ((currentExperiencePoints) >= maxExperiencePoints)
			levelUp ();

		//Stat cap changes
		//Leveled stats for player
		maxHealthPoints = playerLevel * 10;
		maxManaPoints = playerLevel * 5;
		healthSlider.maxValue = maxHealthPoints;
		manaSlider.maxValue = maxManaPoints;

		//UPDATE UI
		levelText.text = "Level: " + playerLevel;
	}

	private void equip(EquipableInventoryItem item){
		if (item.equipment_type == Equipment.right_hand_weapon){
			right_hand_weapon.GetComponent<SpriteRenderer>().sprite = item.right_sprite;
		}
	}

}
