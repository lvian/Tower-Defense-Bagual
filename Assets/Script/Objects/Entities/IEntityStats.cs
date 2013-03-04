using UnityEngine;
using System.Collections;

public interface IEntityStats {
	string Name   { get; }
	string Prefab { get; }
	
	int health  { get; }

	int shield  { get; }
	
	float armor { get; }
	
	float speed { get; }
	
	float dps   { get; }
	
	float reach { get; }
}
