using UnityEngine;
using System.Collections;

public class BotVitals : Stats {
	public BotVitals(){
		BaseValue = 0;
	}
}

public enum BotVitalsName {
	Health,
	Shield
}
