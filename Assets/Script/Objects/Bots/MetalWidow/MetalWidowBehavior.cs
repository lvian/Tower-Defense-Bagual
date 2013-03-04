using UnityEngine;
using System.Collections;

public class MetalWidowBehavior : IBotBehavior {
	private bool eggsLaid;
	
	public MetalWidowBehavior(){
		eggsLaid = false;
	}
	
	#region IBotBehavior implementation
	public void handleUpdate (GameObject gameObject){
		Bot bot = gameObject.GetComponent<Bot>();
		if(!eggsLaid && bot.distanceMoved >= 5){
			IBotStats botStats = new Nest();
			GameObject  newUnit = Object.Instantiate(Resources.Load(botStats.Prefab)) as GameObject;
			newUnit.name = botStats.Name+Time.realtimeSinceStartup;
			Bot botScript = newUnit.GetComponent<Bot>();
			botScript.botStats = new Nest();
			botScript.botBehavior = new NestBehavior();
			botScript.defendBehavior = new NestDefense();
			botScript.moveBehavior = new NestStillness(bot.transform.position);
			botScript.init();
			botScript.setPath(bot.lane, bot.waypointId, bot.transform.position);
			eggsLaid = true;
		}
	}
	#endregion
}
