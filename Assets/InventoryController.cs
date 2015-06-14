using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryController : MonoBehaviour {

	//Item containers
	public List<EquipableInventoryItem> equipableInventoryItems = new List<EquipableInventoryItem>();
	public List<ConsumableInventoryItem> consumableInventoryItems = new List<ConsumableInventoryItem>();

	public void collect(GameObject item){
		//Allocating Item to correct location
		if (item.GetComponent<EquipableInventoryItem>()){
			EquipableInventoryItem newItem = item.GetComponent<EquipableInventoryItem>();
			equipableInventoryItems.Add(newItem);
		} else if (item.GetComponent<ConsumableInventoryItem>()){
			consumableInventoryItems.Add(item.GetComponent<ConsumableInventoryItem>());
		}

		Debug.Log ("Current Equipable Inventory: " + equipableInventoryItems.FindLast().position);
	}

}
