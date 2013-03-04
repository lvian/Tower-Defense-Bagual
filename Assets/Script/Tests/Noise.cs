using UnityEngine;
using System;  // Needed for Random
public class Noise : MonoBehaviour
{
  // un-optimized version of a noise generator
  private System.Random RandomNumber = new System.Random();
  public float offset = 0;
  void OnAudioFilterRead(float[] data, int channels)
  {
    for (int i = 0; i < data.Length; i++)
    {
      data[i] =  offset -1.0f + (float)RandomNumber.NextDouble()*2.0f;
    }
  }
}