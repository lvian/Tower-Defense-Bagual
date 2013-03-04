using UnityEngine;
using System.Collections;

public class OilSquallBase : IStructureStats {
	#region IStructureStats implementation
	public string Name {
		get {
			return "Oil Squall";
		}
	}

	public string Prefab {
		get {
			return "Prefabs/OilSquall";
		}
	}

	public float damage {
		get {
			return 0f;
		}
	}

	public float reach {
		get {
			return 3.5f;
		}
	}
	
	public int price {
		get {
			return 30;
		}
	}
	
	public int jumps{
		get {
			return 0;
		}
	}
	#endregion
}
