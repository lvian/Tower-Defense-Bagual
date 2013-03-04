using UnityEngine;
using System.Collections;

public class CameraMovement2 : MonoBehaviour {
	
	public float minFOV = 25;
	public float maxFOV = 50;
	public float speed = 1f;
	//private Bounds plane;
	//private Bounds currentPlane;
	GameObject plane;
	GameObject currentPlane;

	void Start(){
		Vector3 center = camera.ViewportToWorldPoint(new Vector3(.5f, .5f, camera.farClipPlane));
		Vector3 leftUp = camera.ViewportToWorldPoint(new Vector3(0f, 0f, camera.farClipPlane));
		Vector3 rightDown = camera.ViewportToWorldPoint(new Vector3(1f,1f,camera.farClipPlane));
		Vector3 size = new Vector3(rightDown.x - leftUp.x, rightDown.y - leftUp.y, 12f);
		/*plane = new Bounds(center, size);
		plane.Expand(new Vector3(.01f, .01f, 0f));*/
		
		plane = GameObject.CreatePrimitive(PrimitiveType.Cube);
		plane.name = "Plane";
		plane.transform.position = center;
		plane.transform.localScale = size;
		plane.renderer.enabled = false;
		
		currentPlane = GameObject.CreatePrimitive(PrimitiveType.Cube);
		currentPlane.name = "currentPlane";
		currentPlane.transform.position = center;
		currentPlane.transform.localScale = size;
		currentPlane.renderer.enabled = false;
	}
	
	void Update(){
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
			
			transform.Translate(x,y,0);
			
			Vector3 center = camera.ViewportToWorldPoint(new Vector3(.5f, .5f, camera.farClipPlane));
			Vector3 leftUp = camera.ViewportToWorldPoint(new Vector3(0f, 0f, camera.farClipPlane));
			Vector3 rightDown = camera.ViewportToWorldPoint(new Vector3(1f,1f,camera.farClipPlane));
			Vector3 size = new Vector3(rightDown.x - leftUp.x, rightDown.y - leftUp.y, 0);
			//currentPlane = new Bounds(center, size);
			currentPlane.name = "currentPlane";
			currentPlane.transform.position = center;
			currentPlane.transform.localScale = size;
			currentPlane.renderer.enabled = false;
			
			/*if(!plane.collider.bounds.Contains(currentPlane.collider.bounds.min)){
				Debug.Log("Oh GoD!!");
				//Vector3 adjust = plane.collider.bounds.min - currentPlane.collider.bounds.min;
			}
			
			if(!plane.collider.bounds.Contains(currentPlane.collider.bounds.max)){
				Debug.Log("Oh GoD!!"); 
				//Vector3 adjust = plane.collider.bounds.max - currentPlane.collider.bounds.max;
			}*/
			
			if(!plane.collider.bounds.Contains(currentPlane.collider.bounds.min)||!plane.collider.bounds.Contains(currentPlane.collider.bounds.max)){
				Debug.Log("Oh GoD!!");
				transform.Translate(-x, -y, 0f);
			}
		}
	}
}
