using UnityEngine;
using System.Collections;

public class NailSpitterBase : IStructureStats {
	#region IStructureStats implementation
	public string Name {
		get {
			return "Nail Spitter";
		}
	}
	public string Prefab {
		get {
			return "Prefabs/NailSpitter";
		}
	}

	public float damage {
		get {
			return 8f;
		}
	}

	public float reach {
		get {
			return 3f;
		}
	}
	
	public int price {
		get {
			return 20;
		}
	}
	
	public int jumps {
		get {
			return 0;
		}
	}
	#endregion
}
