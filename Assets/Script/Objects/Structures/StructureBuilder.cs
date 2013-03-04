using UnityEngine;
using System.Collections;

public class StructureBuilder : MonoBehaviour {
	public float buildingTime = 0f;
	public Vector3 position;
	public IStructureBehavior attackBehavior;
	public TowerFactory factory;
	public StructureManager structureManager;
	
	private float temporizer;
	
	void Awake(){
		temporizer = buildingTime;
	}
	
	void Start(){
		transform.Translate(new Vector3(0f, gameObject.renderer.bounds.size.y /2 ,0f));
	}
	
	void Update(){
		if(!Game.getInstance().isPaused()){
			if(temporizer >= 0){
				structureManager.addStructure(position, attackBehavior, factory);
				Destroy(gameObject);
			}
			temporizer -= Time.deltaTime;
		}
	}
	
	void OnGUI(){
		
	}
}
