/*using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BunkerBehavior : IStructureBehavior {
	
	
	#region IStructureBehavior implementation
	public int attack (AttackingEntity structure, GameObject[] targets)
	{
		int returnCode = 0;
		
		foreach( GameObject tgt in targets){
			if(tgt){
				if(Time.realtimeSinceStartup - structure.lastShot > 1 || structure.lastShot == 0){
					GameObject newShot = Object.Instantiate(Resources.Load("Prefabs/Shot")) as GameObject;
					newShot.name = "Shot" + Time.realtimeSinceStartup;
					newShot.transform.position = structure.transform.position;
					Shot shot = newShot.GetComponent<Shot>();
					shot.target = tgt.gameObject;
					shot.dps = structure.structureStats.dps;
					structure.lastShot = Time.realtimeSinceStartup;
				}
			}
			returnCode = 0;
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
	#endregion
}*/