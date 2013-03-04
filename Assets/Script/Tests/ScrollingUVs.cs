using UnityEngine;
using System.Collections;
 
public class ScrollingUVs : MonoBehaviour 
{
    public int materialIndex = 0;
    public Vector2 uvAnimationRate;
    public string textureName = "_MainTex";
 
    Vector2 uvOffset = Vector2.zero;
 
    void LateUpdate() 
    {
        uvOffset += ( uvAnimationRate * Time.deltaTime );
        if( renderer.enabled )
        {
			//Debug.Log(renderer.materials[ materialIndex ].mainTexture);
            renderer.material.SetTextureOffset (textureName, uvOffset);
        }
    }
}