using UnityEngine;
using System.Collections;

public interface IDefendBehavior {
	
	float handleAttack(Damage damage, Effect[] effects, GameObject gameObject);
	
}
