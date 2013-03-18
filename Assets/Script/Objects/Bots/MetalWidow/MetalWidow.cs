using UnityEngine;
using System.Collections;

public class MetalWidow : IBotStats {
	#region IBotStats implementation
	public string Name {
		get {
			return "Metal Widow";
		}
	}

	public string Prefab {
		get {
			return "Prefabs/MetalWidow";
		}
	}

	public int health {
		get {
			return 30;
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
			return 45f;
		}
	}

	public float damage {
		get {
			return 2f;
		}
	}

	public int bounty {
		get {
			return 2;
		}
	}

	public int score {
		get {
			return 7;
		}
	}
	#endregion
}
