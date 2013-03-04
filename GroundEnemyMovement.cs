using UnityEngine;
using System.Collections;

public class GroundEnemyMovement : IMovementBehavior {
	
	private bool onCourse;
	private Map map;
	private Vector2 origen;
	private Vector2 destiny;
	
	public GroundEnemyMovement(GameObject map, Vector2 org){
		this.map = map.GetComponents("Map");
	}
	
	public void move (Object movedObject)
	{
		
	}
}
