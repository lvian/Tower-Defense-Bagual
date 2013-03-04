using UnityEngine;
using System.Collections;

public class PlayerManager {
	private static PlayerManager 	_instance;
	
	private int 			_gold;
	private int 			_lives;
	private int 			_score;
	private string			_unlockedTowers;
	private ParticleSystem	_damageAnimation;

	private PlayerManager(){
		Messenger<GameObject>.AddListener("BotLeft", OnBotLeft);
		Messenger<GameObject>.AddListener("BotDied", OnBotDied);
		_gold  = 400;
		_lives = 20;
		_score = 0;
	}
	
	private void OnBotLeft(GameObject gameObject){
		_damageAnimation = GameObject.Find("Heart_Damage").GetComponent<ParticleSystem>() as ParticleSystem;
		_damageAnimation.Play();
		Bot bot = gameObject.GetComponent<Bot>();
		Lives -= (int)bot.getAttributeValue(BotAttributeName.Damage);
		if( Lives <= 0){
			Messenger<bool>.Broadcast("PlayerVictory", false);
		}
	}
	
	private void OnBotDied(GameObject bot){
		Bot b = (Bot)bot.GetComponent<Bot>();
		_gold += (int)b.getAttributeValue(BotAttributeName.Bounty);
		_score+= (int)b.getAttributeValue(BotAttributeName.Score);
	}
	
	public static PlayerManager instance{
		get{
			if(_instance == null){
				_instance = new PlayerManager();
			}
			return _instance;
		}
	}
	
	public int Gold {
		get {
			return this._gold;
		}
		set {
			_gold = value;
		}
	}

	public int Lives {
		get {
			return this._lives;
		}
		set {
			_lives = value;
		}
	}

	public int Score {
		get {
			return this._score;
		}
		set {
			_score = value;
		}
	}

	public string UnlockedTowers {
		get {
			return this._unlockedTowers;
		}
		set {
			_unlockedTowers = value;
		}
	}
}
