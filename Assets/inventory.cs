using UnityEngine;
using System.Collections;

public class inventory : MonoBehaviour {


	// TODO CHANGE GAMEOBJECT TYPE TO EQUIPMENT OBJECT... GIVE EQUIPMENT OBJECTS EQUIP AND REMOVE FUNCTIONS
	// ToDo ENUM LOCATION OF EQUIPMENT

	//Inventory for equiped, unequiped wearables
	private GameObject[] wearables;
	//Inventory for consumables
	private GameObject[] consumables;

	public void equip(GameObject equipment){
		wearables[0]=equipment;
	}
	public void remove(int equipmentLocationTemp){
//		wearables[equipmentLocationTemp].unequip();
	}
}
