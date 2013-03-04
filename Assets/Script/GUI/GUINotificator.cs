using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUINotificator : MonoBehaviour {
	public GUISkin skin;
	public List<NotificatorMessage> notifications;
	public List<NotificatorMessage> notificationsCopy;
	public float messageTTL = 8;
	
	void Start () {
		notifications = new List<NotificatorMessage>();
		notificationsCopy = new List<NotificatorMessage>();
		Messenger<string>.AddListener("Notification", enqueueNotification);
	}
	
	void Update () {
		foreach(NotificatorMessage nm in notifications){
			if((Time.realtimeSinceStartup - nm.ArrivalTime) > messageTTL){
				notificationsCopy.Add(nm);
			}
		}
		foreach(NotificatorMessage nm in notificationsCopy){
			if((Time.realtimeSinceStartup - nm.ArrivalTime) > messageTTL){
				notifications.Remove(nm);
			}
		}
	}
	
	void enqueueNotification(string message){
		notifications.Add(new NotificatorMessage(message, Time.realtimeSinceStartup));
	}
	
	void OnGUI(){
		GUI.skin = skin;
		GUI.BeginGroup(new Rect(Screen.width * .01f, Screen.height - (Screen.height * .01f) - 110, 300, 110));
		GUI.skin = skin;
		int messagesCounter;
		if(notifications.Count > 5){
			messagesCounter = 5;
		}
		else{
			messagesCounter = notifications.Count;	
		}
		for(int i = 0; i < messagesCounter; i++){
			NotificatorMessage message = notifications.ToArray()[i];
			GUI.color = new Color(1f, 1f, 1f, 1 - (Time.realtimeSinceStartup - message.ArrivalTime)/messageTTL);
			GUI.Label(new Rect(2.5f, (messagesCounter - (i +1)) * 20 + 2.5f, 300, 30),message.Message);
		}
		
		
		GUI.EndGroup();
	}
}
