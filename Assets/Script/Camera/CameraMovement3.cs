using UnityEngine;
using System.Collections;

public class CameraMovement3 : MonoBehaviour {
	
	public float minFOV = 25;
	public float maxFOV = 50;
	public float speed  = 50f;
	
	
	
	void Start(){
		
	}
	
	
	void Update()
	{
		if(Game.getInstance().GState == Game.GameState.Regular){
			if(Input.GetAxis("Mouse ScrollWheel") > 0){
				if(transform.camera.fov > minFOV){
					transform.camera.fov -= 2;
				}
			}
			if(Input.GetAxis("Mouse ScrollWheel") < 0){
				if(transform.camera.fov < maxFOV){
					transform.camera.fov += 2;
				}
				
			}
			
			float x = 0,y = 0;
			if(Input.GetMouseButton(1)){
				x = -Input.GetAxis("Mouse X") * Time.deltaTime * speed;
				y = -Input.GetAxis("Mouse Y") * Time.deltaTime * speed;
			}
			
			transform.Translate(x, y, 0f);
			
			// [left]   [right]
			//  [top]   [bottom]
			Vector3[][] camSides = new Vector3[][]{
				new Vector3[] {camera.ViewportToWorldPoint(new Vector3(.5f,0,camera.nearClipPlane)), camera.ViewportToWorldPoint(new Vector3(.5f,1,camera.nearClipPlane))},
				new Vector3[] {camera.ViewportToWorldPoint(new Vector3(0,.5f,camera.nearClipPlane)), camera.ViewportToWorldPoint(new Vector3(1,.5f,camera.nearClipPlane))}
			};
			
		}
	}
}
