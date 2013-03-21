using UnityEngine;
using System.Collections;

public class SlautherHouseScript : LevelScript {
	
	public override void Awake (){
		base.Awake();
	}

	public override void Start (){
		base.Start ();
	}
	
	public override void Update(){
		if(_scriptWaves){
			if(Application.loadedLevel == 2){
				if(_scriptWaves.CurrentWave >= 5)
					_playerManager.UnlockedTowers = "NailSpitter OilSquall LightningTower";
				else
					_playerManager.UnlockedTowers = "NailSpitter OilSquall";	
			}
		}
		else{
			_scriptWaves = GameObject.Find("Waves").GetComponent<Waves>();
		}
	}
}
