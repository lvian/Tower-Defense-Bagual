using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Map
{
	public int tileSize = 1;
	public int lines = 12;
	public int columns = 12;
	public float gridWidth;
	private bool loaded;
	static private Map instance;
	
	private Map()
	{
		loaded = false;
	}
	
	static public Map getInstance(){
		if(instance == null){
			instance = new Map();
		}
		return instance;
	}
	
	public bool isCoordinateOutOfBounds(Vector2 coord){
		return !((coord.x > 0 && coord.x <= lines)&&(coord.y > 0 && coord.y <= columns));
	}
	
	public Vector2 tileToWorldCoordinate2d (Vector2 tile)
	{
		return new Vector2((((tile.x)*tileSize)-(tileSize/2)),(((tile.y-1)*tileSize)-(tileSize/2)));
	}
	
	public Vector3 tileToWorldCoordinate3d (Vector2 tile)
	{
		return new Vector3(((tile.x*tileSize)-(tileSize/2)), 0, ((tile.y*tileSize)-(tileSize/2)));
	}
	
	public Vector2 coordinateToTile(Vector3 coord){
		Vector2 tile = new Vector2();
		if(Mathf.Floor(coord.x) + .5f < coord.x)
			tile.x = Mathf.Ceil(coord.x);
		else
			tile.x = Mathf.Floor(coord.x);
		if(Mathf.Floor(coord.z) + .5f < coord.z)
			tile.y = Mathf.Ceil(coord.z);
		else
			tile.y = Mathf.Floor(coord.z);
		return tile;
	}
	
	public bool isLoaded(){
		return loaded;	
	}
	
	public GameObject getTile(Vector2 coordinate){
		return GameObject.Find("Tile"+coordinate.x+"x"+coordinate.y);
	}


}