using UnityEngine;
using System.Collections;

public class OneTimePlayedParticle : MonoBehaviour {
	
	ParticleSystem[] particles;
	
	void Start () {
		particles = GetComponentsInChildren<ParticleSystem>();
		foreach(ParticleSystem ps in particles){
			ps.Play();
		}
	}
	
	void Update () {
		bool destroy = true;
		foreach(ParticleSystem ps in particles){
			destroy =  (destroy && !ps.isPlaying); 
		}
		if(destroy)
			Destroy(this.gameObject);
	}
}
