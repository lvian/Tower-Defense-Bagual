using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {
	
	public GameObject target;
	public float speed;
	public float dps;
	Transform thisTransform;
	
	void Start(){
		thisTransform = transform;
	}
	
	void Update(){
		if(!Game.getInstance().isPaused()){
			try{
				transform.position = thisTransform.position;
				transform.rotation = thisTransform.rotation;
				float distance = Vector3.Distance (transform.position, target.transform.position);
				if(distance < 1){	
					Bot botScript = target.GetComponent("Bot") as Bot;
					botScript.Attack(new Damage(dps));
					Destroy(this.gameObject);
				}
			}
			catch{
				Destroy(this.gameObject);
			}
		}
	}	
	
	void LateUpdate(){
		if(target != null){
			thisTransform.rotation = Quaternion.Slerp(thisTransform.rotation, Quaternion.LookRotation(target.transform.position - thisTransform.position), 100f * Time.deltaTime);
			thisTransform.position+= thisTransform.forward * speed * Time.deltaTime;
		}
	}
}
