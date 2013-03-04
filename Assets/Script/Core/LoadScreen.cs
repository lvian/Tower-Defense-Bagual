using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadScreen : MonoBehaviour {
	public List<ProceduralMaterial> materials;
	public Texture2D				background;
	public GUISkin					skin;
	
	private Game _gameInstance;
	public bool _ready;
	public bool _playing;
	public int  _countLoaded;
	private GateState _gateState;
	
	public Texture2D		gateLeft;//vian
	public Texture2D		gateRight;//vian
	private Rect rectLeft;
	private Rect rectRight;
	private float	offset;
	public int nextLevel;
	
	void Start () {
		foreach(ProceduralMaterial p in  materials){
			p.RebuildTextures();
		}
		_gameInstance =  Game.getInstance();
		_ready = false;
		_playing = false;
		
		rectLeft = new Rect(0,0,Screen.width , Screen.height );
		rectRight = new Rect(0,0,Screen.width  , Screen.height );
		offset = 0;
		_gateState = GateState.Idle;

	}
	
	void Update(){
		_countLoaded = 0;
		bool loading = true;
		if(_gameInstance.GState == Game.GameState.Loading){
			foreach(ProceduralMaterial p in  materials){
				loading = loading && !p.isProcessing;
				if(!p.isProcessing){
					_countLoaded++;
				}
			}
		}
		else{
			if(_playing && _ready){
				Destroy(this);	
			}
		}
		_ready = loading;
		
		
		if(_gateState == GateState.Open)
		{
			rectLeft.x -= offset;	
			rectRight.x +=offset;
			offset += .5f;
			if(rectRight.x >= 400)
			{
				_gameInstance.GState = Game.GameState.Regular;
				_gateState = GateState.Idle;
				offset = 0;
			}
			
		}
		
		if(_gateState == GateState.Closed)
		{
			rectLeft.x += offset;	
			rectRight.x -=offset;
			offset += .5f;
			if(rectRight.x <= 0)
			{
				//_gameInstance.GState = Game.GameState.Regular;
				_gateState = GateState.Idle;
				offset = 0;
				Application.LoadLevel(nextLevel);
				
			}
			
		}
		
	}
	
	void OnGUI(){
		GUI.skin = null;
		if(_gameInstance.GState == Game.GameState.Loading){
			GUI.DrawTexture(rectLeft, gateLeft, ScaleMode.ScaleAndCrop);
			GUI.DrawTexture(rectRight, gateRight, ScaleMode.ScaleAndCrop);
			if(_ready && _gateState == GateState.Idle){
				GUI.BeginGroup(new Rect(Screen.width /2 -100, Screen.height -30 -(Screen.height * .1f), 200 ,30));
				if(GUI.Button(new Rect(0,0,200,30), "Click to play!")){
					_gateState = GateState.Open;
					//_gameInstance.GState = Game.GameState.Regular;
				}
				GUI.EndGroup();
			}
			if(!_ready)
			{
				GUI.BeginGroup(new Rect(Screen.width /2 -100, Screen.height -30 -(Screen.height * .1f), 200, 30));
				GUI.Box(new Rect(0,0,200,30), "Loading " + Mathf.FloorToInt(((float)_countLoaded / (float)materials.Count) * 100f) + " %");
				GUI.EndGroup();
			}
		}	
	}
	
	public void gateClosed()
	{
		_gateState = GateState.Closed;
	}
	
}


public enum GateState{
	Open,
	Closed,
	Idle
}
