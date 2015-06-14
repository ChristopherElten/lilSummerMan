using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Type of pickup
public enum Pickup {Experience, Mana, Health, Damage};

//Type of Equipment
public enum Equipment {right_foot, left_foot, right_leg, left_leg, right_arm, left_arm, right_weapon, left_weapon, torso, head};

public class GameManager : MonoBehaviour {

	public GameObject textMesh;
	public float rangeTextDisplay;
	private GameObject textHolder;

	// Use this for initialization
	void Start () {
		//Organizing text instantiation
		textHolder = new GameObject ();
		textHolder.name = "TextHolder";
		textHolder.AddComponent<SpriteRenderer> ().sortingLayerName = "DynamicText";
	}

	public void instantiateText(string text, Vector3 pos, Pickup pickup){

		//Instantiating text object whenever event occurs (damage, health, mana, etc)
		//Withing a range of passed location
		pos = new Vector3(Random.Range(pos.x-rangeTextDisplay, pos.x+rangeTextDisplay),Random.Range(pos.y-rangeTextDisplay, pos.y+rangeTextDisplay), 0);
		GameObject temp = Instantiate(textMesh, pos, Quaternion.identity) as GameObject;
		temp.GetComponent<TextMesh> ().text = text;
		temp.transform.SetParent (textHolder.transform);

		if (pickup == Pickup.Experience){
			temp.GetComponent<TextMesh>().color = Color.grey;
		} else if (pickup == Pickup.Mana){
			temp.GetComponent<TextMesh>().color = Color.blue;
		} else if (pickup == Pickup.Health){
			temp.GetComponent<TextMesh>().color = Color.green;
		} else if (pickup == Pickup.Damage){
			temp.GetComponent<TextMesh>().color = Color.red;
		}
	}
}
