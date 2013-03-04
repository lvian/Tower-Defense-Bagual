using UnityEngine;
using System.Collections;
 

public class WayPoint : MonoBehaviour  {
	
	private float 		explosionRadius = .15f;
	public	int			pathID = 0;
	public 	GameObject 	next;
	public  bool drawGizmos;
	
	void OnDrawGizmos(){
		drawGizmos = transform.parent.GetComponent<Lane>().showWaypoints;
		pathID = transform.parent.GetComponent<Lane>().pathID;
		
		if(drawGizmos){
			Gizmos.color =  Color.cyan;
	        Gizmos.DrawSphere(transform.position, explosionRadius);
			if (next != null) {
	        	Gizmos.color = Color.yellow;
	            Gizmos.DrawLine(transform.position, next.transform.position);
	        }
		}
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public int PathID {
		get {
			return this.pathID;
		}
		set {
			pathID = value;
		}
	}
}