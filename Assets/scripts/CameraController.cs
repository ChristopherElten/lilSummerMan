using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	//Lerping/tween towards player
	public int maxDistanceFromPlayer;
	public float speedToMoveTowardsPlayer;
	private Transform playerTransform;
	private bool catchingUpToPlayer;
	private float lastCatchUp;

	private Camera myCamera;



	// Use this for initialization
	void Start () {
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y+2, -10);

		myCamera = GetComponent<Camera> ();

		myCamera.orthographicSize = 8;
	}
	
	// Update is called once per frame
	void Update () {

		//Catch up to player if they are far enough
 		if (Mathf.Abs (transform.position.x - playerTransform.position.x) > maxDistanceFromPlayer){
			catchingUpToPlayer = true;
		} else if (Mathf.Abs (transform.position.x - playerTransform.position.x) > 0.05){
			catchingUpToPlayer = false;
		}

		if (catchingUpToPlayer) {
			transform.position = new Vector3 (Mathf.Lerp (transform.position.x, playerTransform.position.x, lastCatchUp/100 * lastCatchUp), transform.position.y, -10);
			lastCatchUp += speedToMoveTowardsPlayer;
		} else {
			lastCatchUp = 0;
		}

		transform.rotation = Quaternion.identity;
	}
}
