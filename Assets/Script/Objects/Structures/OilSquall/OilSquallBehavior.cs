using UnityEngine;
using System.Collections;

public class OilSquallBehavior : IStructureBehavior {
	private Effect effect;
	
	public OilSquallBehavior(){
		effect = new Effect();
		effect.MultiplicativeMods.Add((int)BotAttributeName.Speed, new Modifier(.7f));
	}
	
	#region IStructureBehavior implementation
	public int attack (AttackingEntity structure, GameObject[] targets)
	{
		Transform thisTransform = structure.transform;
		int returnCode = 0;
		Vector3 center = Vector3.zero;
		GameObject tgt = targets[0];
		
		if(tgt)
		{
			center = center + tgt.transform.position;
			thisTransform.rotation =  Quaternion.Slerp(thisTransform.rotation , Quaternion.LookRotation(center  - thisTransform.position) , 1000f);
			thisTransform.rotation = Quaternion.Euler(0f, thisTransform.rotation.eulerAngles.y, 0f);
			thisTransform.Rotate(0f, 90f, 0f);
		}
		else
		{
			returnCode = 1;	
		}
		
		return returnCode;
	}

	public void applyAuraEffects (AttackingEntity structure, GameObject target)
	{
		try{
			Bot bot = target.GetComponent<Bot>();
			if(bot != null){
				effect.Source = structure.gameObject;
				bot.addEffect(effect);
			}
		}catch{ return; }
	}

	public void removeAuraEffects (AttackingEntity structure, GameObject target)
	{
		try{
			Bot bot = target.GetComponent<Bot>();
			bot.removeEffect(effect);
		}catch{ return; }
	}
	
	public void handleUpdate (GameObject gameObject)
	{
		
	}
	#endregion
}