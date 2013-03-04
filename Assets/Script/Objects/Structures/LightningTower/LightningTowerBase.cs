using UnityEngine;
using System.Collections;

public class LightningTowerBase : IStructureStats {
	
	#region IStructureStats implementation
	public string Name {
		get {
			return "Lightning Tower";
		}
	}
	public string Prefab {
		get {
			return "Prefabs/LightningTower";
		}
	}

	public float damage {
		get {
			return 6f;
		}
	}

	public float reach {
		get {
			return 3f;
		}
	}
	
	public int price {
		get {
			return 30;
		}
	}
	
	public  int jumps {
		get{
			return 2;	
		}
		
	}
	
	#endregion
}
