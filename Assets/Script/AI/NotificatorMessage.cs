using UnityEngine;
using System.Collections;

public class NotificatorMessage{
	private string 	_message;
	private float	_arrivalTime;
	
	public NotificatorMessage(string message, float arrivalTime){
		_message = message;
		_arrivalTime = arrivalTime;
	}

	public float ArrivalTime {
		get {
			return this._arrivalTime;
		}
		set {
			_arrivalTime = value;
		}
	}

	public string Message {
		get {
			return this._message;
		}
		set {
			_message = value;
		}
	}
}
