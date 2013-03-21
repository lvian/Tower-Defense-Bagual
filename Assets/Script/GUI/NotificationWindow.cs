using UnityEngine;
using System.Collections;

public class NotificationWindow : Notification {
	Rect rect;
	Texture2D image;

	public NotificationWindow(Rect r, string n, Notification nt){
		rect = r;
		notification = n;
		image = null;
		pauseGame = true;
		type = NotificationType.Notification;
		nextNotification = nt;
	}
	
	public NotificationWindow(Rect r, string n, Texture2D i, Notification nt){
		rect = r;
		notification = n;
		image = i;
		pauseGame = true;
		type = NotificationType.Notification;
		nextNotification = nt;		
	}
	#region implemented abstract members of Notification
	public override Rect draw(){
		rect = new Rect(Screen.width/2 - rect.width/2, Screen.height/2 - rect.height/2, rect.width, rect.height);
		Game.getInstance().GState = Game.GameState.Pause;
		GUI.Window(1, rect, doWindow, "Notification");
		return rect;
	}
	#endregion
	
	void doWindow(int windowID){
		GUI.skin = null;
		if(image){
			GUI.Box(new Rect(10,15,64,64),image);
			GUI.Label(new Rect(20 + 64, 15, rect.width - (30 + 64), rect.height - 50),notification);
			if(GUI.Button(new Rect(rect.width - 80, rect.height - 35, 75f, 30f), "Dissmiss")){
				Messenger<Notification>.Broadcast("RemoveNotification", this);
				Game.getInstance().GState = Game.GameState.Regular;
			}
		}
	}
	
}
