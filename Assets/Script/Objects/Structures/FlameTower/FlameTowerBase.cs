using UnityEngine;
using System.Collections;

public class FlameTowerBase : IStructureStats {
	#region IStructureStats implementation
	public string Name {
		get {
			return "Flame Tower";
		}
	}
	public string Prefab {
		get {
			return "Prefabs/FlameTower";
		}
	}

	public float damage {
		get {
			return 4f;
		}
	}

	public float reach {
		get {
			return 2f;
		}
	}
	
	public int price {
		get {
			return 25;
		}
	}
	
	public int jumps {
		get {
			return 0;
		}
	}
	#endregion
}
