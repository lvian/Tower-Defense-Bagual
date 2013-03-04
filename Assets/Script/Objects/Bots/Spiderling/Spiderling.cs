using UnityEngine;
using System.Collections;

public class Spiderling : IBotStats {
	#region IBotStats implementation
	public string Name {
		get {
			return "Spiderling";
		}
	}

	public string Prefab {
		get {
			return "Prefabs/Spiderling";
		}
	}

	public int health {
		get {
			return 10;
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
			return 30f;
		}
	}

	public float damage {
		get {
			return 1f;
		}
	}

	public int bounty {
		get {
			return 0;
		}
	}

	public int score {
		get {
			return 1;
		}
	}
	#endregion
}
