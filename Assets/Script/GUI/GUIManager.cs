using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIManager : MonoBehaviour {
	
	#region variables daclaration
	private Camera				_camera;
	private CameraTools 		_cameraTools;
	private Game 				_game;
	private Map					_map;
	private AudioListener 		_audio;
	private InterfaceState 		_iState;
	private GameObject			_selectedTile;
	private PlayerManager 		_player;
	private float				_gameSpeed;
	private GameObject			_selectedTower; //vian
	private GameObject			_towerReachObject; //vian
	private LoadScreen 			_ls; //vian
	private bool				_victoryControl;
	
	// hover tooltip
	private string lastHover;
	private float  hoverDelay;
	
	public StructureManager structureManager;
	public GUISkin skin;
	
	//collider
	static private List<Rect> _GUICollider;
	private List<Rect> GUICollider;
	public bool showColliders;
	
	//notifications
	private Queue<Notification> notifications;
	private Notification currentNotification;
	
	public Texture2D		nailSpitterIcon;
	public Texture2D		archTowerIcon;
	public Texture2D		oilSquallIcon;
	public Texture2D		flameTowerIcon;
	
	
	public Material		towerReach;
	#endregion
	
	#region Components's
	//main HUD
	public Texture2D mainHUD;
	public GUISkin sknMuteOff;
	public GUISkin sknMuteOn;
	public GUISkin sknOptionsButton;
	public GUISkin sknWhiteText;
	public GUISkin sknBlackText;
	public Rect lblScore;
	public Rect lblHealth;
	public Rect lblMoney;
	public Rect btnSound;
	public Rect btnOptions;
	
	//wave controller
	public GUISkin sknWaveStopped;
	public GUISkin sknWaveStarted;
	public GUISkin sknWaveRushed;
	public Rect btnWave;
	public Rect lblWaves;
	
	//fast forward
	public Texture2D fastForwardHUD;
	public GUISkin sknFastForwardPlusSkin;
	public GUISkin sknFastForwardMinusSkin;
	public Rect lblGameSpeed;
	public Rect btnNormalizeSpeed;
	public Rect btnFastForwardPlus;
	public Rect btnFastForwardMinus;
	
	//menu
	public GUISkin sknMenu;
	public Texture2D bkgMenu;
	public GUISkin sknResumeGame;
	public Rect btnResumeGame;
	public GUISkin sknRestartLevel;
	public Rect btnRestartLevel;
	public GUISkin sknOptionsMenu;
	public Rect btnOptionsMenu;
	public GUISkin sknQuit;
	public Rect btnQuit;
	
	//tooltip
	public GUISkin sknTooltip;
	
	//Upgrades
	public Texture2D		damageIcon;
	public Texture2D		rangeIcon;
	public Texture2D		attackSpeedIcon;
	
	//tower stats tooltip
	public Texture2D	towerStatsTexture;
	
	//tile menu
	public Rect cldTileMenu;
	public GUISkin sknTileMenu;
	public GUISkin sknBoldText;
	public GUISkin sknItalicText;
	
	#endregion
	
	void Start (){
		_iState = InterfaceState.Load;
		_audio = GameObject.Find("Camera").GetComponent("AudioListener") as AudioListener;
		_camera = Camera.main;
		_cameraTools = GameObject.Find("Camera").GetComponent("CameraTools") as CameraTools;
		_game = Game.getInstance();
		_map  = Map.getInstance();
		_player = PlayerManager.instance;
		Time.timeScale = _gameSpeed = 1f;
		_towerReachObject = GameObject.Find("TowerReach");
		_ls = GameObject.Find("LoadingScreen").GetComponent<LoadScreen>();
		_victoryControl = false;
		
		notifications = new Queue<Notification>();
		
		_GUICollider = new List<Rect>();
		GUICollider  = new List<Rect>();
		initComponents();
		
		Messenger<bool>.AddListener("PlayerVictory",defVictoryDefeat);
		Messenger<Notification>.AddListener("SendNotification", collectNotification);
		Messenger<Notification>.AddListener("RemoveNotification", discardNotification);
		Messenger<Notification>.AddListener("SkipNotification", skipNotifications);
	}
	
	void initComponents(){
		#region MainHUD's components
		lblScore = new Rect(50f, 96f, 50f, 30f);
		lblMoney = new Rect(349f, 88f, 30f, 30f);
		lblHealth = new Rect(134f, 92f, 30f, 20f);
		btnOptions = new Rect(450.5f, 103f, sknOptionsButton.button.normal.background.width, sknOptionsButton.button.normal.background.height);
		btnSound = new Rect(413f, 103f, 16f, 16f);
		#endregion
		
		#region Wave Controller's components
		btnWave = new Rect(237.5f, 59f, 26f, 33f);
		lblWaves = new Rect(225f, 101f, 50f, 30f);
		#endregion
		
		#region Fast Forward HUD's components
		lblGameSpeed = new Rect( 56f, 30f , 40f , 20f);
		btnNormalizeSpeed = new Rect(82f, 9f, 0f, 0f);
		btnFastForwardPlus = new Rect(82f, 15f, sknFastForwardPlusSkin.button.normal.background.width, sknFastForwardPlusSkin.button.normal.background.height);
		btnFastForwardMinus = new Rect(14f, 15f, sknFastForwardMinusSkin.button.normal.background.width, sknFastForwardMinusSkin.button.normal.background.height);
		#endregion
		
		#region Menu's components
		btnResumeGame = new Rect(202f, 176.5f, 104f, 15f);
		btnRestartLevel = new Rect(208f, 227f, 92f, 15f);
		btnOptionsMenu = new Rect(226f, 279f, 55f, 15f);
		btnQuit = new Rect(188f, 329f, 130f, 17f);
		#endregion
		
	}
	
	void Update(){
		if(_game.GState == Game.GameState.Regular){
			if(_iState == InterfaceState.Load){
				_iState = InterfaceState.Regular;	
			}
			if(Input.GetMouseButton(0)){
				if(!isHitingInterface()){
					_selectedTile = _map.getTile(_map.coordinateToTile(_cameraTools.ScreenToWorld(Input.mousePosition)));
					if(_selectedTile != null){
						if(_selectedTile.GetComponent<Tile>().slot && _selectedTile.GetComponent<Tile>().isEmpty()){
							_game.GState = Game.GameState.Build;
							_iState = InterfaceState.TileSelected;
						} else if (_selectedTile.GetComponent<Tile>().tower){ //vian
							_selectedTower = _selectedTile.GetComponent<Tile>().tower;
							_game.GState = Game.GameState.Build;
							_iState = InterfaceState.TowerSelected;
						}
					}
				}
			}
			
			if(Input.GetKeyDown(KeyCode.Escape)){
				_game.GState = Game.GameState.Menu;
				_iState = InterfaceState.Menu;
			}
		}
		
		if(_game.GState == Game.GameState.Build){
			
		
			if(Input.GetMouseButton(0)){
				if(!isHitingInterface()){
					_selectedTile = _map.getTile(_map.coordinateToTile(_cameraTools.ScreenToWorld(Input.mousePosition)));
					if(_selectedTile != null){
						if(_selectedTile.GetComponent<Tile>().slot && _selectedTile.GetComponent<Tile>().isEmpty()){
							_game.GState = Game.GameState.Build;
							_iState = InterfaceState.TileSelected;
						} else if (_selectedTile.GetComponent<Tile>().tower){ //vian
							_selectedTower = _selectedTile.GetComponent<Tile>().tower;
							_game.GState = Game.GameState.Build;
							_iState = InterfaceState.TowerSelected;
						}
					}
				}
			}
			if(Input.GetMouseButtonUp(1)){
				_game.GState = Game.GameState.Regular;
				_iState = InterfaceState.Regular;
				_towerReachObject.renderer.enabled = false;
			}
			if(Input.GetKeyDown(KeyCode.Escape)){
				_game.GState = Game.GameState.Regular;
				_iState = InterfaceState.Regular;
				_towerReachObject.renderer.enabled = false;
			}
		}
		
		if(_game.GState == Game.GameState.Menu){
			if(Input.GetKeyDown(KeyCode.Escape)){
				_game.GState = Game.GameState.Regular;
				_iState = InterfaceState.Regular;
			}	
		}
	}
 
 	void OnGUI(){
		GUICollider.Clear();
		
		GUI.skin = skin;
		switch(_iState){
		case InterfaceState.Regular:
			DrawGameGUI();
			break;
		case InterfaceState.TileSelected:
			DrawTileMenu();
			break;
		case InterfaceState.Victory:
			DrawVictoryScreen();
			break;
		case InterfaceState.Defeat:
			DrawDefeatScreen();
			break;
		case InterfaceState.Menu:
			DrawMenu();
			break;
		case InterfaceState.OptionsMenu:
			DrawOptions();
			break;
		case InterfaceState.TowerSelected:
			DrawTowerMenu();
			break;
		default:
			break;
		}
		
		if(currentNotification == null && notifications.Count > 0)
			currentNotification = notifications.Dequeue();
		else{
				showNotification(currentNotification);
		}
		
		#region Tooltip
		string text = null;
		switch(GUI.tooltip){
		case "lblHealth":
			text = "Lives: Don't lose them all";
			break;
		case "lblMoney":
			text = "Gold: Spent it carefully";
			break;
		case "lblScore":
			text = "Score: Run for it";
			break;
		case "btnSound":
			if(PlayerPrefs.GetInt("Mute",0) == 1)
				text = "Unmute";
			else
				text = "Mute";
			break;
		case "btnWave":
			text = "Wave";
			break;
		case "btnFastForwardPlus":
			text = "FAST";
			break;
		default:
			text = null;
			break;
		}
		
		if(text != null){
			if( GUI.tooltip == lastHover){
				hoverDelay += Time.deltaTime;	
			}
			else{
				lastHover = GUI.tooltip;
				hoverDelay = 0;
			}
			if(hoverDelay > 1f){
				GUI.skin = sknTooltip;
				GUI.Box(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y - 30f , text.Length * 12, 25f), text);
			}
		}
		#endregion
		
		if(showColliders){
			GUI.skin = null;
			foreach(Rect rect in _GUICollider){
				GUI.Box(rect,"");	
			}
		}
		_GUICollider = GUICollider;
	}
	
	void DrawMenu(){
		
		DrawDummyGUIMenu();
		GUI.skin = skin;
		GUI.BeginGroup(new Rect(Screen.width/2 - bkgMenu.width/2, Screen.height/2 - bkgMenu.height/2, bkgMenu.width, bkgMenu.height ));
		GUI.Box(new Rect(0f, 0f, bkgMenu.width, bkgMenu.height), bkgMenu);
		
		GUI.skin = sknResumeGame;
		if(GUI.Button(btnResumeGame,new GUIContent("", "btnResumeGame"))){
			_iState = InterfaceState.Regular;
			_game.GState = Game.GameState.Regular;
		}
		
		GUI.skin = sknRestartLevel;
		if(GUI.Button(btnRestartLevel, new GUIContent("", "btnRestartLevel"))){
			_game.GState = Game.GameState.Loading;
			_ls.nextLevel = Application.loadedLevel;
			_ls.gateClosed();	
		}
		
		GUI.skin = sknOptionsMenu;
		if(GUI.Button(btnOptionsMenu, new GUIContent("", "btnOptionsMenu"))){
			_iState = InterfaceState.OptionsMenu;
		}
		
		GUI.skin = sknQuit;
		if(GUI.Button(btnQuit, new GUIContent("", "btnQuit"))){
			_game.GState = Game.GameState.Loading;
			_ls.nextLevel = 0;
			_ls.gateClosed();
		}
		GUI.EndGroup();
	}
	
	void DrawOptions(){
		GUI.BeginGroup(new Rect(Screen.width /2 -120, Screen.height/2 -85, 200, 170));
		
		GUI.Box(new Rect(0,0,200,170), "Options");
		
		// Mute option
		string[] muteOptions = new string[] {"ON", "OFF"};
		int muteOption  = PlayerPrefs.GetInt("Mute", 0);
		if( ++muteOption >= muteOptions.Length)
				muteOption = 0;
		GUI.Label(new Rect( 10 , 30 , 50 , 30), "Mute");
		if(GUI.Button(new Rect( 70 , 30 , 100 , 30), muteOptions[muteOption]))
			PlayerPrefs.SetInt("Mute", muteOption);
		
		GUI.Label(new Rect(10, 65, 80, 30), "Volume");
		float audioVolume = PlayerPrefs.GetFloat("AudioVolume",100.0f);
		audioVolume = GUI.HorizontalSlider(new Rect(70, 70, 100, 30), audioVolume, 0.0f, 100.0f);
		PlayerPrefs.SetFloat("AudioVolume", audioVolume);
		
		// back button
		if(GUI.Button(new Rect( 10 , 130 , 180 , 30), "< Go Back"))
			_iState = InterfaceState.Menu;
		
		
		GUI.EndGroup();
	}
	
	void DrawGameGUI(){
		#region MainHUD player info
		GUICollider.Add(new Rect(140f, 415f, 510f, 100f));
		GUI.BeginGroup(new Rect((Screen.width - mainHUD.width)/2f, Screen.height - (mainHUD.height), (float)mainHUD.width, (float)mainHUD.height));
		GUI.Box(new Rect(0,0,mainHUD.width,mainHUD.height), new GUIContent(mainHUD, ""));
		GUI.skin = sknWhiteText;
		GUI.Label(lblHealth, new GUIContent("" + _player.Lives, "lblHealth"));
		GUI.Label(lblMoney, new GUIContent("" + _player.Gold, "lblMoney"));
		GUI.skin = sknBlackText;
		GUI.Label(lblScore, new GUIContent("" + _player.Score, "lblScore"));
		
		//mute button
		bool muteOption = (PlayerPrefs.GetInt("Mute",0) == 1);
		if(muteOption){
			GUI.skin = sknMuteOn;	
		}
		else{
			GUI.skin = sknMuteOff;	
		}
		if(GUI.Button(btnSound, new GUIContent("","btnSound"))){
			muteOption = !muteOption;
			PlayerPrefs.SetInt("Mute", muteOption ? 1 : 0);
			_audio.enabled = !muteOption;
		}
		
		
		//options button
		GUI.skin = sknOptionsButton;
		if(GUI.Button(btnOptions, "")){
			_game.GState = Game.GameState.Menu;
			_iState = InterfaceState.Menu;
		}
		#endregion
		
		#region Wave Controller
		Waves scriptWaves = GameObject.Find("Waves").GetComponent<Waves>();
		WavesState waveState = scriptWaves.state;
		if(waveState == WavesState.NotStarted){
			GUI.skin = sknWaveStopped;
			if(GUI.Button(btnWave, new GUIContent("","btnWave"))){
				scriptWaves.Rushing = true;
				scriptWaves.state = WavesState.Setup;
			}
		}
		if(waveState == WavesState.Idle){
			if(!scriptWaves.Rushing){
				GUI.skin = sknWaveStarted;
				if(GUI.Button(btnWave, new GUIContent("","btnWave"))){
					scriptWaves.Rushing = true;
				}
			}
			else{
				GUI.skin =  sknWaveRushed;
				if(GUI.Button(btnWave, new GUIContent("","btnWave"))){
					scriptWaves.Rushing = false;
				}	
			}
		}
		GUI.skin = sknBlackText;
		GUI.Label(lblWaves, scriptWaves.CurrentWave + "/" + scriptWaves.WavesCount);
		GUI.EndGroup();
		#endregion
		
		#region Fast Forward HUD
		GUI.skin = skin;
		GUICollider.Add(new Rect(350f, 10f, 100f, 60f));
		GUI.BeginGroup(new Rect((Screen.width - fastForwardHUD.width)/2f, 0, fastForwardHUD.width , fastForwardHUD.height));
		GUI.Box(new Rect(0,0,fastForwardHUD.width, fastForwardHUD.height),fastForwardHUD);
		GUI.Label(lblGameSpeed, _gameSpeed+"x");
		
		GUI.skin = sknFastForwardMinusSkin;
		if(GUI.Button(btnFastForwardMinus, new GUIContent("","btnFastForwardMinus"))){
			if(_gameSpeed > 0)
			{
				controlGameSpeed(-1);
			} else
			{
				Messenger<string>.Broadcast("Notification", "Game is Paused!" );	
			}
		}
		
		GUI.skin = sknFastForwardPlusSkin;
		if(GUI.Button(btnFastForwardPlus, new GUIContent("","btnFastForwardPlus")))
		{
			if(_gameSpeed < 3)
			{
				controlGameSpeed(1);
			} else
			{
				Messenger<string>.Broadcast("Notification", "At maximum speed!" );	
			}
		}
		GUI.EndGroup();
		#endregion
	}
	
	void DrawDummyGUI(){
		#region MainHUD player info
		GUICollider.Add(new Rect(140f, 415f, 510f, 100f));
		GUI.BeginGroup(new Rect((Screen.width - mainHUD.width)/2f, Screen.height - (mainHUD.height), (float)mainHUD.width, (float)mainHUD.height));
		GUI.Box(new Rect(0,0,mainHUD.width,mainHUD.height),mainHUD);
		GUI.skin = sknWhiteText;
		GUI.Label(lblHealth, "" + _player.Lives);
		GUI.Label(lblMoney, "" + _player.Gold);
		GUI.skin = sknBlackText;
		GUI.Label(lblScore, "" + _player.Score);
		
		//mute button
		bool muteOption = (PlayerPrefs.GetInt("Mute",0) == 1);
		if(muteOption){
			GUI.skin = sknMuteOn;	
		}
		else{
			GUI.skin = sknMuteOff;	
		}
		GUI.Box(btnSound, "");
		
		
		//options button
		GUI.skin = sknOptionsButton;
		GUI.Box(btnOptions, "");
		#endregion
		
		#region Wave Controller
		Waves scriptWaves = GameObject.Find("Waves").GetComponent<Waves>();
		WavesState waveState = scriptWaves.state;
		if(waveState == WavesState.NotStarted){
			GUI.skin = sknWaveStopped;
			GUI.Box(btnWave, new GUIContent("","btnWave"));
		}
		if(waveState == WavesState.Idle){
			if(!scriptWaves.Rushing){
				GUI.skin = sknWaveStarted;
				GUI.Box(btnWave, new GUIContent("","btnWave"));
			}
			else{
				GUI.skin =  sknWaveRushed;
				GUI.Box(btnWave, new GUIContent("","btnWave"));
			}
		}
		GUI.skin = sknBlackText;
		GUI.Label(lblWaves, scriptWaves.CurrentWave + "/" + scriptWaves.WavesCount);
		GUI.EndGroup();
		#endregion
		
		#region Fast Forward HUD
		GUI.skin = skin;
		GUICollider.Add(new Rect(350f, 10f, 100f, 60f));
		GUI.BeginGroup(new Rect((Screen.width - fastForwardHUD.width)/2f, 0, fastForwardHUD.width , fastForwardHUD.height));
		GUI.Box(new Rect(0,0,fastForwardHUD.width, fastForwardHUD.height),fastForwardHUD);
		GUI.Label(lblGameSpeed, _gameSpeed+"x");
		
		GUI.skin = sknFastForwardMinusSkin;
		GUI.Box(btnFastForwardMinus, "");
		
		GUI.skin = sknFastForwardPlusSkin;
		GUI.Box(btnFastForwardPlus, "");
		GUI.EndGroup();
		#endregion
	}
	
	void DrawDummyGUIMenu(){
		
		#region MainHUD player info
		GUICollider.Add(new Rect(140f, 415f, 510f, 100f));
		GUI.BeginGroup(new Rect((Screen.width - mainHUD.width)/2f, Screen.height - (mainHUD.height), (float)mainHUD.width, (float)mainHUD.height));
		GUI.Box(new Rect(0,0,mainHUD.width,mainHUD.height),mainHUD);
		GUI.skin = sknWhiteText;
		GUI.Label(lblHealth, "" + _player.Lives);
		GUI.Label(lblMoney, "" + _player.Gold);
		GUI.skin = sknBlackText;
		GUI.Label(lblScore, "" + _player.Score);
		
		//mute button
		bool muteOption = (PlayerPrefs.GetInt("Mute",0) == 1);
		if(muteOption){
			GUI.skin = sknMuteOn;	
		}
		else{
			GUI.skin = sknMuteOff;	
		}
		GUI.Box(btnSound, "");
		
		
		//options button
		GUI.skin = sknOptionsButton;
		if(GUI.Button(btnOptions, "")){
			_game.GState = Game.GameState.Regular;
			_iState = InterfaceState.Regular;
		}
		#endregion
		
		#region Wave Controller
		Waves scriptWaves = GameObject.Find("Waves").GetComponent<Waves>();
		WavesState waveState = scriptWaves.state;
		if(waveState == WavesState.NotStarted){
			GUI.skin = sknWaveStopped;
			GUI.Box(btnWave, new GUIContent("","btnWave"));
		}
		if(waveState == WavesState.Idle){
			if(!scriptWaves.Rushing){
				GUI.skin = sknWaveStarted;
				GUI.Box(btnWave, new GUIContent("","btnWave"));
			}
			else{
				GUI.skin =  sknWaveRushed;
				GUI.Box(btnWave, new GUIContent("","btnWave"));
			}
		}
		GUI.skin = sknBlackText;
		GUI.Label(lblWaves, scriptWaves.CurrentWave + "/" + scriptWaves.WavesCount);
		GUI.EndGroup();
		#endregion
		
		#region Fast Forward HUD
		GUI.skin = skin;
		GUICollider.Add(new Rect(350f, 10f, 100f, 60f));
		GUI.BeginGroup(new Rect((Screen.width - fastForwardHUD.width)/2f, 0, fastForwardHUD.width , fastForwardHUD.height));
		GUI.Box(new Rect(0,0,fastForwardHUD.width, fastForwardHUD.height),fastForwardHUD);
		GUI.Label(lblGameSpeed, _gameSpeed+"x");
		
		GUI.skin = sknFastForwardMinusSkin;
		GUI.Box(btnFastForwardMinus, "");
		
		GUI.skin = sknFastForwardPlusSkin;
		GUI.Box(btnFastForwardPlus, "");
		GUI.EndGroup();
		#endregion
	}
	
	#region TileMenu
	void DrawTileMenu(){
		DrawDummyGUI();
		GUI.skin = sknTileMenu;
		if(!_selectedTile){
			_game.GState = Game.GameState.Regular;
			_iState = InterfaceState.Regular;
			return;
		}
		Vector3 pos = _camera.WorldToViewportPoint(_selectedTile.transform.localPosition);
		Rect buttonPos1 = new Rect((pos.x * _camera.GetScreenWidth()) -25, (_camera.GetScreenHeight() - (pos.y * _camera.GetScreenHeight()) -25), 50, 50);
		Rect buttonPos2 = new Rect((pos.x * _camera.GetScreenWidth()) +25, (_camera.GetScreenHeight() - (pos.y * _camera.GetScreenHeight()) -25), 50, 50);
		Rect buttonPos3 = new Rect((pos.x * _camera.GetScreenWidth()) +75, (_camera.GetScreenHeight() - (pos.y * _camera.GetScreenHeight()) -25), 50, 50);
		Rect buttonPos4 = new Rect((pos.x * _camera.GetScreenWidth()) +125, (_camera.GetScreenHeight() - (pos.y * _camera.GetScreenHeight()) -25), 50, 50);
		GUICollider.Add(new Rect(buttonPos1.x, buttonPos1.y, buttonPos4.xMax - buttonPos1.xMin, 50f));
		
		GUI.skin = sknTileMenu;
		if(_player.UnlockedTowers.Contains("NailSpitter")){
			if(GUI.Button(buttonPos1, nailSpitterIcon)){
				spawnUnit("NailSpitter", _selectedTile);
			}
			if(buttonPos1.Contains(Event.current.mousePosition)){
				drawTowerInfo(buttonPos1, "NailSpitter");
			}
		}
		
		GUI.skin = sknTileMenu;
		if(_player.UnlockedTowers.Contains("OilSquall")){
			if(GUI.Button(buttonPos2, oilSquallIcon)){
				spawnUnit("OilSquall", _selectedTile);	
			}
			if(buttonPos2.Contains(Event.current.mousePosition)){
				drawTowerInfo(buttonPos2, "OilSquall");
			}
		}
		
		GUI.skin = sknTileMenu;
		if(_player.UnlockedTowers.Contains("LightningTower")){
			if(GUI.Button(buttonPos3, archTowerIcon)){
				spawnUnit("LightningTower", _selectedTile);
			}
			if(buttonPos3.Contains(Event.current.mousePosition)){
				drawTowerInfo(buttonPos3, "LightningTower");
			}
		}	
		
		GUI.skin = sknTileMenu;
		if(_player.UnlockedTowers.Contains("FlameTower")){
			if(GUI.Button(buttonPos4, flameTowerIcon)){
				Debug.Log("FlameTower");
				spawnUnit("FlameTower", _selectedTile);
			}
			if(buttonPos4.Contains(Event.current.mousePosition)){
				drawTowerInfo(buttonPos4, "FlameTower");
			}
		}	
	}
	#endregion
	
	#region TowerMenu
	void DrawTowerMenu(){
		DrawDummyGUI();
		GUI.skin = sknTileMenu;
		if(!_selectedTower){
			_game.GState = Game.GameState.Regular;
			_iState = InterfaceState.Regular;
			return;
		}
		Vector3 pos = _camera.WorldToViewportPoint(_selectedTile.transform.localPosition);
		Rect buttonPos1 = new Rect((pos.x * _camera.GetScreenWidth()) -25, (_camera.GetScreenHeight() - (pos.y * _camera.GetScreenHeight()) - 25 ), 50, 50);
		
		
		AttackingUnit attackingUnit = getUnit(_selectedTower.name.Replace(" ", ""));
		TowerFactory towerFactory = attackingUnit.TowerFactory;
		Tower tower = towerFactory.GetTower();
		_towerReachObject.renderer.enabled = true;
			
		_towerReachObject.transform.position = _selectedTower.transform.localPosition;
		_towerReachObject.transform.localScale = new Vector3(tower.reach * 2 , 0 , tower.reach * 2);
		
		if(GUI.Button(buttonPos1, damageIcon)){
			structureManager.removeStructure(_selectedTower);
			_game.GState = Game.GameState.Regular;
			_iState = InterfaceState.Regular;
			_selectedTower = null;
			_towerReachObject.renderer.enabled = false;
		}
	}
	
	void drawTowerInfo (Rect buttonPos, string unit)
	{
		if(_selectedTile)
		{
			AttackingUnit attackingUnit = getUnit(unit);
			TowerFactory towerFactory = attackingUnit.TowerFactory;
			Tower tower = towerFactory.GetTower();
			_towerReachObject.renderer.enabled = true;
			
			_towerReachObject.transform.position = _selectedTile.transform.localPosition;
			_towerReachObject.transform.localScale = new Vector3(tower.reach * 2 , 0 , tower.reach * 2);
			
			GUI.skin = sknTileMenu;
			GUI.BeginGroup(new Rect(0f , _camera.GetScreenHeight() / 2 - 72.5f, 250f , 135f));
			GUI.Box(new Rect(0,0,250,135), "");
			GUI.skin = sknBoldText;
			GUI.Label(new Rect(10, 20, 200, 25), tower.Name);
			GUI.skin = sknItalicText;
			GUI.Label(new Rect(10, 33, 200, 55), tower.Description);
			GUI.skin = null;
			GUI.Label(new Rect(10, 70, 200, 25), "Damage");
			GUI.skin = sknBoldText;
			GUI.Label(new Rect(100, 70, 200, 25), "" + tower.damage);
			
			GUI.skin = null;
			GUI.Label(new Rect(10, 85, 200, 25), "Reach");
			GUI.skin = sknBoldText;
			GUI.Label(new Rect(100, 85, 200, 25), "" + tower.reach);	
			
			GUI.skin = null;
			GUI.Label(new Rect(10, 100, 200, 25), "Attack Speed");
			GUI.skin = sknBoldText;
			GUI.Label(new Rect(100, 100, 200, 25), "" + tower.attackSpeed);
			
			GUI.skin = null;
			GUI.Label(new Rect(10, 115, 200, 25), "Price");
			GUI.skin = sknBoldText;
			GUI.Label(new Rect(100, 115, 200, 25), "" + tower.price);

			GUI.EndGroup();
		}
		
	}
	#endregion
	
	#region VictoryDefeat
	void defVictoryDefeat(bool victory){
		if(!_victoryControl)
		{
			if(victory){
				_game.GState = Game.GameState.Pause;
				_iState = InterfaceState.Victory;
				_victoryControl = true;
			}
			else{
				_game.GState = Game.GameState.Pause;
				_iState = InterfaceState.Defeat;	
				_victoryControl = true;
			}
		}
	}
	
	void DrawVictoryScreen(){
		GUI.BeginGroup(new Rect(Screen.width/2 -100 ,Screen.height/2 -85, 200,170));
		
		
		if(Application.loadedLevel == 1)
		{
			GUI.Box(new Rect(0,0,200,170),"VICTORY");
			GUI.Box(new Rect(10,30,180,30),"Score: " + _player.Score);
			if(GUI.Button(new Rect(10,65,180,30),"Next Level")){
				_game.GState = Game.GameState.Loading;
				_ls.nextLevel = Application.loadedLevel + 1;
				_ls.gateClosed();	
			}
			if(GUI.Button(new Rect(10,100,180,30),"Restart Level")){
				_game.GState = Game.GameState.Loading;
				_ls.nextLevel = Application.loadedLevel;
				_ls.gateClosed();	
			}
			if(GUI.Button(new Rect(10,135,180,30),"Main Menu")){
				_game.GState = Game.GameState.Loading;
				_ls.nextLevel = 0;
				_ls.gateClosed();	
				
			}
		}
		else
		{
			GUI.Box(new Rect(0,0,200,170),"VICTORY");
			GUI.Box(new Rect(10,30,180,30),"Score: " + _player.Score);
			GUI.Label(new Rect(10,65,180,30),"Congratulations!");
			GUI.Label(new Rect(10,100,180,30),"Alpha 0.2 Complete");
			
			if(GUI.Button(new Rect(10,135,180,30),"Main Menu")){
				_game.GState = Game.GameState.Loading;
				_ls.nextLevel = 0;
				_ls.gateClosed();	
				
			}
			
		}
		
		GUI.EndGroup();
	}
	
	void DrawDefeatScreen(){
		GUI.BeginGroup(new Rect(Screen.width/2 -100 ,Screen.height/2 -85, 200,170));
		GUI.Box(new Rect(0,0,200,170),"DEFEAT");
		GUI.Box(new Rect(10,30,180,30),"Score: " + _player.Score);
		
		if(GUI.Button(new Rect(10,100,180,30),"Restart Level")){
			_game.GState = Game.GameState.Loading;
			_ls.nextLevel = Application.loadedLevel;
			_ls.gateClosed();
		}
		if(GUI.Button(new Rect(10,135,180,30),"Main Menu")){
			_game.GState = Game.GameState.Loading;
			_ls.nextLevel = 0;
			_ls.gateClosed();		
		}
		
		GUI.EndGroup();
	}
	#endregion
	
	#region Unit Spawn
	void spawnUnit(string unit, GameObject tile){
		AttackingUnit attackingUnit = getUnit(unit);
		TowerFactory towerFactory = attackingUnit.TowerFactory;
		Tower tower = towerFactory.GetTower();
		if(_player.Gold >= tower.price)
		{
			structureManager.spawnBuilder(tile.GetComponent<Tile>().transform.localPosition, attackingUnit.StructureBehavior, attackingUnit.TowerFactory);
			_player.Gold -= tower.price;
			_game.GState = Game.GameState.Regular;
			_iState = InterfaceState.Regular;
			_selectedTile = null;
		}
		else{
			_game.GState = Game.GameState.Regular;
			_iState = InterfaceState.Regular;
			_selectedTile = null;
			Messenger<string>.Broadcast("Notification", "Not enough gold!" );
		}
		_towerReachObject.renderer.enabled = false;
	}
	
	AttackingUnit getUnit(string unit){
		switch(unit){
		case "NailSpitter":
			return new AttackingUnit(new NailSpitterBehavior(), new NailSpitterFactory());
		case "LightningTower":
			return new AttackingUnit(new LightningTowerBehavior(), new LightningTowerFactory());
		case "OilSquall":
			return new AttackingUnit(new OilSquallBehavior(), new OilSquallFactory());
		case "FlameTower":
			return new AttackingUnit(new FlameTowerBehavior(), new FlameTowerFactory());
		default:
			return null;
		}
	}
	#endregion
	
	#region Speed Control
	void controlGameSpeed(int n)
	{
		_gameSpeed += n;
		
		switch((int)_gameSpeed){
			case 0 :
				_game.GState = Game.GameState.Pause;
				break;
			case 1 :
				_game.GState = Game.GameState.Regular;
				Time.timeScale = _gameSpeed;
				break;
			case 2 :
				Time.timeScale = _gameSpeed;
				break;
			case 3 :
				Time.timeScale = _gameSpeed;
				break;
			case 4 :
				Time.timeScale = _gameSpeed;
				break;
			case 5 :
				Time.timeScale = _gameSpeed;
				break;
		}
	}	
		
	void IncreaseGameSpeed(){
		if(_gameSpeed <= 4)
		{
			_gameSpeed++;	
		}
	}
	
	void DecreaseGameSpeed(){
		if(_gameSpeed >= 1)
		{
			_gameSpeed--;	
		}
	}
	#endregion
	
	public static bool isHitingInterface(){
		bool hit = false;
		Vector2 mousePosition = Input.mousePosition;
		mousePosition.y = Screen.height - mousePosition.y;
		foreach(Rect r in _GUICollider){
			hit = hit || r.Contains(mousePosition);
		}
		return hit;
	}
	
	#region Notifications
	void collectNotification(Notification n){
		notifications.Enqueue(n);
		while(n.nextNotification != null){
			n = n.nextNotification;
			notifications.Enqueue(n);
		}
	}
	
	void discardNotification(Notification n){
		currentNotification = null;
	}
	
	void skipNotifications(Notification n){
		while(n.nextNotification != null){
			n = notifications.Dequeue();
		}
		discardNotification(n);
	}
	
	void showNotification(Notification n){
		if(_game.GState != Game.GameState.Loading) if(n != null) GUICollider.Add(n.draw());
	}
	#endregion
}

public enum InterfaceState{
	Load,
	Regular,
	Menu,
	OptionsMenu,
	TileSelected,
	TowerSelected,
	Victory,
	Defeat
}
