using UnityEngine;
using System.Collections;

public class NestDefense : IDefendBehavior {
	#region IDefendBehavior implementation
	public float handleAttack (Damage damage, Effect[] effects, GameObject gameObject)
	{
		return 0f;
	}
	#endregion
}
