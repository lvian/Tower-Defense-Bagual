/*using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(Lane))]
public class LaneEditor : Editor {
	SerializedObject waypoint;
	SerializedProperty lanesCount;
	SerializedProperty laneid;
	
	int objectsCount;
	
	
	// Use this for initialization
	void OnEnable() {
		waypoint = new SerializedObject(target);
		lanesCount = waypoint.FindProperty("waypointsCount");
		laneid =  waypoint.FindProperty("pathID");
		objectsCount = (GameObject.FindObjectOfType(typeof(Lane)) as Lane).gameObject.GetComponentsInChildren<WayPoint>().Length;
	}
	
	public override void OnInspectorGUI(){
		waypoint.Update();
		
		GUILayout.Label("WayPoints");
		laneid.intValue = EditorGUILayout.IntField("Lane ID", laneid.intValue); 
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Number of Waypoints\t\t"+lanesCount.intValue);
		if(GUILayout.Button("-")){
			if(lanesCount.intValue != 1){
				lanesCount.intValue--;
			}
		}
		if(GUILayout.Button("+")){
			lanesCount.intValue++;
		}
		EditorGUILayout.EndHorizontal();
		
		waypoint.ApplyModifiedProperties();
		
		if(lanesCount.intValue != objectsCount){
			if(	lanesCount.intValue > objectsCount){
				GameObject waypointInstance = Instantiate(Resources.Load("Prefabs/waypoints/WayPoint")) as GameObject;
				foreach ( GameObject lns in GameObject.FindGameObjectsWithTag("Lane")){
					if(lns.GetComponent<Lane>().pathID == laneid.intValue){
						waypointInstance.transform.parent = lns.gameObject.transform;
						waypointInstance.GetComponent<WayPoint>().PathID = laneid.intValue;
					}
				}
				if(objectsCount != 0)
					GameObject.Find("Waypoint."+laneid.intValue+"."+objectsCount).GetComponent<WayPoint>().next = waypointInstance;
				objectsCount++;
				waypointInstance.name = "Waypoint."+laneid.intValue+"."+objectsCount;
			}
			if(	lanesCount.intValue < objectsCount){
				DestroyImmediate(GameObject.Find("Waypoint"+laneid.intValue+"."+(objectsCount)));
				objectsCount--;
			}
		}
	}
	
	void OnSceneGUI(){
				
	}
}*/
