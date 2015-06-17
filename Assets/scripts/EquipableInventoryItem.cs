using UnityEngine;
using System.Collections;

public class EquipableInventoryItem : MonoBehaviour {

	//Type of equipment (location/position ex: left_foot, right_foot)
	public Equipment equipment_type;
	//Sprite to render
	public Sprite right_sprite;
	public Sprite left_sprite;
	//Name of equipment
	public string title;
	public string description;

	void Start(){
		this.gameObject.AddComponent<SpriteRenderer>();
		this.gameObject.GetComponent<SpriteRenderer>().sprite = right_sprite;
	}

}
