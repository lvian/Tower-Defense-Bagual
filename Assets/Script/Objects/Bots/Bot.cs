using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Bot : InvadingEntity {
	
	public  int		 			waypointId;
	private Transform			thisTransform;
	public  List<Transform>		wayPoints;
	public  int					lane;
	private bool 				deathInformed = false;
	private bool 				exitInformed =  false;
	
	public void Start(){
		thisTransform = transform;
	}
	
	public void Awake(){
		waypointId = 0;
		wayPoints = new List<Transform>();
	}
	
	public void Update(){
		if(!Game.getInstance().isPaused()){
			if(isAlive()){
				handleUpdate();
				move();
			}
			else{
				if(animation){
					animation["dying"].speed = 4f;
					animation["dying"].wrapMode = WrapMode.ClampForever;
					animation.CrossFade("dying");
				}
				StartCoroutine("Die");
			}
		}
		else{
			animation["dying"].speed = 0f;
			animation["walk"].speed = 0f;
		}
		
	}
	
	public bool isAlive(){
		return (getVitalValue(BotVitalsName.Health) > 0);
	}
	
	public void setPath(int id){
		lane = id;
		foreach(GameObject waypoint in GameObject.FindGameObjectsWithTag("Waypoint")) { 
			if(waypoint.GetComponent<WayPoint>().PathID == id){
				wayPoints.Add(waypoint.transform);
			}			
		}
		
		wayPoints.Sort(delegate(Transform t1, Transform t2){
			int _t1 = int.Parse(t1.name.Substring(t1.name.LastIndexOf(".")+1).Trim());
			int _t2 = int.Parse(t2.name.Substring(t2.name.LastIndexOf(".")+1).Trim());
			return  _t1.CompareTo(_t2);
			//return t1.name.CompareTo(t2.name);
		});
		transform.position = wayPoints[waypointId].position;
		waypointId++;
		transform.LookAt(wayPoints[waypointId]);
	}
	
	public void setPath(int id, int waypointID, Vector3 startPos){
		lane = id;
		foreach(GameObject waypoint in GameObject.FindGameObjectsWithTag("Waypoint")) { 
			if(waypoint.GetComponent<WayPoint>().PathID == id){
				wayPoints.Add(waypoint.transform);
			}			
		}
		
		wayPoints.Sort(delegate(Transform t1, Transform t2){
			int _t1 = int.Parse(t1.name.Substring(t1.name.LastIndexOf(".")+1).Trim());
			int _t2 = int.Parse(t2.name.Substring(t2.name.LastIndexOf(".")+1).Trim());
			return  _t1.CompareTo(_t2);
			//return t1.name.CompareTo(t2.name);
		});
		transform.position = startPos;
		waypointId = waypointID;
		transform.LookAt(wayPoints[waypointID]);
	}
	
	public IEnumerator Die(){
		if(!deathInformed){
			AudioClip shotSound = Resources.Load("FX/Death 1") as AudioClip;
			Instantiate(Resources.Load("Prefabs/SteamRebelExplosions"), thisTransform.localPosition, Quaternion.identity);
			Messenger<GameObject>.Broadcast("BotDied", this.gameObject);
			deathInformed = true;
			AudioSource.PlayClipAtPoint(shotSound, gameObject.transform.position , 1);
		}
		yield return new WaitForSeconds(3f);
		
		Destroy(this.gameObject);
	}
	
	public IEnumerator Leave(){
		if(!exitInformed){
			Messenger<GameObject>.Broadcast("BotLeft", this.gameObject);
			Instantiate(Resources.Load("Prefabs/SteamRebelExplosions"), thisTransform.localPosition, Quaternion.identity);
			exitInformed =  true;
		}
		yield return new WaitForSeconds(.1f);
		Destroy(this.gameObject);
	}	
}
