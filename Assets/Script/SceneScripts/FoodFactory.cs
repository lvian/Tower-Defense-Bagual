using UnityEngine;
using System.Collections;

public class FoodFactory : LevelScript {
	private bool appleNotificationShowed = false;
	
	public override void Awake ()
	{
		base.Awake ();
	}

	public override void Start ()
	{
		base.Start ();
	}

	public override void Update ()
	{
		if(Time.realtimeSinceStartup > 5 && !appleNotificationShowed){
			Messenger<Notification>.Broadcast("SendNotification", new NotificationWindow(
				new Rect(0f, 0f, 300f, 200f),
				"Hey, use the Nail Spitter to defend yourself! You can place it by clicking on any tile highlighted in green.",
				Resources.Load("Icons/NailSpitterIcon") as Texture2D,
				null));
			appleNotificationShowed = true;
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
