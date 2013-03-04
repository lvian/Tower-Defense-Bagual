using UnityEngine;
using System.Collections;

public class FoodFactoryWave : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		GameObject waves = new GameObject("Waves");
		waves.transform.parent = transform;
		waves.AddComponent<Waves>();
			
		GameObject wave1 = new GameObject("wave1");
		wave1.transform.parent = waves.transform;
		wave1.AddComponent<Wave>();
		Wave wave1Script = wave1.GetComponent<Wave>();
		wave1Script.units = new WaveUnit[] {
			new WaveUnit("SteamRebel", 2f, 0, 0, 2),
			new WaveUnit("SteamRebel", 2f, 10, 0, 2),
			new WaveUnit("SteamRebel", 2f, 20, 0, 2),
			new WaveUnit("SteamRebel", 2f, 30, 0, 3),
			new WaveUnit("SteamRebel", 2f, 40, 0, 3),
		};
		
		GameObject wave2 = new GameObject("wave2");
		wave2.transform.parent = waves.transform;
		wave2.AddComponent<Wave>();
		Wave wave2Script = wave2.GetComponent<Wave>();
		wave2Script.units = new WaveUnit[]{ 
			new WaveUnit("SteamRebel", 2f, 0, 1, 2),
			new WaveUnit("SteamRebel", 2f, 10, 1, 3),
			new WaveUnit("SteamRebel", 2f, 20, 1, 3),
			new WaveUnit("SteamRebel", 2f, 30, 1, 4),
			new WaveUnit("SteamRebel", 2f, 40, 1, 4),
		};
		
		GameObject wave3 = new GameObject("wave3");
		wave3.transform.parent = waves.transform;
		wave3.AddComponent<Wave>();
		Wave wave3Script = wave3.GetComponent<Wave>();
		wave3Script.units = new WaveUnit[] {
			new WaveUnit("SteamRebel", 2f, 0, 0, 4),
			new WaveUnit("SteamRebel", 2f, 10, 1, 4),
			new WaveUnit("SteamRebel", 2f, 20, 0, 4),
			new WaveUnit("SteamRebel", 2f, 30, 1, 5),
			new WaveUnit("SteamRebel", 2f, 40, 0, 5)
		};
		
		GameObject wave4 = new GameObject("wave4");
		wave4.transform.parent = waves.transform;
		wave4.AddComponent<Wave>();
		Wave wave4Script = wave4.GetComponent<Wave>();
		wave4Script.units = new WaveUnit[] {
			new WaveUnit("SteamRebel", 2f, 0, 0, 3),
			new WaveUnit("SteamRebel", 2f, 0, 1, 3),
			new WaveUnit("SteamRebel", 2f, 10, 0, 4),
			new WaveUnit("SteamRebel", 2f, 10, 1, 4),
			new WaveUnit("SteamRebel", 2f, 22, 0, 5),
			new WaveUnit("SteamRebel", 2f, 22, 1, 5)
			
		};
		
		
		GameObject wave5 = new GameObject("wave5");
		wave5.transform.parent = waves.transform;
		wave5.AddComponent<Wave>();
		Wave wave5Script = wave5.GetComponent<Wave>();
		wave5Script.units = new WaveUnit[] {
			new WaveUnit("SteamRebel", 2f, 0, 0, 4),
			new WaveUnit("SteamRebel", 2f, 0, 1, 4),
			new WaveUnit("SteamRebel", 2f, 15, 0, 5),
			new WaveUnit("SteamRebel", 2f, 15, 1, 5),
			new WaveUnit("SteamRebel", 2f, 30, 0, 6),
			new WaveUnit("SteamRebel", 2f, 30, 1, 6)
		};
		
		GameObject wave6 = new GameObject("wave6");
		wave6.transform.parent = waves.transform;
		wave6.AddComponent<Wave>();
		Wave wave6Script = wave6.GetComponent<Wave>();
		wave6Script.units = new WaveUnit[] {
			new WaveUnit("MetalWidow", 2f, 0, 0, 1),
			new WaveUnit("MetalWidow", 2f, 0, 1, 1),
			new WaveUnit("MetalWidow", 2f, 10, 0, 2),
			new WaveUnit("MetalWidow", 2f, 10, 1, 2),
			new WaveUnit("MetalWidow", 2f, 20, 0, 3),
			new WaveUnit("MetalWidow", 2f, 20, 1, 3)
		};
		
		GameObject wave7 = new GameObject("wave7");
		wave7.transform.parent = waves.transform;
		wave7.AddComponent<Wave>();
		Wave wave7Script = wave7.GetComponent<Wave>();
		wave7Script.units = new WaveUnit[] {
			new WaveUnit("MetalWidow", 2f, 0, 0, 1),
			new WaveUnit("MetalWidow", 2f, 0, 1, 1),
			new WaveUnit("SteamRebel", 2f, 2, 0, 2),
			new WaveUnit("SteamRebel", 2f, 2, 1, 2),
			new WaveUnit("MetalWidow", 2f, 10, 0, 1),
			new WaveUnit("MetalWidow", 2f, 10, 1, 1),
			new WaveUnit("SteamRebel", 2f, 10, 0, 2),
			new WaveUnit("SteamRebel", 2f, 10, 1, 2),
			new WaveUnit("MetalWidow", 2f, 15, 0, 1),
			new WaveUnit("MetalWidow", 2f, 15, 1, 1),
			new WaveUnit("SteamRebel", 2f, 15, 0, 3),
			new WaveUnit("SteamRebel", 2f, 15, 1, 3)
		};
		
		GameObject wave8 = new GameObject("wave8");
		wave8.transform.parent = waves.transform;
		wave8.AddComponent<Wave>();
		Wave wave8Script = wave8.GetComponent<Wave>();
		wave8Script.units = new WaveUnit[] {
			new WaveUnit("MetalWidow", 2f, 0, 0, 2),
			new WaveUnit("SteamRebel", 2f, 0, 1, 2),
			new WaveUnit("MetalWidow", 2f, 10, 0, 2),
			new WaveUnit("SteamRebel", 2f, 10, 1, 4),
			new WaveUnit("MetalWidow", 2f, 0, 1, 2),
			new WaveUnit("SteamRebel", 2f, 0, 0, 2),
			new WaveUnit("MetalWidow", 2f, 10, 1, 2),
			new WaveUnit("SteamRebel", 2f, 10, 0, 4),
			new WaveUnit("MetalWidow", 2f, 20, 0, 2),
			new WaveUnit("SteamRebel", 2f, 20, 1, 5),
			new WaveUnit("MetalWidow", 2f, 20, 1, 2),
			new WaveUnit("SteamRebel", 2f, 20, 0, 5)
		};
		
		
		GameObject wave9 = new GameObject("wave9");
		wave9.transform.parent = waves.transform;
		wave9.AddComponent<Wave>();
		Wave wave9Script = wave9.GetComponent<Wave>();
		wave9Script.units = new WaveUnit[] {
			new WaveUnit("MetalWidow", 2f, 0, 0, 2),
			new WaveUnit("MetalWidow", 2f, 0, 1, 2),
			new WaveUnit("SteamRebel", 2f, 2, 0, 2),
			new WaveUnit("SteamRebel", 2f, 2, 1, 2),
			new WaveUnit("MetalWidow", 2f, 10, 0, 2),
			new WaveUnit("MetalWidow", 2f, 10, 1, 2),
			new WaveUnit("SteamRebel", 2f, 15, 0, 3),
			new WaveUnit("SteamRebel", 2f, 15, 1, 3),
			new WaveUnit("MetalWidow", 2f, 22, 0, 2),
			new WaveUnit("MetalWidow", 2f, 22, 1, 2),
			new WaveUnit("SteamRebel", 2f, 25, 0, 4),
			new WaveUnit("SteamRebel", 2f, 25, 1, 4),
			new WaveUnit("MetalWidow", 2f, 25, 0, 2),
			new WaveUnit("SteamRebel", 2f, 25, 1, 4),
		};
		
		
		GameObject wave10 = new GameObject("wave10");
		wave10.transform.parent = waves.transform;
		wave10.AddComponent<Wave>();
		Wave wave10Script = wave10.GetComponent<Wave>();
		wave10Script.units = new WaveUnit[] {
			new WaveUnit("MetalWidow", 2f, 0, 0, 2),
			new WaveUnit("SteamRebel", 2f, 0, 1, 4),
			new WaveUnit("MetalWidow", 2f, 15, 0, 3),
			new WaveUnit("SteamRebel", 2f, 15, 1, 5),
			new WaveUnit("MetalWidow", 2f, 30, 1, 4),
			new WaveUnit("SteamRebel", 2f, 30, 0, 6),
			new WaveUnit("MetalWidow", 2f, 45, 1, 5),
			new WaveUnit("SteamRebel", 2f, 45, 0, 5)
		};
	}
}
