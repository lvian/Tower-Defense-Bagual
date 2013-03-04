using UnityEngine;
using System.Collections;

public class BotStats {
	
	public const int STATS_COUNT = 6;
	public enum ID {
		health,
		maxHealth,
		shield,
		maxShield,
		armor,
		speed
	}
	
	public float[] attributes;
	
	public BotStats(){
		attributes = new float[STATS_COUNT];
	}
	
	public BotStats(IBotStats entityStats){
		attributes = new float[STATS_COUNT];
		attributes[(int)ID.maxHealth] = entityStats.health;
		attributes[(int)ID.maxShield] = entityStats.shield;
		attributes[(int)ID.armor]     = entityStats.armor;
		attributes[(int)ID.speed]     = entityStats.speed;
	}
	
	public float getAttributeValue(BotStats.ID attID){
		return attributes[(int)attID];
	}
}
