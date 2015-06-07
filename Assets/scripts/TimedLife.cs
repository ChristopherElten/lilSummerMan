using UnityEngine;
using System.Collections;

public class TimedLife : MonoBehaviour {

	//LifeSpan of object
	public float lifeSpan;

	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, lifeSpan);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
