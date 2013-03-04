using UnityEngine;
using System.Collections;

public class WaveUnit{
	private string 	_name;			// unit name
	private float 	_delay;			// delay between each unit spawn
	private int		_cooldown;		// star time of units spawn since the start of the wave
	private int		_lane;			// lane wich the units are spawned
	private int		_amount;		// amount of units created
	
	public float	currentDelay;		// amount of time since the last respawn
	public float	currentCooldown;	// amount of time sinnce the last wave start
	public int		unitCount;			// amount of unit already spawned
	
	
	public WaveUnit(string name, float delay, int cooldown, int lane, int maxamount){
		this._name 		= name;
		this._delay 	= delay;
		this._cooldown 	= cooldown;
		this._lane 		= lane;
		this._amount = maxamount;
		
		currentDelay	= 0;
		unitCount		= 0;
	}
	
	public bool Done{
		get {
			return (unitCount == Amount);	
		}
	}
	
	public int Cooldown {
		get {
			return this._cooldown;
		}
	}

	public float Delay {
		get {
			return this._delay;
		}
	}

	public int Lane {
		get {
			return this._lane;
		}
	}

	public int Amount {
		get {
			return this._amount;
		}
	}

	public string Name {
		get {
			return this._name;
		}
	}
}
