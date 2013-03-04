using UnityEngine;
using System.Collections;

public class BotAttribute : Stats {
	public BotAttribute(){
		BaseValue = 0;	
	}
}

public enum BotAttributeName{
	MaxHealth,
	MaxShield,
	Armor,
	Speed,
	Damage,
	Bounty,
	Score
}