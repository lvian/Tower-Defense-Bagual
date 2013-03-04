using UnityEngine;
using System.Collections;

public class JumpShot : MonoBehaviour {
	
	public GameObject target;
	public float speed;
	public float dps;
	public int jumps;
	public float jumpRange;
	Transform thisTransform;
	public ArrayList targetsHit;
	private float timeStamp;
	
	void Start(){
		timeStamp = Time.time;
		thisTransform = transform;
		targetsHit = new ArrayList();
	}
	
	void Update(){
		if(!Game.getInstance().isPaused()){
			try{
				transform.position = thisTransform.position;
				transform.rotation = thisTransform.rotation;
				float distance = Vector3.Distance (transform.position, target.transform.position);
				if(distance < 0.3){	
					Bot botScript = target.GetComponent("Bot") as Bot;
					botScript.Attack(new Damage(dps));
					targetsHit.Add(botScript.name);
					if(jumps > 0 && target)
					{
						target = null;
						float dist = jumpRange;
						GameObject[] targets = GameObject.FindGameObjectsWithTag("Respawn");
						if(targets.Length > 0)
						{
							foreach(GameObject tgt in targets)
							{
								Bot tmpBot = tgt.GetComponent("Bot") as Bot;
								float tempdist = (transform.position - tgt.transform.position).magnitude;
								if( tempdist < dist && !targetsHit.Contains(tgt.name) && tmpBot.isAlive()  )
								{
									dist = tempdist;
									target = tgt.gameObject;
								}
							}
							jumps--;
						}
						else
						{
							StartCoroutine(destroyShot(this.gameObject));
						}
					}
					else
					{
						target = null;
						StartCoroutine(destroyShot(this.gameObject));
					}
				}
			}
			catch{
				StartCoroutine(destroyShot(this.gameObject));
			}
		}
	}	
	
	
	IEnumerator destroyShot(GameObject gb)
	{
		yield return new WaitForSeconds(0.5f);
		Destroy(this.gameObject);
	}
		
	void LateUpdate(){
		
		if(target != null){
			if(timeStamp != Time.time)
			{
			transform.Translate(Vector3.forward * Time.deltaTime*speed);
			transform.Rotate(Vector3.right * Time.deltaTime);
			transform.Translate(new Vector3(Random.Range(-20.0f,20.0f)  ,Random.Range(-20.0f,20.0f)  ,Random.Range(-20.0f,20.0f) ) * Time.deltaTime );
			transform.Rotate(Vector3.left * Time.deltaTime);
			transform.LookAt(target.transform);//Look target.
			/*
			thisTransform.rotation = Quaternion.Slerp(thisTransform.rotation, Quaternion.LookRotation(target.transform.position - thisTransform.position), 100f * Time.deltaTime);*/
			transform.position += transform.forward * speed * Time.deltaTime;
			}
			
		}
	}
}
