using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour, IEventListener {
	private Game				_gameInstance;
	private StructureManager 	_structureManager;
	private PlayerManager 		_playerManager;
	private CameraTools 		_cameraTools;
	private Waves				_scriptWaves;
	
	void Awake(){
		_gameInstance = Game.getInstance();
		_gameInstance.GState = Game.GameState.Loading;
		_playerManager = PlayerManager.instance;
	}

	void Start () {
		_structureManager = GetComponentInChildren<StructureManager>();
		_cameraTools = GameObject.Find("Camera").GetComponent("CameraTools") as CameraTools;
		loadPlayerPrefs();
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.V)){
			IBotStats botStats = new SteamRebel();
			GameObject  newBot = Object.Instantiate(Resources.Load(botStats.Prefab)) as GameObject;
			newBot.name = botStats.Name+Time.realtimeSinceStartup;
			Bot botScript = newBot.GetComponent("Bot") as Bot;
			botScript.botStats = botStats;
			botScript.botBehavior = new SteamRebelBehavior();
			botScript.defendBehavior = new SteamRebelDefense();
			botScript.moveBehavior = new WalkBehavior();
			botScript.setPath(0);
			botScript.init();
		}
		
		if(Input.GetKeyDown(KeyCode.I)){
			IBotStats botStats = new IronBeetle();
			GameObject  newBot = Object.Instantiate(Resources.Load(botStats.Prefab)) as GameObject;
			newBot.name = botStats.Name+Time.realtimeSinceStartup;
			Bot botScript = newBot.GetComponent("Bot") as Bot;
			botScript.botStats = botStats;
			botScript.botBehavior = new IronBeetleBehavior();
			botScript.defendBehavior = new IronBeetleDefense();
			botScript.moveBehavior = new WalkBehavior();
			botScript.setPath(0);
			botScript.init();
		}
		
		if(Input.GetKeyDown(KeyCode.B)){
			IBotStats botStats = new SteamRebel();
			GameObject  newBot = Object.Instantiate(Resources.Load(botStats.Prefab)) as GameObject;
			newBot.name = botStats.Name+Time.realtimeSinceStartup;
			Bot botScript = newBot.GetComponent("Bot") as Bot;
			botScript.botStats = botStats;
			botScript.botBehavior = new SteamRebelBehavior();
			botScript.defendBehavior = new SteamRebelDefense();
			botScript.moveBehavior = new WalkBehavior();
			botScript.setPath(1);
			botScript.init();
		}
		
		if(Input.GetKeyDown(KeyCode.M)){
			IBotStats botStats = new MetalWidow();
			GameObject  newBot = Object.Instantiate(Resources.Load(botStats.Prefab)) as GameObject;
			newBot.name = botStats.Name+Time.realtimeSinceStartup;
			Bot botScript = newBot.GetComponent("Bot") as Bot;
			botScript.botStats = botStats;
			botScript.botBehavior = new MetalWidowBehavior();
			botScript.defendBehavior = new MetalWidowDefense();
			botScript.moveBehavior = new CrawlBehavior();
			botScript.setPath(1);
			botScript.init();
		}
		
		if(Input.GetKeyDown(KeyCode.N)){
			Vector3 mouseOnCoord3 = _cameraTools.ScreenToWorld(Input.mousePosition);
			Vector2 mouseOnCoord2 = Map.getInstance().coordinateToTile(mouseOnCoord3);
			GameObject tile = Map.getInstance().getTile(mouseOnCoord2);
			if(!tile){
				return;
			}
			_structureManager.spawnBuilder(tile.GetComponent<Tile>().transform.localPosition, new NailSpitterBehavior(), new NailSpitterFactory());
		}
		
		if(Input.GetKeyDown(KeyCode.P)){
			_playerManager.Lives += 1;
		}
	}
	
	void loadPlayerPrefs(){
		Camera.main.GetComponent<AudioListener>().enabled = (PlayerPrefs.GetInt("Mute", 0) == 0);
	}

	#region IEventListener implementation
	public bool HandleEvent (IEvent evt){
		return true;
	}
	#endregion
}