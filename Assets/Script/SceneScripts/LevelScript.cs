using UnityEngine;
using System.Collections;

public class LevelScript : MonoBehaviour {
	public Game					_gameInstance;
	public StructureManager 	_structureManager;
	public PlayerManager 		_playerManager;
	public CameraTools 			_cameraTools;
	public Waves				_scriptWaves;
	
	public virtual void Awake(){
		_gameInstance = Game.getInstance();
		_playerManager = PlayerManager.instance;
		
		_playerManager.Gold = 50;
		_playerManager.Lives = 20;
		_playerManager.Score = 0;
		_playerManager.UnlockedTowers = "NailSpitter OilSquall LightningTower";
	}
	
	public virtual void Start(){
		_structureManager = GetComponentInChildren<StructureManager>();
		_cameraTools = GameObject.Find("Camera").GetComponent("CameraTools") as CameraTools;
	}
	
	public virtual void Update(){
		
	}
}
