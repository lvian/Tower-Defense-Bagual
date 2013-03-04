using UnityEngine;
using System.Collections;

public class LightningTowerAnimation : MonoBehaviour {
	
	private Transform 	_meio;
	private Transform	_cima;
	private Waves		_waveState;
	private Light		_light;
	// Use this for initialization
	void Start () {
		_meio = gameObject.transform.FindChild("meio_lightning_tower_Cube_004") as Transform;
		_cima = gameObject.transform.FindChild("cima_lightning_tower_Cube_002") as Transform;
		_waveState = GameObject.Find("Waves").GetComponent<Waves>();
		_light = _cima.GetChild(0).GetComponent<Light>();
		
	}
	
	// Update is called once per frame
	void Update () {
		//Middle piece animation(always on)
		_meio.transform.Rotate( new Vector3( 0, 1 ,0));
		
		//Top piece animation(depending on waveState)
		if(_waveState.state == WavesState.Idle)
		{
			_light.enabled = true;
			_light.intensity = Random.Range( 0 , 5);
			_cima.transform.Rotate( new Vector3( 0, -3 ,0));
		} else
		{
			_light.enabled = false;	
		}
	
	}
}
