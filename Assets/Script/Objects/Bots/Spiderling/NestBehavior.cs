using UnityEngine;
using System.Collections;

public class NestBehavior : IBotBehavior {
	private float hatchCooldown;
	private bool  eggsHatched;
	
	public NestBehavior(){
		hatchCooldown = 4f;
		eggsHatched = false;
	}
	
	#region IBotBehavior implementation
	public void handleUpdate (GameObject gameObject)
	{
		if(hatchCooldown <= 0 && !eggsHatched){
			Bot nest = gameObject.GetComponent<Bot>();
			
			IBotStats botStats = new Spiderling();
			GameObject  newUnit = Object.Instantiate(Resources.Load(botStats.Prefab),
				nest.transform.position, nest.transform.rotation) as GameObject;
			newUnit.name = botStats.Name+Time.realtimeSinceStartup;
			Bot botScript = newUnit.GetComponent("Bot") as Bot;
			botScript.botStats = new Spiderling();
			botScript.botBehavior = new SpiderlingBehavior();
			botScript.defendBehavior = new SpiderlingDefense();
			botScript.moveBehavior = new SpiderlingWalking(-0.3f);
			botScript.init();
			botScript.setPath(nest.lane, nest.waypointId, nest.transform.position);
			
			botStats = new Spiderling();
			newUnit = Object.Instantiate(Resources.Load(botStats.Prefab)) as GameObject;
			newUnit.name = botStats.Name+Time.realtimeSinceStartup;
			botScript = newUnit.GetComponent("Bot") as Bot;
			botScript.botStats = new Spiderling();
			botScript.botBehavior = new SpiderlingBehavior();
			botScript.defendBehavior = new SpiderlingDefense();
			botScript.moveBehavior = new SpiderlingWalking(0);
			botScript.init();
			
			botScript.setPath(nest.lane, nest.waypointId, new Vector3(
				nest.transform.position.x,
				nest.transform.position.y,
				nest.transform.position.z + 0.3f));
			
			botStats = new Spiderling();
			newUnit = Object.Instantiate(Resources.Load(botStats.Prefab)) as GameObject;
			newUnit.name = botStats.Name+Time.realtimeSinceStartup;
			botScript = newUnit.GetComponent("Bot") as Bot;
			botScript.botStats = new Spiderling();
			botScript.botBehavior = new SpiderlingBehavior();
			botScript.defendBehavior = new SpiderlingDefense();
			botScript.moveBehavior = new SpiderlingWalking(0.3f);
			botScript.init();
			botScript.setPath(nest.lane, nest.waypointId, nest.transform.position);
			
			eggsHatched = true;
			nest.StartCoroutine("Die");
		}
		else{
			hatchCooldown -= Time.deltaTime;
		}
	}
	#endregion
}
