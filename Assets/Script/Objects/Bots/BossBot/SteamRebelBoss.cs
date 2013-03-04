using UnityEngine;
using System.Collections;

public class SteamRebelBoss : IBotStats {
	#region IBotStats implementation
	public string Name {
		get {
			return "Steam Rebel Boss";
		}
	}

	public string Prefab {
		get {
			return "Prefabs/SteamRebelBoss";
		}
	}

	public int health {
		get {
			return 500;
		}
	}

	public int shield {
		get {
			return 0;
		}
	}

	public float armor {
		get {
			return 2f;
		}
	}

	public float speed {
		get {
			return 1f;
		}
	}
	
	 public float damage {
		get {
			return 2f;
		}
	}
	
	public int bounty {
		get {
			return 50;
		}
	}
	
	public int score {
		get {
			return 500;
		}
	}
	#endregion

}
