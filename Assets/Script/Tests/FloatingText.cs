using UnityEngine;
using System.Collections;

public class FloatingText : MonoBehaviour {
	public float lifetime;
	public float speed;
	public Color color;
	
	private TextMesh text;
	

	// Use this for initialization
	void Start () {
		text = GetComponent<TextMesh>();
		text.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		renderer.material.color = color;
		if(text.text != "" && lifetime > 0){
			transform.position = Vector3.MoveTowards(text.transform.position, Vector3.up, speed * Time.deltaTime);
			lifetime -= Time.deltaTime;
		}
		if(lifetime <= 0){
			text.gameObject.renderer.enabled = false;
		}	
	}
}
