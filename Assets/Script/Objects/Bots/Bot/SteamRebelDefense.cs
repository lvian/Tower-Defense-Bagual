using UnityEngine;
using System.Collections;

public class SteamRebelDefense : IDefendBehavior{
	#region IDefendBehavior implementation
	float IDefendBehavior.handleAttack (Damage damage, Effect[] effects, GameObject gameObject)
	{
		return 0f;
	}
	#endregion
}
