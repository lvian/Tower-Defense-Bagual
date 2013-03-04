using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(GA_SpecialEvents))]
[RequireComponent(typeof(GA_Gui))]

public class GA_SystemTracker : MonoBehaviour {

	#region public values
	
	public static GA_SystemTracker GA_SYSTEMTRACKER;
	
	public bool UseForSubsequentLevels;
	
	public bool IncludeSystemSpecs;
	public bool IncludeSceneChange;
	public bool SubmitErrors;
	public int MaxErrorCount = 10;
	public bool SubmitErrorStackTrace;
	public bool SubmitErrorSystemInfo;
	public bool SubmitFpsAverage;
	public bool SubmitFpsCritical;
	public int FpsCriticalThreshold = 30;
	public int FpsCirticalSubmitInterval = 10;
	public bool GuiEnabled;
	public bool GuiAllowScreenshot;

	public bool ErrorFoldOut;
	
	#endregion
	
	#region unity derived methods
	 
	/// <summary>
	/// Setup involving other components
	/// </summary>
	public void Start ()
	{
		if (GA_SYSTEMTRACKER != null)
		{
			// only one system tracker allowed per scene
			GA.LogWarning("Destroying dublicate GA_SystemTracker - only one is allowed per scene!");
			Destroy(gameObject);
			return;
		}
		GA_SYSTEMTRACKER = this;
		
		if (UseForSubsequentLevels)
			DontDestroyOnLoad(gameObject);
		
		GA_Gui gui = GetComponent<GA_Gui>();
		gui.GuiAllowScreenshot = GuiAllowScreenshot;
		gui.GuiEnabled = GuiEnabled;
		
		GA.API.Debugging.SubmitErrors = SubmitErrors;
		GA.API.Debugging.SubmitErrorStackTrace = SubmitErrorStackTrace;
		GA.API.Debugging.SubmitErrorSystemInfo = SubmitErrorSystemInfo;
		GA.API.Debugging.MaxErrorCount = MaxErrorCount;
		
		if(GA.API.Debugging.SubmitErrors) //Warning this registerLogCallback is slow because it saves the stacktraces
			Application.RegisterLogCallback(GA.API.Debugging.HandleLog);
		
		// Add system specs to the submit queue
		if (IncludeSystemSpecs)
		{
			List<Dictionary<string, object>> systemspecs = GA.API.GenericInfo.GetGenericInfo("");
			
			foreach (Dictionary<string, object> spec in systemspecs)
			{
				GA_Queue.AddItem(spec, GA_Submit.CategoryType.GA_Log, false);
			}
		}
	}
	
	void OnDestroy()
	{
		if (GA_SYSTEMTRACKER == this)
			GA_SYSTEMTRACKER = null;	
	}
	
	#endregion
}
