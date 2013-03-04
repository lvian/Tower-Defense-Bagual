using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CapsuleCollider))]
public class Structure : AttackingEntity {
	
	void Start(){
	}
	
	
	void Update (){
		GetComponent<CapsuleCollider>().radius = getAttributeValue(StructureAttributeNames.Reach);
		GetComponent<CapsuleCollider>().height = (getAttributeValue(StructureAttributeNames.Reach) +1);
		if(!Game.getInstance().isPaused()){
			handleUpdate(gameObject);
			if(targets.ToArray().Length > 0){
				if(this.attack(this,targets.ToArray()) != 0){
					//attack succeded
				}
				else{
					//attack failed	
				}
			}
			lastShot -= Time.deltaTime;
		}
	}
}
