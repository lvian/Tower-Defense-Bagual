/// <summary>
/// This class handles special events unique to the Unity Wrapper, such as submitting level/scene changes, and delaying application quit
/// until data has been sent.
/// </summary>

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GA_SystemTracker))]

public class GA_SpecialEvents : MonoBehaviour
{
	/*[HideInInspector]
	public bool SubmitFpsAverage;
	[HideInInspector]
	public bool SubmitFpsCritical;
	[HideInInspector]
	public bool IncludeSceneChange;
	[HideInInspector]
	public int FpsCriticalThreshold;
	[HideInInspector]
	public int FpsSubmitInterval;*/
	#region private values
	
	private float _lastLevelStartTime = 0f;
	
	private int _frameCountAvg = 0;
	private float _lastUpdateAvg = 0f;
	private int _frameCountCrit = 0;
	private float _lastUpdateCrit = 0f;
	
	private GA_SystemTracker _systemTracker;
	
	#endregion
	
	#region unity derived methods
	
	public void Start ()
	{
		_systemTracker = GetComponent<GA_SystemTracker>();
		
		SceneChange();
		StartCoroutine(SubmitFPSRoutine());
	}
	private IEnumerator SubmitFPSRoutine()
	{
		while(Application.isPlaying)
		{
			SubmitCriticalFPS();
			yield return new WaitForSeconds(_systemTracker.FpsCirticalSubmitInterval);
		}
	}
	public void Update()
	{
		//average FPS
		if (_systemTracker.SubmitFpsAverage)
		{
			_frameCountAvg++;
		}
		
		//critical FPS
		if (_systemTracker.SubmitFpsCritical)
		{
			_frameCountCrit++;
		}
	}
	
	public void OnLevelWasLoaded ()
	{
		SceneChange();
	}
	
	public void OnApplicationQuit ()
	{
#if UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
		if (!GA_Queue.QUITONSUBMIT)
		{
			SubmitAverageFPS();
		}
#endif
	}
	
	public void SubmitAverageFPS()
	{
		//average FPS
		if (_systemTracker.SubmitFpsAverage)
		{
			float timeSinceUpdate = Time.time - _lastUpdateAvg;
			float fpsSinceUpdate = _frameCountAvg / timeSinceUpdate;
			_lastUpdateAvg = Time.time;
			_frameCountAvg = 0;
			
			if (fpsSinceUpdate > 0)
			{
				if (GA.Settings.TrackTarget != null)
					GA.API.Quality.NewEvent("GA:AverageFPS", ((int)fpsSinceUpdate).ToString(), GA.Settings.TrackTarget.position);
				else
					GA.API.Quality.NewEvent("GA:AverageFPS", ((int)fpsSinceUpdate).ToString());
			}
		}
	}
	
	public void SubmitCriticalFPS()
	{
		//critical FPS
		if (_systemTracker.SubmitFpsCritical)
		{
			float timeSinceUpdate = Time.time - _lastUpdateCrit;
			float fpsSinceUpdate = _frameCountCrit / timeSinceUpdate;
			_lastUpdateCrit = Time.time;
			_frameCountCrit = 0;
			
			if (fpsSinceUpdate <= _systemTracker.FpsCriticalThreshold)
			{
				if (GA.Settings.TrackTarget != null)
					GA.API.Quality.NewEvent("GA:CriticalFPS", _frameCountCrit.ToString(), GA.Settings.TrackTarget.position);
				else
					GA.API.Quality.NewEvent("GA:CriticalFPS", _frameCountCrit.ToString());
				
			}
		}
	}
	
	#endregion
	
	#region private methods
	
	private void SceneChange()
	{
		SubmitAverageFPS();
		
		if (_systemTracker.IncludeSceneChange)
		{
			if (GA.Settings.TrackTarget != null)
				GA.API.Design.NewEvent("GA:LevelStarted", Time.time - _lastLevelStartTime, GA.Settings.TrackTarget.position);
			else
				GA.API.Design.NewEvent("GA:LevelStarted", Time.time - _lastLevelStartTime);
		}
		_lastLevelStartTime = Time.time;
	}
	
	#endregion
	
}
