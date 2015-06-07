using UnityEngine;
using System.Collections;

public class PS3Controller : MonoBehaviour {


	// Checking button input
	public float rightAnologHorizontal;
	public float rightAnologVertical;
	public float leftAnologHorizontal;
	public float leftAnologVertical;
	public bool rightAnologClick;
	public bool leftAnologClick;
	public bool x;
	public bool square;
	public bool triangle;
	public bool circle;
	public bool rightBumper;
	public bool leftBumper;
	public bool rightTrigger;
	public bool leftTrigger;
	public bool dpadUp;
	public bool dpadRight;
	public bool dpadLeft;
	public bool dpadDown;
	public bool start;
	public bool select;
	public bool playstation;

	//Joystick configuration for PS3
	// Based on http://www.wobbleboxx.com/Development/Gamedev?p=359
	private string selectButton = "joystick button 0";
	private string leftAnologButton = "joystick button 1";
	private string rightAnologButton = "joystick button 2";
	private string startButton = "joystick button 3";
	private string dpadUpButton = "joystick button 4";
	private string dpadRightButton = "joystick button 5";
	private string dpadDownButton = "joystick button 6";
	private string dpadLeftButton = "joystick button 7";
	private string leftTriggerButton = "joystick button 8";
	private string rightTriggerButton = "joystick button 9";
	private string leftBumperButton = "joystick button 10";
	private string rightBumperButton = "joystick button 11";
	private string triangleButton = "joystick button 12";
	private string circleButton = "joystick button 13";
	private string xButton = "joystick button 14";
	private string squareButton = "joystick button 15";
	private string playstationButton = "joystick button 16";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//GetKeyDown and GetKey used as needed
		leftAnologHorizontal = Input.GetAxis ("HorizontalMovement");
		leftAnologVertical = Input.GetAxis ("VerticalMovement");
		rightAnologHorizontal = Input.GetAxis ("HorizontalAiming");
		rightAnologVertical = Input.GetAxis ("VerticalAiming");



		leftBumper = Input.GetKey (leftBumperButton);
		rightTrigger = Input.GetKey (rightTriggerButton);
		leftTrigger = Input.GetKey (leftTriggerButton);
		select = Input.GetKey (selectButton);
		playstation = Input.GetKey (playstationButton);
		rightAnologClick = Input.GetKey (rightAnologButton);
		leftAnologClick = Input.GetKey (leftAnologButton);
		dpadUp = Input.GetKey (dpadUpButton);
		dpadRight = Input.GetKey (dpadRightButton);
		dpadLeft = Input.GetKey (dpadLeftButton);
		dpadDown = Input.GetKey (dpadDownButton);

		circle = Input.GetKeyDown (circleButton);
		square = Input.GetKeyDown (squareButton);
		triangle = Input.GetKeyDown (triangleButton);
		x = Input.GetKeyDown (xButton);
		rightBumper = Input.GetKeyDown (rightBumperButton);
		start = Input.GetKeyDown (startButton);


		
	}
}
