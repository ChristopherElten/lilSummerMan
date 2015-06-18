using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryController : MonoBehaviour {

	//Player equipped items to set dictionary with initially
	[SerializeField] GameObject initial_right_hand_weapon;
	[SerializeField] GameObject initial_left_hand_weapon;
	[SerializeField] GameObject initial_arms;
	[SerializeField] GameObject initial_legs;
	[SerializeField] GameObject initial_torso;
	[SerializeField] GameObject initial_head;

	//Items on player (Equipped)
	public Dictionary<Equipment, LinkedListNode<EquipableInventoryItem>> equipedInventoryItems = new Dictionary<Equipment, LinkedListNode<EquipableInventoryItem>>();
		
	//Item containers for unequiped Items, still held by player
	public Dictionary<Equipment, LinkedList<EquipableInventoryItem>> equipableInventoryItems = new Dictionary<Equipment, LinkedList<EquipableInventoryItem>>();
	public LinkedList<ConsumableInventoryItem> consumableInventoryItems = new LinkedList<ConsumableInventoryItem>();

	//Linked lists for equipment types
	LinkedList<EquipableInventoryItem> right_hand_weapons_ll = new LinkedList<EquipableInventoryItem>();
	LinkedList<EquipableInventoryItem> left_hand_weapons_ll = new LinkedList<EquipableInventoryItem>();
	LinkedList<EquipableInventoryItem> legs_ll = new LinkedList<EquipableInventoryItem>();
	LinkedList<EquipableInventoryItem> arms_ll = new LinkedList<EquipableInventoryItem>();
	LinkedList<EquipableInventoryItem> torso_ll = new LinkedList<EquipableInventoryItem>();
	LinkedList<EquipableInventoryItem> head_ll = new LinkedList<EquipableInventoryItem>();

	void OnEnable(){
		//Initializing player inventory to dictionary
		//Initializing Equipable Inventory

		LinkedListNode<EquipableInventoryItem> primary_legs = new LinkedListNode<EquipableInventoryItem>(initial_legs.GetComponent<EquipableInventoryItem>());
		LinkedListNode<EquipableInventoryItem> primary_arms = new LinkedListNode<EquipableInventoryItem>(initial_arms.GetComponent<EquipableInventoryItem>());
		LinkedListNode<EquipableInventoryItem> primary_right_hand_weapon = new LinkedListNode<EquipableInventoryItem>(initial_right_hand_weapon.GetComponent<EquipableInventoryItem>());
		LinkedListNode<EquipableInventoryItem> primary_left_hand_weapon = new LinkedListNode<EquipableInventoryItem>(initial_left_hand_weapon.GetComponent<EquipableInventoryItem>());
		LinkedListNode<EquipableInventoryItem> primary_torso = new LinkedListNode<EquipableInventoryItem>(initial_torso.GetComponent<EquipableInventoryItem>());
		LinkedListNode<EquipableInventoryItem> primary_head = new LinkedListNode<EquipableInventoryItem>(initial_head.GetComponent<EquipableInventoryItem>());
	
		//Adding Linked Lists to dictionary
		equipableInventoryItems.Add(Equipment.right_hand_weapon, right_hand_weapons_ll);
		equipableInventoryItems.Add(Equipment.left_hand_weapon, left_hand_weapons_ll);
		equipableInventoryItems.Add(Equipment.legs, legs_ll);
		equipableInventoryItems.Add(Equipment.arms, arms_ll);
		equipableInventoryItems.Add(Equipment.torso, torso_ll);
		equipableInventoryItems.Add(Equipment.head, head_ll);

		//adding nodes
		equipableInventoryItems[Equipment.right_hand_weapon].AddFirst(primary_right_hand_weapon);
		equipableInventoryItems[Equipment.left_hand_weapon].AddFirst(primary_left_hand_weapon);
		equipableInventoryItems[Equipment.legs].AddFirst(primary_legs);
		equipableInventoryItems[Equipment.arms].AddFirst(primary_arms);
		equipableInventoryItems[Equipment.head].AddFirst(primary_head);
		equipableInventoryItems[Equipment.torso].AddFirst(primary_torso);

		
		equipedInventoryItems[Equipment.legs] = primary_legs;
		equipedInventoryItems[Equipment.arms] = primary_arms;
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
		LinkedListNode<EquipableInventoryItem> item_lln = new LinkedListNode<EquipableInventoryItem>(equipment.GetComponent<EquipableInventoryItem>());
		//Adding to linked list associated with equipment type
		equipableInventoryItems[equipment.GetComponent<EquipableInventoryItem>().equipment_type].AddLast(item_lln);
	}
}
