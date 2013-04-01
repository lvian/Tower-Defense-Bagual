using UnityEngine;
using System.Collections;

public class StoryWindow : Notification {

	Rect rect;
	Texture2D image;

	public StoryWindow(Rect r, Texture2D img, Notification nt = null){
		rect = r;
		notification = null;
		image = img;
		pauseGame = true;
		type = NotificationType.Story;
		nextNotification = nt;
	}
	
	
	#region implemented abstract members of Notification
	public override Rect draw(){
		GUI.skin = null;
		rect = new Rect(Screen.width / 10, Screen.height / 10 , Screen.width - Screen.width / 5 , Screen.height - Screen.height / 5 );
		Game.getInstance().GState = Game.GameState.Pause;
		GUI.DrawTexture(rect ,image);
		
		if(nextNotification != null){
			if(GUI.Button(new Rect(rect.width -5, rect.height , 75f, 30f), "Next"))
			{
				Messenger<Notification>.Broadcast("RemoveNotification", this);
				Game.getInstance().GState = Game.GameState.Regular;
			}
		}
		else
		{
			if(GUI.Button(new Rect(rect.width -5 , rect.height , 75f, 30f), "OK"))
			{
				Messenger<Notification>.Broadcast("RemoveNotification", this);
				Game.getInstance().GState = Game.GameState.Regular;
			}
		}
		if(nextNotification != null)
		{
			if(GUI.Button(new Rect(rect.width  -45, rect.height , 75f, 30f), "Skip"))
			{
				Messenger<Notification>.Broadcast("SkipNotification", this);
				Game.getInstance().GState = Game.GameState.Regular;
			}
		}
		return rect;
	}
	#endregion
	
}
