using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlameTowerBehavior: IStructureBehavior {
	
	
	#region IStructureBehavior implementation
	public int attack (AttackingEntity structure, GameObject[] targets)
	{
		GameObject gun = structure.transform.GetChild(1).gameObject;
		GameObject flame  = gun.transform.GetChild(0).gameObject;
		flame.particleSystem.loop = false;
		int returnCode = 0;
		GameObject tgt = targets[0];
		Collider areaOfDamage = gun.collider;
		if(tgt){
			flame.particleSystem.Play();
			Quaternion rot =  Quaternion.Slerp(gun.transform.rotation , 
							             Quaternion.LookRotation(tgt.transform.position  - gun.transform.position) , 100f);
			gun.transform.rotation = Quaternion.Euler(-90, rot.eulerAngles.y, -90);
			//center.transform.Rotate(Vector3.up, 0f);
			if(structure.lastShot <= 0){
				//gun.particleSystem.Play();
				foreach (GameObject tg in targets)
		        {
					if(areaOfDamage.bounds.Contains(tg.transform.position))
					{
						Bot botScript = tg.GetComponent("Bot") as Bot;
						botScript.Attack(new Damage(structure.tower.damage));
					}
		        }		
				GameObject newShot = Object.Instantiate(Resources.Load("Prefabs/Nail")) as GameObject;
				AudioClip shotSound = Resources.Load("FX/Attack 1") as AudioClip;
				/*
				newShot.name = "Shot" + Time.realtimeSinceStartup;
				newShot.transform.position = gun.transform.position + new Vector3( 0, .8f , 0);
				Shot shot = newShot.GetComponent<Shot>();
				shot.target = tgt.gameObject;
				shot.dps = structure.tower.damage;
				
				*/
				structure.lastShot = structure.tower.attackSpeed;
				AudioSource.PlayClipAtPoint(shotSound, gun.transform.position , 1);
			}
		}
		else{
			flame.particleSystem.Stop();
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