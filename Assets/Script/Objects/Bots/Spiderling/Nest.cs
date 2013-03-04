using UnityEngine;
using System.Collections;

public class Nest : IBotStats {
	#region IBotStats implementation
	public string Name {
		get {
			return "Metal Widow's Nest";
		}
	}

	public string Prefab {
		get {
			return "Prefabs/MetalWidowNest";
		}
	}

	public int health {
		get {
			return 25;
		}
	}

	public int shield {
		get {
			return 0;
		}
	}

	public float armor {
		get {
			return 5f;
		}
	}

	public float speed {
		get {
			return 0f;
		}
	}

	public float damage {
		get {
			return 0f;
		}
	}

	public int bounty {
		get {
			return 0;
		}
	}

	public int score {
		get {
			return 3;
		}
	}
	#endregion
}
