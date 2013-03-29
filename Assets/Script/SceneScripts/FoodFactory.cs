using UnityEngine;
using System.Collections;

public class FoodFactory : LevelScript {
	private bool nailNotificationShowed = false;
	private bool oilNotificationShowed = false;
	private bool steamNotificationShowed = false;
	private bool widowNotificationShowed = false;
	
	public override void Awake ()
	{
		base.Awake();
	}

	public override void Start ()
	{
		_playerManager.UnlockedTowers = "NailSpitter";
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
				"Here they come! We call them the \"Steam Rebels\", these bastards used to load our furnaces, now they're are trying to destroy our factory. " +
				"But don't worry, they're slow an always in need of maintenance. You should be able to hold them off nice and easy!",
				null);
				Messenger<Notification>.Broadcast("SendNotification", n);
				steamNotificationShowed = true;
			}
		}
		if(_scriptWaves){
			if(Application.loadedLevel == 1){
				if(_scriptWaves.CurrentWave == 5)
				{
					_playerManager.UnlockedTowers = "NailSpitter OilSquall";
					if(!oilNotificationShowed){
						Notification n = new NotificationWindow(
							new Rect(0f, 0f, 300f, 200f),
							"Hey there, good news, I just finished the last ajusts on the new tower." +
							"I called it the \"Oil Squall\"!! What? Didn't liked? Sorry, no time to change it now." +
							"This tower wont attack, but the oil it spreads around will slow Sir Misit robots, make sure to have one at choke points!",
							Resources.Load("Icons/oilSquallIcon") as Texture2D,
							null);
							Messenger<Notification>.Broadcast("SendNotification", n);
						oilNotificationShowed = true;
					}
					
				}
				if(_scriptWaves.CurrentWave == 6)
				{
					if(!widowNotificationShowed){
						Notification n = new NotificationWindow(
							new Rect(0f, 0f, 300f, 200f),
							"Hey there, bad news, I just got warned that Sir Misit is sending a new type of robots against us." +
							"The \"Black Widow\", as he called, were once used as pipe cleaners, deliverers and even for surveilance." +
							"That robot frame is weaker than the steam rebel, but faster, also it can lay eggs that spawn smaller spiders.",
							null);
							Messenger<Notification>.Broadcast("SendNotification", n);
							widowNotificationShowed = true;
					}
					
				}
				
			}
		}
		else{
			_scriptWaves = GameObject.Find("Waves").GetComponent<Waves>();
		}
	}
}
