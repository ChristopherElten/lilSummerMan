using UnityEngine;
using System.Collections;

public class EquipableInventoryItem : MonoBehaviour {

	//Type of equipment (location/position ex: left_foot, right_foot)
	public Equipment type;
	//Sprite to render
	public Sprite sprite;
	//Name of equipment
	public string name;

	void Start(){
		this.gameObject.AddComponent<SpriteRenderer>();
		this.gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
	}

}
