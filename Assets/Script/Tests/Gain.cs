using UnityEngine;
using System.Collections;

public class Gain : MonoBehaviour {
	
	public float gain = 1;
	
	void OnAudioFilterRead(float[] data, int channels) {
    for (int i = 0; i < data.Length; ++i)
        data[i] = data[i] * gain;            
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
