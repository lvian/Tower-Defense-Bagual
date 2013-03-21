using UnityEngine;
using System.Collections;

public abstract class Notification{
	public string 			notification;
	public bool				pauseGame;
	public NotificationType	type;
	public Notification		nextNotification;
	
	public abstract Rect draw();
}

public enum NotificationType{
	Story,
	Tip,
	Notification,
	Unlock,
	Error
}
