using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class InvadingEntity : MonoBehaviour {
	public IDefendBehavior 	defendBehavior;
	public IBotBehavior 	botBehavior;
	public IMoveBehavior	moveBehavior;
	public IBotStats 		botStats;
	
	public float 			distanceMoved;
	
	private BotAttribute[] 	_baseAttributes;
	private BotVitals[] 	_botVitals;
	private List<Effects> 	_effects;
	
	#region Initialization
	public void init(){
		initBaseAttributes();
		initVitals();
		_effects = new List<Effects>();
	}
	
	private void initBaseAttributes(){
		_baseAttributes = new BotAttribute[Enum.GetValues(typeof(BotAttributeName)).Length];
		for( int b = 0 ; b < Enum.GetValues(typeof(BotAttributeName)).Length; b++ ){
			_baseAttributes[b] = new BotAttribute();
		}
		_baseAttributes[(int)BotAttributeName.MaxHealth].BaseValue = (float) botStats.health;
		_baseAttributes[(int)BotAttributeName.MaxShield].BaseValue = (float) botStats.shield;
		_baseAttributes[(int)BotAttributeName.Armor].BaseValue     = botStats.armor;
		_baseAttributes[(int)BotAttributeName.Speed].BaseValue     = botStats.speed;
		_baseAttributes[(int)BotAttributeName.Damage].BaseValue	   = botStats.damage;
		_baseAttributes[(int)BotAttributeName.Score].BaseValue	   = botStats.score;
		_baseAttributes[(int)BotAttributeName.Bounty].BaseValue	   = botStats.bounty;
	}
	
	private void initVitals(){
		_botVitals = new BotVitals[Enum.GetValues(typeof(BotVitalsName)).Length];
		for( int b = 0 ; b < Enum.GetValues(typeof(BotVitalsName)).Length; b++ ){
			_botVitals[b] = new BotVitals();	
		}
		_botVitals[(int)BotVitalsName.Health].BaseValue = (float) _baseAttributes[(int)BotAttributeName.MaxHealth].BaseValue;
		_botVitals[(int)BotVitalsName.Shield].BaseValue = (float) _baseAttributes[(int)BotAttributeName.MaxShield].BaseValue;
	}
	#endregion
	
	#region Behaviors
	
	public void attack(Damage damage, Effect[] effects){
		defendBehavior.handleAttack(damage,	effects, gameObject);
	}
	
	public void handleUpdate(){
		botBehavior.handleUpdate(gameObject);	
	}
	
	public void move(){
		distanceMoved += moveBehavior.move(gameObject);
	}
	
	#endregion
	
	public float getAttributeValue(BotAttributeName id){
		float val = _baseAttributes[(int)id].BaseValue;
		foreach ( Effects e in _effects ){
			val = e.apllyAttributeModifier((int)id, val);	
		}
		return val;
	}
	
	public float getVitalValue(BotVitalsName id){
		return _botVitals[(int)id].BaseValue;
	}
	
	public float Attack(Damage dmg){
		_botVitals[(int)BotVitalsName.Health].BaseValue -= dmg.DmgValue;
		return dmg.DmgValue;
	}
	
	public void addEffect(Effects effect){
		_effects.Add(effect);	
	}
	
	public void removeEffect(Effects effect){
		_effects.Remove(effect);
	}
}
