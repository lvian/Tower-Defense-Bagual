using UnityEngine;
using System.Collections;

public class HighlightTile : MonoBehaviour {
	
	public  Object       prefab;
	private GameObject  _tileHighlighter;
	private CameraTools _cameraTools;
	private Game        _gameState;

	void Start () {
		_cameraTools = GameObject.Find("Camera").GetComponent("CameraTools") as CameraTools;
		_tileHighlighter = Object.Instantiate(prefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;
		_tileHighlighter.gameObject.renderer.enabled = false;
		_gameState = Game.getInstance();
	}
	
	void Update () {
		Vector3 mouseOnCoord3 = _cameraTools.ScreenToWorld(Input.mousePosition);
		Vector2 mouseOnCoord2 = Map.getInstance().coordinateToTile(mouseOnCoord3);
		//Debug.Log(mouseOnCoord3+ " " + mouseOnCoord2);
		if(GUIManager.isHitingInterface()){
			_tileHighlighter.gameObject.renderer.enabled = false;
			return;	
		}
		if(!_gameState.isPaused()){
			GameObject tile = Map.getInstance().getTile(mouseOnCoord2);
			if(!tile){
				_tileHighlighter.gameObject.renderer.enabled = false;
				return;
			}
			if(!tile.GetComponent<Tile>().slot){
				_tileHighlighter.gameObject.renderer.enabled = false;
				return;
			}
			
			_tileHighlighter.gameObject.transform.position = tile.GetComponent<Tile>().transform.localPosition;
			Material newMat = null;
			if(tile.GetComponent<Tile>().isEmpty()){
				newMat = Resources.Load("Materials/TileHighlightPossible", typeof(Material)) as Material;
			}
			else{
				newMat = Resources.Load("Materials/TileHighlightImpossible", typeof(Material)) as Material;	
			}
			_tileHighlighter.gameObject.renderer.material = newMat;
			_tileHighlighter.gameObject.renderer.enabled = true;
		} else{
			_tileHighlighter.gameObject.renderer.enabled = false;
		}
	}
}
