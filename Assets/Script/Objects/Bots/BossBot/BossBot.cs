using UnityEngine;
using System.Collections;

public class BossBot : IBotStats {
	#region IBotStats implementation
	public string Name {
		get {
			return "BossBot";
		}
	}

	public string Prefab {
		get {
			return "Prefabs/BossBot";
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
			return 20;
		}
	}
	public int score {
		get {
			return 500;
		}
	}
	#endregion

}
