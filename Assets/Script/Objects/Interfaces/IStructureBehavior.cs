using System.Collections;
using UnityEngine;

public interface IStructureBehavior
{
	int attack(AttackingEntity structure,GameObject[] targets);
	
	void applyAuraEffects(AttackingEntity structure, GameObject target);
	
	void removeAuraEffects(AttackingEntity structure, GameObject target);
	
	void handleUpdate(GameObject gameObject);
}


