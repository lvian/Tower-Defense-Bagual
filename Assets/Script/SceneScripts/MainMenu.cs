using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public enum State{
		Setup,
		Main,
		Options,
		Credits,
		Ready
	}
	
	public Texture2D	background;
	public Texture2D	logo;
	private State		_state;
	private bool		_ready;
	private Game		_game;
	private LoadScreen	_ls;
	
	void Start (){
		_state = State.Setup;
		_game = Game.getInstance();
		_game.GState = Game.GameState.Loading;
		_ls = GameObject.Find("LoadingScreen").GetComponent<LoadScreen>();
	}
	
	void Update (){
		if(Input.GetKeyDown("escape")){
			if(_state == State.Options)
				_state = State.Main;
			if(_state == State.Credits)
				_state = State.Main;
		}
		if(Time.realtimeSinceStartup > 0f)
			_ready = true;
	}

	void OnGUI(){
		if(_state == State.Setup){
			setup();
			_state = State.Main;
		}
			
		if(_state == State.Main)
			main();
			
		if(_state == State.Options)
			options();
			
		if(_state == State.Credits)
			credits();
			
		if(_state == State.Ready){
			_game.GState = Game.GameState.Loading;
			_ls.nextLevel = Application.loadedLevel + 1;
			_ls.gateClosed();
		}
	}
	void setup(){
		_ready = false;
		_state = State.Main;
		_ls._gateState = GateState.MainMenu;
	}
		
	void main(){
		if(background)
			GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height), background);
		if(logo)
			GUI.DrawTexture(new Rect(Screen.width/2 -75, Screen.height * 0.01f, 150, 150), logo);
		
		GUI.BeginGroup(new Rect(Screen.width /2 -100, Screen.height/2 -67.5f , 200 , 170 ));
		
		//Make a box at the center of t he screen
		GUI.Box(new Rect(0,0,200,170), "Main Menu");
		
		// Play button
		if(_ready){
			if(GUI.Button(new Rect(10, 30, 180, 30), "Play")){
				_state = State.Ready;
			}
		}
		else{
			GUI.Box(new Rect(10, 30, 180, 30), "Play");
		}
			
		//Options button
		if(GUI.Button(new Rect(10, 65, 180, 30), "Options")){
			//AudioSource.PlayClipAtPoint(optionSound, transform.position , 1)
			_state = State.Options;
		}
			
		//Credits button
		if(GUI.Button(new Rect(10, 100, 180, 30), "Credits")){
			//AudioSource.PlayClipAtPoint(creditSound, transform.position , 1)
			_state = State.Credits;
		}
			
		// Feedback button
		if(GUI.Button(new Rect(10, 135, 180, 30), "Feedback")){
			Application.OpenURL("https://www.facebook.com/SturdyDef");
		}
			
		GUI.EndGroup();
	}
	
	void options(){
		if(background)
			GUI.DrawTexture(new Rect(0,0,Screen.width , Screen.height), background);
		if(logo)
			GUI.DrawTexture(new Rect(Screen.width/2 -75, Screen.height * 0.01f, 150, 150), logo);
			
		GUI.BeginGroup(new Rect(Screen.width /2 -150, Screen.height/2 -150 , 300 ,300 ));
		
		GUI.Box(new Rect(0,0,300,300), "Options");
		
		// Dificulty option
	    //dificultyOptions = ["Beginner", "Easy", "Hard" ]
		//dificultyOption  = PlayerPrefs.GetInt("Dificulty", 0);
		//if ++dificultyOption >= dificultyOptions.Count:
		//		dificultyOption = 0
		//GUI.Label(Rect( 10 , 30 , 80 , 30), "Dificulty:")
		//if GUI.Button(Rect( 120 , 25 , 100 , 30), dificultyOptions[dificultyOption] as string):
		//	PlayerPrefs.SetInt("Dificulty", dificultyOption)
		
		// Mute option
		string[] muteOptions = new string[] {"ON", "OFF"};
		int muteOption  = PlayerPrefs.GetInt("Mute", 0);
		if(++muteOption >= muteOptions.Length)
				muteOption = 0;
		GUI.Label(new Rect(10, 30, 50, 30), "Mute");
		if(GUI.Button(new Rect(120, 30, 100, 30), muteOptions[muteOption]))
			PlayerPrefs.SetInt("Mute", muteOption);
		
		//GUI.Label(Rect( 10 , 100 , 80 , 30), "Audio:")
		// Background volume option
		GUI.Label(new Rect(10, 65, 80, 30), "Volume");
		float audioVolume = PlayerPrefs.GetFloat("AudioVolume",100.0f);
		audioVolume = GUI.HorizontalSlider(new Rect(120, 70, 100, 30), audioVolume, 0.0f, 100.0f);
		PlayerPrefs.SetFloat("AudioVolume", audioVolume);
		
		// back button
		if(GUI.Button(new Rect(10, 260, 280, 30), "< Go Back"))
			_state = State.Main;
		
		
		GUI.EndGroup();	
	}
	
	void credits(){
		Debug.Log("CREDITS_B");
		if(background)
			GUI.DrawTexture(new Rect(0,0,Screen.width , Screen.height), background);
		if(logo)
			GUI.DrawTexture(new Rect(Screen.width/2 -75, Screen.height * 0.01F, 150, 150), logo);
		
		GUI.BeginGroup(new Rect(Screen.width /2 -255, Screen.height/2 -50 - 105, 250 , 100 ));
		
		GUI.Box(new Rect(0,0,250,100), "Programmers");
		GUI.Label(new Rect( 10 , 30 , 180 , 30), "Leandro Vian");
		GUI.Label(new Rect( 10 , 65 , 180 , 30), "Carlos Colombo");
		
		GUI.EndGroup();
		
		GUI.BeginGroup(new Rect(Screen.width /2 + 5, Screen.height/2 -50 - 105 , 250 , 100 ));
		
		GUI.Box(new Rect(0,0,250,100), "Art");
		GUI.Label(new Rect( 10 , 30 , 230 , 30), "Juliano Silveira Britto");
		GUI.Label(new Rect( 10 , 65 , 230 , 30), "Pablo Alexandre Silveira Medeiros");
		
		GUI.EndGroup();
		
		GUI.BeginGroup(new Rect(Screen.width /2 -125, Screen.height/2 -50, 250 , 100 ));
		GUI.Box(new Rect(0,0,250,100), "Special Thanks");
		GUI.Label(new Rect( 10 , 30 , 180 , 30), "Johnny Carvalho");
		
		GUI.EndGroup();
		
		if(GUI.Button(new Rect( 50, Screen.height - 50, 30 , 30), "<"))
			_state = State.Main;
		Debug.Log("CREDITS_E");
	}
}
