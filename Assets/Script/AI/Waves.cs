using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Waves : MonoBehaviour{
	public WavesState state;
	
	private List<Wave> 	_waves;
	private int 		_index;
	private bool		_autoStartNextWave;
	
	IEnumerator Start (){
		state = WavesState.Initialize;
		while(true){
			switch(state){
			case WavesState.Initialize:
				Initialize();
				break;
			case WavesState.NotStarted:
				break;
			case WavesState.Setup:
				Setup();
				break;
			case WavesState.Idle:
				Wait();
				break;
			case WavesState.Finished:
				Finish();
				break;
			default:
				break;
			}
			yield return 0;
		}
	}
	/*
	public void Begin(){
		_waves = new List<Wave>( GetComponentsInChildren<Wave>());
		_waves.Sort(delegate(Wave x, Wave y) {
			return x.name.CompareTo(y.name);
		});
	}*/
	
	public void Begin(){
  		_waves = new List<Wave>( GetComponentsInChildren<Wave>());
		_waves.Sort(delegate(Wave x, Wave y){
			int _x = int.Parse(x.name.Substring(4));
			int _y = int.Parse(y.name.Substring(4));
			return _x.CompareTo(_y);
		});
 	}
	
	private void Initialize(){
		_index = -1;
		_autoStartNextWave = true;
		Begin();
		state = WavesState.NotStarted;
	}
	
	private void Setup(){
		_index++;
		GameObject[] gos = GameObject.FindGameObjectsWithTag("Respawn");
		if(_index == _waves.Count){
			if(gos.Length == 0)
				state = WavesState.Finished;
			return;	
		}
		else{
			Wave current = _waves[_index];
			current.initUnits();
			if(!_autoStartNextWave){
				state = WavesState.NotStarted;
				_index--;
			}
			else{
				current.started = true;
				_autoStartNextWave = false;
				state = WavesState.Idle;
			}
		}
	}
	
	private void Wait(){
		if(_waves[_index].done){
			GameObject[] gos = GameObject.FindGameObjectsWithTag("Respawn");
			if(gos.Length == 0)
				state = WavesState.Setup;
		}
	}
	
	private void Finish(){
		Messenger<bool>.Broadcast("PlayerVictory", true);
	}
	
	public int WavesCount{
		get {
			return _waves.Count;
		}
	}
	
	public int CurrentWave{
		get {
			return _index + 1;
		}
	}
	
	public bool Rushing {
		get {
			return _autoStartNextWave;	
		}
		set {
			_autoStartNextWave = value;	
		}
	}
}

public enum WavesState{
	Idle,
	Initialize,
	NotStarted,
	Setup,
	Finished
}