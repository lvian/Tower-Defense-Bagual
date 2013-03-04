using UnityEngine;
using System.Collections;

public class heart : MonoBehaviour {
	
	
	public float lives;
	private PlayerManager player;
	private Light _light;
	
	// Use this for initialization
	void Start () {
		_light = gameObject.GetComponent("light") as Light;
		_light = gameObject.light;
		player = PlayerManager.instance;
		StartCoroutine(heartLightBeating());
		lives = player.Lives;
	}
	
	// Update is called once per frame
	void Update () {
		lives = player.Lives;
		if (lives > 0)
			transform.Rotate(((5 * lives) * Vector3.up * Time.deltaTime));
			
	}
	
	IEnumerator heartLightBeating()
	{
		//Debug.Log("waitting");
		yield return new  WaitForSeconds(lives / 5 );
		if( lives > 0)
		{
			if( _light.enabled == true) 
			{
				_light.enabled = false;
				StartCoroutine(heartLightBeating());
				//tocarsom
			}	
			else
			{
				_light.enabled = true;
				StartCoroutine(heartLightBeating());
				//tocarsom
			}
		}
		else
		{
			_light.enabled = false;
		}
	}
}
