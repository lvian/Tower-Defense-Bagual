using UnityEngine;
using System.Collections;

public class CommonBot : IBotStats {
	#region IBotStats implementation
	public string Name {
		get {
			return "CommonBot";
		}
	}

	public string Prefab {
		get {
			return "Prefabs/Bot";
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
			return 10f;
		}
	}
	
	public float damage {
		get {
			return 1f;
		}
	}
	
	public int bounty {
		get {
			return 2;
		}
	}
	
	public int score {
		get {
			return 5;
		}
	}
	#endregion
}
