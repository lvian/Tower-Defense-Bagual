using UnityEngine;
using System.Collections;

public class StructureManager : MonoBehaviour {
	
	private Map _mapInstance;
	
	void Start () {
		_mapInstance = Map.getInstance();
	}
	
	void Update () {
	}
	
	public void addStructure(Vector3 position, IStructureBehavior attackBehavior, TowerFactory factory){
		GameObject tileGO = _mapInstance.getTile(new Vector2(position.x, position.z));
		if(!tileGO){
			return;
		}
		Tile tile = tileGO.GetComponent<Tile>();
		Tower tower = factory.GetTower();
		GameObject newStruct = Object.Instantiate(Resources.Load(tower.Prefab)) as GameObject;
		newStruct.name = tower.Name;
		GameObject gObject = new GameObject();
		gObject.name = tower.Name+Time.realtimeSinceStartup;
		gObject.transform.parent = this.transform;
		newStruct.transform.parent = gObject.transform;
		Structure structureScript = newStruct.GetComponent("Structure") as Structure;
		newStruct.transform.position = new Vector3(tileGO.transform.localPosition.x , tileGO.transform.localPosition.y, tileGO.transform.localPosition.z);
		structureScript.structureBehavior = attackBehavior;
		structureScript.tower = tower;
		structureScript.init();
		tile.tower = newStruct;
	}
	
	public void removeStructure(GameObject structure)
	{
		GameObject tileGO = _mapInstance.getTile(new Vector2(structure.transform.position.x, structure.transform.position.z));
		if(!tileGO){
			return;
		}
		Tile tile = tileGO.GetComponent<Tile>();
		tile.tileEmpty = true;
		tile.tower = null;
		
		Structure structureScript = structure.GetComponent<Structure>();
		PlayerManager.instance.Gold += Mathf.CeilToInt(structureScript.getAttributeValue(StructureAttributeNames.Price) * .25f);
		
		Destroy(structure.transform.parent.gameObject);
	}
	
	public void spawnBuilder(Vector3 position, IStructureBehavior attackBehavior, TowerFactory factory){
		GameObject tileGO = _mapInstance.getTile(new Vector2(position.x, position.z));
		if(!tileGO){
			return;
		}
		Tile tile = tileGO.GetComponent<Tile>();
		if(tile.isEmpty() && tile.slot){
			tile.tileEmpty = false;
			GameObject newBuilder = Object.Instantiate(Resources.Load("Prefabs/Builder"), position, Quaternion.identity) as GameObject;
			StructureBuilder builder = newBuilder.GetComponent<StructureBuilder>();
			builder.structureManager = this;
			builder.factory = factory;
			builder.position = position;
			builder.attackBehavior = attackBehavior;
			builder.buildingTime = 4f;
		}
	}
}
