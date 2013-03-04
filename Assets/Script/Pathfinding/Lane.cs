using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lane : MonoBehaviour {
	public int pathID;
	public bool showWaypoints = true;
	public bool editMode = true;
	
	void OnDrawGizmos(){
		if(editMode){
			List<GameObject> gos = new List<GameObject>();
			foreach(Component com in GetComponentInChildren<Transform>()){
				gos.Add(com.gameObject);
			}
			
			for(int c = 0; c < gos.Count;c++){
				gos[c].name = "Waypoint."+pathID+"."+ (c+1);
				gos[c].GetComponent<WayPoint>().PathID = pathID;
				gos[c].GetComponent<WayPoint>().drawGizmos = showWaypoints;
				if(c + 1 < gos.Count)
					gos[c].GetComponent<WayPoint>().next = gos[c+1];
			}
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
