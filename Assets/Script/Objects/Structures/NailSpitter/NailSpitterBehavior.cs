using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NailSpitterBehavior: IStructureBehavior {
	
	
	#region IStructureBehavior implementation
	public int attack (AttackingEntity structure, GameObject[] targets)
	{
		int returnCode = 0;
		GameObject tgt = targets[0];
		if(tgt){
			GameObject center = structure.transform.GetChild(1).gameObject;
			GameObject gun	  = center;
			Quaternion rot =  Quaternion.Slerp(center.transform.rotation , 
							             Quaternion.LookRotation(tgt.transform.position  - center.transform.position) , 100f);
			center.transform.rotation = Quaternion.Euler(-90, rot.eulerAngles.y, 90);
			//center.transform.Rotate(Vector3.up, 0f);
			if(structure.lastShot <= 0){
				//gun.particleSystem.Play();
				GameObject newShot = Object.Instantiate(Resources.Load("Prefabs/Nail")) as GameObject;
				AudioClip shotSound = Resources.Load("AudioClip/NailSpitter/Nail Spitter Shot") as AudioClip;
				newShot.name = "Shot" + Time.realtimeSinceStartup;
				newShot.transform.position = gun.transform.position + new Vector3( 0, .8f , 0);
				Shot shot = newShot.GetComponent<Shot>();
				shot.target = tgt.gameObject;
				shot.dps = structure.tower.damage;
				structure.lastShot = structure.tower.attackSpeed;
				AudioSource.PlayClipAtPoint(shotSound, gun.transform.position , 1);
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