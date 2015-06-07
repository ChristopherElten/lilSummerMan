using UnityEngine;
using System.Collections;

public class DynamicTextDisplayController : MonoBehaviour {

	public int lifespan;
	public int movementSpeed;
	private Rigidbody2D myRigidbody2D;

	// Use this for initialization
	void Start () {

		myRigidbody2D = this.GetComponent<Rigidbody2D> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		//animation
		//Also consider using gravity;
		myRigidbody2D.velocity = new Vector2 (myRigidbody2D.velocity.x, movementSpeed);

		//Deleting object after given amount of time
		Destroy (this.gameObject, lifespan);

	}
}
