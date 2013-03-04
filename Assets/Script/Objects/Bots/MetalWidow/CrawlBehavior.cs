using UnityEngine;
using System.Collections;

public class CrawlBehavior : IMoveBehavior {
	#region IMoveBehavior implementation
	public float move (GameObject gameObject)
	{
		Vector3 initialPosition = gameObject.transform.position;
		Transform thisTransform = gameObject.transform;
		Bot bot = gameObject.GetComponent<Bot>();
	
		gameObject.animation["walk"].speed = 5f * (bot.getAttributeValue(BotAttributeName.Speed)/bot.botStats.speed);
		gameObject.animation["walk"].wrapMode = WrapMode.Loop;
		gameObject.animation.CrossFade("walk");
		gameObject.animation.Play();
		Vector3 waypoint = bot.wayPoints[bot.waypointId].transform.position;
		float distance = Vector3.Distance(gameObject.transform.position , waypoint);
		//Debug.Log(distance);
		if(distance < .5f ) {
			
			if(bot.waypointId +2 <= bot.wayPoints.Count)
			{
				bot.waypointId += 1;
			}	
			else{
				bot.StartCoroutine("Leave");
			}
		}
		thisTransform.rotation = Quaternion.Slerp(thisTransform.rotation, Quaternion.LookRotation(waypoint - thisTransform.position), 5 * Time.deltaTime);
		thisTransform.position+= thisTransform.forward * (bot.getAttributeValue(BotAttributeName.Speed)/50) * Time.deltaTime;
		
		return Vector3.Distance(initialPosition, thisTransform.position);
	}
	#endregion
	
}
