using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadScreen : MonoBehaviour {
	public Texture2D				background;
	public GUISkin					skin;
	
	private Game _gameInstance;
	public bool _playing;
	public int  _countLoaded;
	public GateState _gateState;
	
	public Texture2D		gateLeft;//vian
	public Texture2D		gateRight;//vian
	private Rect 			rectLeft;
	private Rect 			rectRight;
	private float			offset;
	public int 				nextLevel;
	
	void Start () {
		_gameInstance =  Game.getInstance();
		_playing = false;
		
		rectLeft = new Rect(0,0,Screen.width, Screen.height );
		rectRight = new Rect(0,0,Screen.width, Screen.height );
		offset = 0;
		_gateState = GateState.Idle;

	}
	
	void Update(){
		
		if(_gateState == GateState.Open){
			rectLeft.x -=  offset;	
			rectRight.x += offset;
			offset += .5f;
			if(rectRight.x >= 400)
			{
				_gameInstance.GState = Game.GameState.Regular;
				_gateState = GateState.Idle;
				offset = 0;
			}
		}
		
		if(_gateState == GateState.Closed){
			rectLeft.x  += offset;	
			rectRight.x -= offset;
			offset += .5f;
			if(rectRight.x <= 0)
			{
				//_gameInstance.GState = Game.GameState.Regular;
				_gateState = GateState.Idle;
				offset = 0;
				Application.LoadLevel(nextLevel);
			}
		}
		
		if(_gateState == GateState.MainMenu){
			rectLeft.x -=  offset;	
			rectRight.x += offset;
			offset += .5f;
			if(rectRight.x >= 400)
			{
				_gameInstance.GState = Game.GameState.Regular;
				_gateState = GateState.Idle;
				offset = 0;
			}
		}
	}
	
	void OnGUI(){
		GUI.skin = null;
		if(_gameInstance.GState == Game.GameState.Loading){
			if(background && _gateState == GateState.Closed && Application.loadedLevel == 0)
				GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height), background);
			GUI.DrawTexture(rectLeft, gateLeft, ScaleMode.ScaleAndCrop);
			GUI.DrawTexture(rectRight, gateRight, ScaleMode.ScaleAndCrop);
			if(_gateState == GateState.Idle){
				GUI.BeginGroup(new Rect(Screen.width /2 -100, Screen.height -30 -(Screen.height * .1f), 200 ,30));
				if(GUI.Button(new Rect(0,0,200,30), "Click to play!")){
					_gateState = GateState.Open;
				}
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
	MainMenu,
	Idle
}
