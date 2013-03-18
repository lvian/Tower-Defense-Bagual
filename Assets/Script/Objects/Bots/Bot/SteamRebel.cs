using UnityEngine;
using System.Collections;

public class SteamRebel : IBotStats {
	#region IBotStats implementation
	public string Name {
		get {
			return "Steam Rebel";
		}
	}

	public string Prefab {
		get {
			return "Prefabs/SteamRebel";
		}
	}

	public int health {
		get {
			return 40;
		}
	}

	public int shield {
		get {
			return 0;
		}
	}
	
	public float armor {
		get {
			return 0f;
		}
	}

	public float speed {
		get {
			return 40f;
		}
	}
	
	public float damage {
		get {
			return 1f;
		}
	}
	
	public int bounty {
		get {
			return 1;
		}
	}
	
	public int score {
		get {
			return 5;
		}
	}
	#endregion
}
