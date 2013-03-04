using UnityEngine;
using System.Collections;

public class gear : MonoBehaviour {
	
	public float 			_lives;
	private PlayerManager	_player;
	private Animation		_anim;
	public GameObject[] 	_gears;
	
	
	// Use this for initialization
	void Start () {
		_player = PlayerManager.instance;
		_lives = _player.Lives;
		
	}
	
	// Update is called once per frame
	void Update () {
		_lives = _player.Lives;
		
		foreach(GameObject gb in _gears)
		{
			_anim = gb.transform.parent.animation; 
			_anim["ModoAnim"].speed = 0.05f * _lives;
		}
				
	}
}
