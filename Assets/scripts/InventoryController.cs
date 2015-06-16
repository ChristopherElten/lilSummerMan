using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryController : MonoBehaviour {

	
	//Items on player (Equipped)
	//TODO Change to DICTIONARY?
	public EquipableInventoryItem[] equipedInventoryItems;

	//Item containers for unequiped Items, still held by player
	public LinkedList<EquipableInventoryItem> equipableInventoryItems = new LinkedList<EquipableInventoryItem>();
	public LinkedList<ConsumableInventoryItem> consumableInventoryItems = new LinkedList<ConsumableInventoryItem>();

	void OnEnable(){
		//Setting the size of the array to all the potential slots for equipment.
		equipedInventoryItems = new EquipableInventoryItem[Equipment.GetNames(typeof(Equipment)).Length];
	}

	public void collect(GameObject item){
		//Allocating Item to correct location
		if (item.GetComponent<EquipableInventoryItem>()){
			equipableInventoryItems.AddLast(item.GetComponent<EquipableInventoryItem>());
		} else if (item.GetComponent<ConsumableInventoryItem>()){
			consumableInventoryItems.AddLast(item.GetComponent<ConsumableInventoryItem>());
		}
	}

	//Equiping item
	public void equip(EquipableInventoryItem item){
		//New item stored in currently equipped storage. NOTE:  No change to the list of equipable items
		Debug.Log((int)item.type);
		equipedInventoryItems[0] = item;
		equipedInventoryItems[(int)item.type] = item;
	}
}
