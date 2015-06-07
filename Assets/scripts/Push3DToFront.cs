using UnityEngine;
using System.Collections;

public class Push3DToFront : MonoBehaviour {

	public string layerToPushTo;

	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().sortingLayerName = layerToPushTo;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
