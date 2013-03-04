using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Wave : MonoBehaviour {
	
	public WaveUnit[] 	units;
	public bool			done;
	public bool 		started;		// defined by Waves script
	
	void Awake(){
		done = started = false;
	}
	
	void Start(){
	}
	
	void Update(){
		if(Game.getInstance().isPaused())
			return;
		if(!started)
			return;
		if(done)
			return;
		bool tmpDone = true;
		foreach(WaveUnit unit in units){
			if(unit.currentDelay <= 0 && unit.currentCooldown <=0){
				if(unit.unitCount < unit.Amount){
					spawnUnit(unit.Name, unit.Lane);
					unit.unitCount++;
					unit.currentDelay = unit.Delay;
				}
			}
			else{
				unit.currentDelay -= Time.deltaTime;	
			}
			tmpDone = (tmpDone && unit.Done);
			unit.currentCooldown -= Time.deltaTime;
		}
		done = tmpDone;
	}
	
	public void initUnits(){
		done = started = false;
		foreach(WaveUnit unit in units){
			unit.unitCount 		= 0;
			unit.currentDelay 	= unit.Delay;
			unit.currentCooldown= unit.Cooldown;
		}
	}
	
	private void spawnUnit(string unit, int lane){
		InvadingUnit iUnit = getUnit(unit);
		IBotStats botStats = iUnit.Stat;
		GameObject  newUnit = Object.Instantiate(Resources.Load(botStats.Prefab)) as GameObject;
		newUnit.name = botStats.Name+Time.realtimeSinceStartup;
		Bot botScript = newUnit.GetComponent("Bot") as Bot;
		botScript.botStats = botStats;
		botScript.botBehavior = iUnit.BotBehavior;
		botScript.defendBehavior = iUnit.DefendBehavior;
		botScript.moveBehavior = iUnit.MoveBehavior;
		botScript.init();
		botScript.setPath(lane);
	}
	
	private InvadingUnit getUnit(string unit){
		switch(unit){
		case "SteamRebel":
			return new InvadingUnit(new SteamRebelDefense(), new SteamRebel(), new SteamRebelBehavior(), new WalkBehavior());
		case "MetalWidow":
			return new InvadingUnit(new MetalWidowDefense(), new MetalWidow(), new MetalWidowBehavior(), new CrawlBehavior());
		case "IronBeetle":
			return new InvadingUnit(new IronBeetleDefense(), new IronBeetle(), new IronBeetleBehavior(), new WalkBehavior());
		default:
			Debug.Log("Creating null");
			return null;
		}
	}
}