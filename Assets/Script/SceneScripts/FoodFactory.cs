using UnityEngine;
using System.Collections;

public class FoodFactory : LevelScript {
	private bool nailNotificationShowed = false;
	private bool steamNotificationShowed = false;
	
	public override void Awake ()
	{
		base.Awake();
	}

	public override void Start ()
	{
		base.Start();
	}

	public override void Update ()
	{
		if(Time.realtimeSinceStartup > 5 && !nailNotificationShowed){
			Notification n1 = new NotificationWindow(
				new Rect(0f, 0f, 300f, 200f),
				"Oh! And spent the gold carefully...",
				Resources.Load("Icons/NailSpitterIcon") as Texture2D,
				null);
			Notification n2 = new NotificationWindow(
				new Rect(0f, 0f, 300f, 200f),
				"Hey, use the Nail Spitter to defend yourself! You can place it by clicking on any tile highlighted in green.",
				Resources.Load("Icons/NailSpitterIcon") as Texture2D,
				n1);
			Messenger<Notification>.Broadcast("SendNotification", n2);
			nailNotificationShowed = true;
		}
		if(!steamNotificationShowed){
			if(GameObject.FindGameObjectsWithTag("Respawn").Length > 0){
				Notification n = new NotificationWindow(
				new Rect(0f, 0f, 300f, 200f),
				"Here they come! The bastards use to load our furnaces, and now they're are trying to destroy our factory. " +
				"But don't worry, they're slow an always in need of maintenance. You should be able to hold them off nice and easy!",
				null);
				Messenger<Notification>.Broadcast("SendNotification", n);
				steamNotificationShowed = true;
			}
		}
		if(_scriptWaves){
			if(Application.loadedLevel == 1){
				if(_scriptWaves.CurrentWave >= 5)
					_playerManager.UnlockedTowers = "NailSpitter OilSquall";
				else
					_playerManager.UnlockedTowers = "NailSpitter";
			}
		}
		else{
			_scriptWaves = GameObject.Find("Waves").GetComponent<Waves>();
		}
	}
}
