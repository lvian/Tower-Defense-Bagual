using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	
	public float minFOV = 25;
	public float maxFOV = 50;
	public float speed = 30f;
	public GameObject camBox;
	
	private BoxCollider _boxCollider;
	private float 		_height;
	
	
	void Start(){
		_boxCollider = camBox.GetComponent<BoxCollider>();
		_height = transform.position.y;
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
			
			
			float x = 0,z = 0;
			if(Input.GetMouseButton(1)){
				x = -Input.GetAxis("Mouse X") * Time.deltaTime * speed;
				z = -Input.GetAxis("Mouse Y") * Time.deltaTime * speed;
			}
			// [right]   [left]
			//  [top]   [bottom]
			Vector3[][] camSides = new Vector3[][]{
				new Vector3[] {camera.ViewportToWorldPoint(new Vector3(.5f,0,camera.nearClipPlane)), camera.ViewportToWorldPoint(new Vector3(.5f,1,camera.nearClipPlane))},
				new Vector3[] {camera.ViewportToWorldPoint(new Vector3(0,.5f,camera.nearClipPlane)), camera.ViewportToWorldPoint(new Vector3(1,.5f,camera.nearClipPlane))}
			};
			
			/*Vector3[][] camSides = new Vector3[][]{
			    new Vector3[] {camera.ViewportToWorldPoint(new Vector3(.5f,0,camera.farClipPlane)), camera.ViewportToWorldPoint(new Vector3(.5f,1,camera.farClipPlane))},
			    new Vector3[] {camera.ViewportToWorldPoint(new Vector3(0,.5f,camera.farClipPlane)), camera.ViewportToWorldPoint(new Vector3(1,.5f,camera.farClipPlane))}
			   };*/
			
			
			// going right
			if(x <= 0){
				Vector3 right = transform.position;
				right.x += x;
				if(_boxCollider.bounds.Contains(transform.position)){
					transform.Translate(x, 0, 0);
					transform.position = new Vector3(transform.position.x, _height, transform.position.z);
				}	
			}
			
			//going left
			if(x >= 0){
				Vector3 left = transform.position;
				left.x += x;
				if(_boxCollider.bounds.Contains(transform.position)){
					transform.Translate(x, 0, 0);
					transform.position = new Vector3(transform.position.x, _height, transform.position.z);
				}
			}
			
			// going south
			if(z <= 0){
				Vector3 south = transform.position;
				south.z += z;
				if(_boxCollider.bounds.Contains(transform.position)){
					transform.Translate(0, 0, z);
					transform.position = new Vector3(transform.position.x, _height, transform.position.z);
				}	
			}
			
			//going north
			if(z >= 0){
				Vector3 north = transform.position;
				north.z += z;
				if(_boxCollider.bounds.Contains(transform.position)){
					transform.Translate(0, 0, z);
					transform.position = new Vector3(transform.position.x, _height, transform.position.z);
				}
			}
			
			
			
			if(_boxCollider.bounds.Contains(transform.position)){
				//transform.Translate(0, 0, z);
				transform.position = new Vector3(transform.position.x, _height, transform.position.z);
			}
			else{
				//recalculate x position so it gets back inside the collider
				Vector3 closestpoint = _boxCollider.ClosestPointOnBounds(transform.position);
				Debug.Log("closest  "+closestpoint);
				Debug.Log("transform  "+transform.position);
				transform.position = closestpoint;
			}
		}
	}
}
