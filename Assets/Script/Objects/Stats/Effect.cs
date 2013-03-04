using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Effect : Effects {
	private bool active;
	private GameObject source;
	
	
	private Dictionary<int,Modifier> multiplicativeMods;
	private Dictionary<int,Modifier> additiveMods;
	
	public Effect(){
		active = true;
		multiplicativeMods = new Dictionary<int,Modifier>();
		additiveMods = new Dictionary<int,Modifier>();
	}
	
	public Effect(GameObject source){
		this.source = source;
		active = true;
		multiplicativeMods = new Dictionary<int,Modifier>();
		additiveMods = new Dictionary<int,Modifier>();
	}

	
	#region Effect implementation
	public float apllyAttributeModifier (int attributeID, float currentValue)
	{
		float val = currentValue;
		if(active){
			Modifier mod = null;
			
			if( multiplicativeMods.ContainsKey(attributeID) ){
				mod = multiplicativeMods[attributeID];
				val = val * mod.Amount;
			}
			
			if( additiveMods.ContainsKey(attributeID) ){
				mod = additiveMods[attributeID];
				val = val + mod.Amount;
			}
		}	
		return val;
	}
	#endregion	
	
	#region Encapsulated fields
	public bool Active {
		get {
			return this.active;
		}
		set {
			active = value;
		}
	}

	public System.Collections.Generic.Dictionary<int, Modifier> AdditiveMods {
		get {
			return this.additiveMods;
		}
		set {
			additiveMods = value;
		}
	}

	public System.Collections.Generic.Dictionary<int, Modifier> MultiplicativeMods {
		get {
			return this.multiplicativeMods;
		}
		set {
			multiplicativeMods = value;
		}
	}

	public UnityEngine.GameObject Source {
		get {
			return this.source;
		}
		set {
			source = value;
		}
	}

	#endregion
}
