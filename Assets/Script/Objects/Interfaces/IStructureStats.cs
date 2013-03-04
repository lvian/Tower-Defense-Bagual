using UnityEngine;
using System.Collections;

public interface IStructureStats {
	string Name   { get; }
	string Prefab { get; }
	
	float damage   { get; }
	
	//float attackSpeed { get; }
	
	float reach { get; }
	
	int jumps { get; }
	
	int	price { get; }
}