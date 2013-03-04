using UnityEngine;
using System.Collections;

public interface IBotStats {
	string Name   { get; }
	string Prefab { get; }
	
	int health  { get; }

	int shield  { get; }
	
	float armor { get; }
	
	float speed { get; }
	
	float damage  { get; }
	
	int bounty  { get; }
	
	int score  { get; }
}