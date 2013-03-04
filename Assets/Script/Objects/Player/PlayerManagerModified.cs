using UnityEngine;
using System.Collections;

public class PlayerManagerModified : IEvent {
	#region IEvent implementation
	public string GetName ()
	{
		return this.GetType().ToString();
	}

	public object GetData ()
	{
		return "The PlayerManager modified";
	}
	#endregion
}
