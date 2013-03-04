using UnityEngine;
using System.Collections;

public class SpiderlingWalking : IMoveBehavior {
	private float offsetX;
	private bool initialCenter;
	
	public SpiderlingWalking(float offsetX){
		this.offsetX = offsetX;
		initialCenter = false;
	}
	
	#region IMoveBehavior implementation
	public float move (GameObject gameObject){
		Transform thisTransform = gameObject.transform;
		if(!initialCenter){
			initialCenter = true;
		}
		else{
			thisTransform.Translate(-offsetX,0f,0f);
		}
		Vector3 initialPosition = gameObject.transform.position;
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
		thisTransform.Translate(offsetX,0f,0f);
		
		return Vector3.Distance(initialPosition, thisTransform.position);
	}
	#endregion
	
}
