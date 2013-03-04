using System;
using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
	public bool tileEmpty;
	public bool path;
	public bool slot;
	public GameObject tower;
	
	void Start(){
	}
	
	void OnDrawGizmos(){
		if(path)
			Gizmos.color =  Color.blue;
		if(slot)
			Gizmos.color = Color.black;
		Matrix4x4 localMatrix = Matrix4x4.TRS(transform.localPosition, transform.localRotation, new Vector3(1, .01f, 1));
		Gizmos.matrix = localMatrix;
		Gizmos.DrawCube(Vector3.zero, new Vector3(1, .01f, 1));
	}
	
	public bool isEmpty(){
		return tileEmpty;	
	}
}