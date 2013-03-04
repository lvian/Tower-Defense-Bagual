using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class AttackingEntity : MonoBehaviour {
		
	public IStructureBehavior 		structureBehavior;
	public Tower 					tower;
	public float 					lastShot;
	public List<GameObject>			targets;
	
	private StructureAttribute[] 	baseAttributes;
	
	public void init(){
		baseAttributes = new StructureAttribute[Enum.GetValues(typeof(StructureAttributeNames)).Length];
		initBaseAttributes();
		baseAttributes[(int)StructureAttributeNames.Dps].BaseValue			= tower.damage;
		baseAttributes[(int)StructureAttributeNames.Reach].BaseValue		= tower.reach;
		baseAttributes[(int)StructureAttributeNames.Price].BaseValue		= tower.price;
		baseAttributes[(int)StructureAttributeNames.Jumps].BaseValue 		= tower.jumps;
		baseAttributes[(int)StructureAttributeNames.AttackSpeed].BaseValue	= tower.attackSpeed;
		
		// initialize targets' list
		targets = new List<GameObject>();
		
		Messenger<GameObject>.AddListener("BotDied", OnBotDied);
		Messenger<GameObject>.AddListener("BotLeft", OnBotDied);
	}
	
	private void initBaseAttributes(){
		for( int b = 0 ; b < Enum.GetValues(typeof(StructureAttributeNames)).Length; b++ ){
			baseAttributes[b] = new StructureAttribute();	
		}
	}
	
	public float getAttributeValue(StructureAttributeNames id){
		return baseAttributes[(int)id].BaseValue;	
	}
	
	public int attack(AttackingEntity structure, GameObject[] targets)
	{
		return structureBehavior.attack(structure,targets);
	}
	
	public void handleUpdate(GameObject gameObject){
		structureBehavior.handleUpdate(gameObject);
	}
	
	void OnTriggerEnter(Collider other){
		//Debug.Log( other.gameObject.name + " entered");
		Bot bot = other.gameObject.GetComponent<Bot>();
		if( bot != null){
			targets.Add(other.gameObject);
			structureBehavior.applyAuraEffects(this,other.gameObject);
		}
	}
	
	void OnTriggerExit(Collider other){
		GameObject gObject = targets.Find(delegate( GameObject target ) {
			try {
				return 	( target.name == other.name );
			}
			catch {
				return false;
			}
		});
		structureBehavior.removeAuraEffects(this, gObject);
		targets.Remove(gObject);
	}
	
	void OnBotDied(GameObject gObj){
		GameObject gObject = targets.Find(delegate( GameObject target ) {
			try {	
				return 	( target.name == gObj.name );
			} 
			catch {
				return false;	
			}
		});
		targets.Remove(gObject);
	}
}
