using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryController : MonoBehaviour {

	
	//Items on player (Equipped)
	public EquipableInventoryItem[] equipedInventoryItems = new EquipableInventoryItem[Equipment.GetNames(typeof(Equipment)).Length];

	//Item containers for unequiped Items, still held by player
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

		//Temp for testing
		equipableInventoryItems.ForEach(Print);
	}

	//Equiping item
	public void equip(EquipableInventoryItem item){
		//If there is already an item equiped add to corresponding lists
		if (equipedInventoryItems[item.type]){
			equipableInventoryItems.Add(equipedInventoryItems[item.type]);
			equipedInventoryItems[item.type]=item;
		}
	}


	//Temp for testing
	private void Print(EquipableInventoryItem e){
		Debug.Log(e.type);
	}
}
