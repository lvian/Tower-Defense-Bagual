using UnityEngine;
using System.Collections;

public class Game{
	public enum GameState{
		Init,
		Loading,
		Tutorial,
		Regular,
		Build,
		Menu,
		Pause
	}
	
	private GameState gState;
	static private Game instance;
	
	private Game(){
		gState = GameState.Init;
	}
	
	public bool isPaused(){
		return ((gState == GameState.Regular || gState == GameState.Build)? false : true);
	}
		
	static public Game getInstance(){
		if(instance == null){
			instance = new Game();
		}
		return instance;
	}
	
	public GameState GState {
		get {
			return this.gState;
		}
		set {
			gState = value;
		}
	}
	
		
}
