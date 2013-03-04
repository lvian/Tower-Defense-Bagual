using UnityEngine;
using System.Collections;

public class Damage {
	private float dmgValue;
	private DamageType dmgType;
	
	#region Contructors
	public Damage(){
		dmgValue = 0f;
		dmgType = DamageType.Physical;
	}
	
	public Damage(float dmg){
		dmgValue = dmg;
		dmgType = DamageType.Physical;
	}
	
	public Damage(float dmg, int type){
		dmgValue = dmg;
		dmgType = (DamageType) type;
	}
	#endregion
	
	#region Encapsulated fields
	public DamageType DmgType {
		get {
			return this.dmgType;
		}
		set {
			dmgType = value;
		}
	}

	public float DmgValue {
		get {
			return this.dmgValue;
		}
		set {
			dmgValue = value;
		}
	}
	#endregion
}

public enum DamageType {
	Physical,
	Fire,
	Oil,
	Water
}	