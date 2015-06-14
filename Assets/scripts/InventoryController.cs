using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryController : MonoBehaviour {

	
	//Items on player (Equipped)
	public EquipableInventoryItem[] equipedInventoryItems;

	//Item containers for unequiped Items, still held by player
	public List<EquipableInventoryItem> equipableInventoryItems = new List<EquipableInventoryItem>();
	public List<ConsumableInventoryItem> consumableInventoryItems = new List<ConsumableInventoryItem>();

	void OnEnable(){
		equipedInventoryItems = new EquipableInventoryItem[Equipment.GetNames(typeof(Equipment)).Length];
	}

	public void collect(GameObject item){
		//Allocating Item to correct location
		if (item.GetComponent<EquipableInventoryItem>()){
			equipableInventoryItems.Add(item.GetComponent<EquipableInventoryItem>());
		} else if (item.GetComponent<ConsumableInventoryItem>()){
			consumableInventoryItems.Add(item.GetComponent<ConsumableInventoryItem>());
		}
	}

	//Equiping item
	public void equip(EquipableInventoryItem item){
		//If there is already an item equiped add to corresponding lists
		if (equipedInventoryItems[(int)item.type ]){
			//Add previously equipped item to player storage list
			equipableInventoryItems.Add(equipedInventoryItems[(int)item.type]);
		}
		//New item stored in currently equipped storage
		equipedInventoryItems[(int)item.type]=item;
	}
}
