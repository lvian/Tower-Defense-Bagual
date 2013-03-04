using UnityEngine;
using System.Collections;

public class StructureAttribute : Stats {
	public StructureAttribute(){
		BaseValue = 0;	
	}
}

public enum StructureAttributeNames {
	Dps,
	Reach,
	Price,
	Jumps,
	AttackSpeed
}
