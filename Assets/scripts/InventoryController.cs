using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryController : MonoBehaviour {

	//Player equipped items to set dictionary with initially
	public EquipableInventoryItem initial_right_hand_weapon;
	public EquipableInventoryItem initial_left_hand_weapon;
	public EquipableInventoryItem initial_boots;
	public EquipableInventoryItem initial_gauntlets;
	public EquipableInventoryItem initial_pants;
	public EquipableInventoryItem initial_torso;
	public EquipableInventoryItem initial_head;

	//Items on player (Equipped)
	public Dictionary<Equipment, LinkedListNode<EquipableInventoryItem>> equipedInventoryItems = new Dictionary<Equipment, LinkedListNode<EquipableInventoryItem>>();
		
	//Item containers for unequiped Items, still held by player
	public Dictionary<Equipment, LinkedList<EquipableInventoryItem>> equipableInventoryItems = new Dictionary<Equipment, LinkedList<EquipableInventoryItem>>();
	public LinkedList<ConsumableInventoryItem> consumableInventoryItems = new LinkedList<ConsumableInventoryItem>();
	
	void OnEnable(){
		//Initializing player inventory to dictionary
		//Initializing Equipable Inventory

		LinkedListNode<EquipableInventoryItem> primary_boots = new LinkedListNode<EquipableInventoryItem>(initial_boots);
		LinkedListNode<EquipableInventoryItem> primary_pants = new LinkedListNode<EquipableInventoryItem>(initial_pants);
		LinkedListNode<EquipableInventoryItem> primary_gauntlets = new LinkedListNode<EquipableInventoryItem>(initial_gauntlets);
		LinkedListNode<EquipableInventoryItem> primary_right_hand_weapon = new LinkedListNode<EquipableInventoryItem>(initial_right_hand_weapon);
		LinkedListNode<EquipableInventoryItem> primary_left_hand_weapon = new LinkedListNode<EquipableInventoryItem>(initial_left_hand_weapon);
		LinkedListNode<EquipableInventoryItem> primary_torso = new LinkedListNode<EquipableInventoryItem>(initial_torso);
		LinkedListNode<EquipableInventoryItem> primary_head = new LinkedListNode<EquipableInventoryItem>(initial_head);
	

		equipedInventoryItems[Equipment.boots] = primary_boots;
		equipedInventoryItems[Equipment.pants] = primary_pants;
		equipedInventoryItems[Equipment.gauntlets] = primary_gauntlets;
		equipedInventoryItems[Equipment.right_hand_weapon] = primary_right_hand_weapon;
		equipedInventoryItems[Equipment.left_hand_weapon] = primary_left_hand_weapon;
		equipedInventoryItems[Equipment.torso] = primary_torso;
		equipedInventoryItems[Equipment.head] = primary_head;
	}

	//TODO Delegate these methods...

	//Equiping an item
	public void equip(LinkedListNode<EquipableInventoryItem> equipment){
		equipedInventoryItems[equipment.Value.equipment_type] = equipment;
	}

	//Collecting new Equipable item
	public void collectEquipableItem(EquipableInventoryItem equipment){
		//Allocating Item to end of linked list
		LinkedListNode<EquipableInventoryItem> newItem = new LinkedListNode<EquipableInventoryItem>(equipment.GetComponent<EquipableInventoryItem>());
		//Adding to linked list associated with equipment type
		equipableInventoryItems[equipment.GetComponent<EquipableInventoryItem>().equipment_type].AddLast(newItem);
	}
}
