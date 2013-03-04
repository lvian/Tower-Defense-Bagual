using UnityEngine;
using System.Collections;

public class NestStillness : IMoveBehavior{
	private bool	facing;
	private Vector3 position;
	
	public NestStillness(Vector3 pos){
		facing = false;
		position = pos;
	}
	
	#region IMoveBehavior implementation
	public float move (GameObject gameObject)
	{
		if(!facing){
			Transform thisTransform = gameObject.transform;
			Bot bot = gameObject.GetComponent<Bot>();
			Vector3 waypoint = bot.wayPoints[bot.waypointId].transform.position;
			thisTransform.rotation =  Quaternion.Slerp(thisTransform.rotation , Quaternion.LookRotation(new Vector3(waypoint.x, thisTransform.position.y, waypoint.z)  - thisTransform.position) , 1000f);
			bot.transform.position = position;
			facing = true;
		}
		return 0f;
	}
	#endregion
}
