/*using UnityEngine;
using System.Collections;

public class MapDraw : MonoBehaviour {

	//public int tileSize;
	public int lines;
	public int columns;
	public float gridWidth;
	private bool stateChanged;
	private Game gameState;
	private int currentState;
	private int lastState;
	private GameObject[] grid;
	public void initializeMap(){
		Map.getInstance().lines = lines;
		Map.getInstance().columns = columns;
		Map.getInstance().gridWidth = gridWidth;
		transform.position = new Vector3((Map.getInstance().tileSize * Map.getInstance().lines) / 2, 0, (Map.getInstance().tileSize * Map.getInstance().columns ) /2);
		transform.localScale = new Vector3((Map.getInstance().tileSize * Map.getInstance().lines), .1f, (Map.getInstance().tileSize * Map.getInstance().columns));
		drawGrid();
	}
	
	void drawGrid(){
		GameObject gridComponents = new GameObject();
		gridComponents.name = "GridComponents";
		for(int l = 0; l <= Map.getInstance().lines; l++){
			GameObject tmp = GameObject.CreatePrimitive(PrimitiveType.Plane);
			tmp.name = "GridLine"+l;
			tmp.transform.position = new Vector3((Map.getInstance().columns * Map.getInstance().tileSize) /2, .2f, l * Map.getInstance().tileSize);
			tmp.transform.localScale = new Vector3(Map.getInstance().lines , Map.getInstance().gridWidth, Map.getInstance().gridWidth);
			tmp.renderer.material.color = Color.black;
			tmp.transform.parent = gridComponents.transform;
			tmp.GetComponent<MeshCollider>().enabled = false;
			tmp.tag = "Grid";
			tmp.renderer.enabled = false;
		}
		for(int c = 0; c <= Map.getInstance().columns; c++){
			GameObject tmp = GameObject.CreatePrimitive(PrimitiveType.Plane);
			tmp.name = "GridColumn"+c;
			tmp.transform.position = new Vector3(c * Map.getInstance().tileSize, .2f, (Map.getInstance().lines * Map.getInstance().tileSize) /2);
			tmp.transform.localScale = new Vector3(Map.getInstance().gridWidth, Map.getInstance().gridWidth, Map.getInstance().columns);
			tmp.renderer.material.color = Color.black;
			tmp.transform.parent = gridComponents.transform;
			tmp.GetComponent<MeshCollider>().enabled = false;
			tmp.tag = "Grid";
			tmp.renderer.enabled = false;
		}
	}
	
	void Start(){
		initializeMap();
		grid = GameObject.FindGameObjectsWithTag("Grid");
		stateChanged = false;
		gameState = Game.getInstance();
		lastState = 1;
		currentState = 1;
	}
	
	void Update(){
		currentState =gameState.GState.getState();
		
		if(currentState == 2 && currentState != lastState )
		{
			showGrid();
			lastState = 2;
		}
		if(currentState != 2 && currentState != lastState)
		{
			hideGrid();
			lastState = 1;
		}
	}
	
	void showGrid()
	{
		foreach(GameObject g in grid)
		{
				g.renderer.enabled = true;
		}
		
	}
	
	void hideGrid()
	{
		foreach(GameObject g in grid)
		{
			g.renderer.enabled = false;
			
		}
		
	}
		
}

*/