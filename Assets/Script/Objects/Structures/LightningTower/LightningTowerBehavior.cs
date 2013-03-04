using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LightningTowerBehavior : IStructureBehavior {

	#region IStructureBehavior implementation
	public int attack (AttackingEntity structure, GameObject[] targets)
	{
		int returnCode = 0;
		GameObject tgt = targets[0];
		if(tgt){
			GameObject center = structure.transform.gameObject;
			Transform gun	  = center.transform.FindChild("cima_lightning_tower_Cube_002") as Transform;
			/*
			Quaternion rot =  Quaternion.Slerp(gun.transform.rotation , 
							             Quaternion.LookRotation(tgt.transform.position  - gun.transform.position) , 100f);
			gun.transform.rotation = Quaternion.Euler(0, rot.eulerAngles.y, 0);
			gun.transform.Rotate(Vector3.up, -90);
			*/
			if(structure.lastShot <= 0){
				//waitting for a lighting particle effect
				//gun.transform.particleSystem.Play();
				GameObject newShot = Object.Instantiate(Resources.Load("Prefabs/ArchedShot")) as GameObject;
				newShot.name = "Shot" + Time.realtimeSinceStartup;
				//temp fix for shot height
				newShot.transform.position = new Vector3(gun.transform.position.x, gun.transform.position.y + 1 ,gun.transform.position.z);
				JumpShot shot = newShot.GetComponent<JumpShot>();
				shot.target = tgt.gameObject;
				shot.dps = structure.tower.damage;
				shot.jumps = structure.tower.jumps;
				shot.jumpRange = structure.tower.reach / 2;
				structure.lastShot = structure.tower.attackSpeed;
			}
		}
		else{
			returnCode = 1;	
		}
		return returnCode;
	}
	

	public void applyAuraEffects (AttackingEntity structure, GameObject target)
	{
		return;
	}

	public void removeAuraEffects (AttackingEntity structure, GameObject target)
	{
		return;
	}
	
	public void handleUpdate (GameObject gameObject)
	{
		
	}
	#endregion
	
	
}
