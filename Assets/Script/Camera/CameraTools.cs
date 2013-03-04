using UnityEngine;
using System.Collections;

public class CameraTools : MonoBehaviour {
	
	public Vector3 ScreenToWorld( Vector2 screenPos ){
	    // Create a ray going into the scene starting
	    // from the screen position provided
	    Ray ray = camera.ScreenPointToRay( screenPos );
	    RaycastHit hit;
	
	    // ray hit an object, return intersection point
		int layerMask = 1 << 8;
	    if( Physics.Raycast( ray, out hit, 100f, layerMask) ){
	       	return hit.point;
		}
	
	    // ray didn't hit any solid object, so return the
	    // intersection point between the ray and
	    // the Y=0 plane (horizontal plane)
	    float t = -ray.origin.y / ray.direction.y;
	    return ray.GetPoint( t );
	}
}
