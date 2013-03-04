using UnityEngine;
using System.Collections;

public class IronBeetle : IBotStats {
	#region IBotStats implementation
	public string Name {
		get {
			return "Iron Beetle";
		}
	}

	public string Prefab {
		get {
			return "Prefabs/IronBeetle";
		}
	}

	public int health {
		get {
			return 100;
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
			return 20f;
		}
	}
	
	public float damage {
		get {
			return 3f;
		}
	}
	
	public int bounty {
		get {
			return 3;
		}
	}
	
	public int score {
		get {
			return 10;
		}
	}
	#endregion
}
