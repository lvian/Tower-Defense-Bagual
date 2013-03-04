using UnityEngine;
using System;

public class clock : MonoBehaviour {
	
	private GameObject 		_ponteiro;
	private float 			_angle;
	private PlayerManager 	_player;
	// Use this for initialization
	void Start () {
		_ponteiro = GameObject.Find("ponteiro");
		_angle = 0f;
		_player = PlayerManager.instance;
	}
	
	// Update is called once per frame
	void Update () {
		if(_player.Lives > 0)
		{
			_angle = _angle + (Time.deltaTime * 1000) / _player.Lives ;
			_ponteiro.transform.localRotation = Quaternion.Euler( 36f, 0f, _angle );
		}
	}
}
