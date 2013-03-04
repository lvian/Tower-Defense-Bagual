using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using LitJson;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(GA_HeatMapRenderer))]
[ExecuteInEditMode]
public class GA_HeatMapDataFilter : GA_HeatMapDataFilterBase
{
	[HideInInspector]
	public List<string> AvailableTypes;
	[HideInInspector]
	public int CurrentTypeIndex;
	
	[HideInInspector]
	public List<string> AvailableBuilds;
	[HideInInspector]
	public int CurrentBuildIndex;
	
	public bool RedownloadDataOnPlay;
	
	public bool IgnoreDates = true;
	
	//Pretty silly hack to force Unity to serialize DateTime. 
	//(It makes sense that it doesn't serialize structs, but there might be a better workaround.
	[SerializeField]
	private string _startDateTime;
	public DateTime? StartDateTime
	{
		get {
			DateTime res = DateTime.Now;
			DateTime.TryParse(_startDateTime,out res);
			return res;
		}
		set {_startDateTime = value.HasValue?value.Value.ToString():"";}
	}
	
	[SerializeField]
	private string _endDateTime;
	public DateTime? EndDateTime
	{
		get {
			DateTime res = DateTime.Now;
			DateTime.TryParse(_endDateTime,out res);
			return res;
		}
		set {_endDateTime = value.HasValue?value.Value.ToString():"";}
	}
	
	[HideInInspector]
	public List<string> AvailableAreas;
	[HideInInspector]
	public int CurrentAreaIndex;
	
	[HideInInspector]
	public List<string> AvailableEvents;
	[HideInInspector]
	public List<bool> CurrentEventFlag;
	
	[HideInInspector]
	public GA_HeatmapData DataContainer;
	[SerializeField]
	private bool didInit = false;
	
	public void OnEnable ()
	{	
		if(didInit)
			return;
		
		Debug.Break();
		AvailableBuilds = new List<string>();
		AvailableAreas = new List<string>();
		AvailableTypes = new List<string>();
		AvailableEvents = new List<string>();
		 
		AvailableTypes.Add("Design");
		AvailableTypes.Add("Quality");
		AvailableTypes.Add("Business");

		StartDateTime = DateTime.Now;
		EndDateTime = DateTime.Now;
		
		didInit = true;
	}

	
	void Awake()
	{
		if(Application.isPlaying && !Application.isEditor && RedownloadDataOnPlay)
		{
			DownloadData();
		}
	}
	
	public override List<GA_DataPoint> GetData()
	{
		return DataContainer!=null?DataContainer.Data:null;
	}
	
	public float LoadProgress;
	public bool Loading;
	
	private IEnumerator FollowProgress(WWW progress)
	{
		Loading = true;
		while(!progress.isDone)
		{
			LoadProgress = progress.progress;
			yield return null;
		}
		Loading = false;
		LoadProgress = 0;
		
	}
	
	void NormalizeDataPoints (List<GA_DataPoint> Data)
	{
		float dataMax = 0,dataMin = 0;
		foreach(GA_DataPoint p in Data)
		{
			if(dataMax < p.count)
			{
				dataMax = p.count;
			}
			if(dataMin > p.count)
			{
				dataMin = p.count;
			}			
		}
		
		foreach(GA_DataPoint p in Data)
		{
			p.density = (p.count - dataMin) / (dataMax - dataMin);
			
		}  
	}
	
	public void OnSuccessDownload(GA_Request.RequestType requestType, JsonData jsonList, GA_Request.SubmitErrorHandler errorEvent)
	{
		if(DataContainer == null) 
		{
			var dataContainerObject = new GameObject("GA_Data");
			dataContainerObject.transform.parent = transform;
			DataContainer = dataContainerObject.AddComponent<GA_HeatmapData>();

			DataContainer.Data = new List<GA_DataPoint>();
			GA.Log(DataContainer);
		}
		else
			DataContainer.Data.Clear();
		
		for (int i = 0; i < jsonList["x"].Count; i++)
		{
			try
			{
				GA_DataPoint p = new GA_DataPoint();
				p.position = new Vector3(float.Parse(jsonList["x"][i].ToString()), float.Parse(jsonList["y"][i].ToString()), float.Parse(jsonList["z"][i].ToString()));
				p.count = int.Parse(jsonList["value"][i].ToString());
				DataContainer.Data.Add(p);
			} 
			catch
			{
				// JSON format error
				GA.LogError("GameAnalytics: Error in parsing JSON data from server");
			}
		}
		
		NormalizeDataPoints (DataContainer.Data);
		
		GetComponent<GA_HeatMapRenderer>().OnDataUpdate();
		Loading = false;
	}

	public void OnErrorDownload(string msg)
	{
		GA.Log("GameAnalytics: Download failed: "+msg);
		Loading = false;
	}
	
	public  void DownloadData()
	{
		List<string> events = new List<string>();
		
		for(int i = 0; i<AvailableEvents.Count;i++)
		{
			if(CurrentEventFlag[i])
				events.Add(AvailableEvents[i]);
		}
		
		bool cancel = false;
		
		if(CurrentAreaIndex>=AvailableAreas.Count)
		{
			GA.LogWarning("GameAnalytics: Warning: Area selected is out of bounds. Not downloading data.");
			cancel = true;
		}
		
		if(CurrentBuildIndex>=AvailableBuilds.Count)
		{
			GA.LogWarning("GameAnalytics: Warning: Build selected is out of bounds. Not downloading data.");
			cancel = true;
		}
		
		if(cancel)
			return;
		
		GA.API.Request.RequestHeatmapData(events, AvailableAreas[CurrentAreaIndex],IgnoreDates?null:StartDateTime,IgnoreDates?null:EndDateTime, OnSuccessDownload, OnErrorDownload);
	}
	
	// Update is called once per frame
	public void DownloadUpdate ()
	{
		DownloadData();
	}
	
	public void OnSuccessDownloadInfo(GA_Request.RequestType requestType, JsonData jsonList, GA_Request.SubmitErrorHandler errorEvent)
	{
		GA.Log("Succesful index downloaded");
		CurrentAreaIndex = 0;
		CurrentTypeIndex = 0;
		CurrentBuildIndex = 0;
		StartDateTime = DateTime.Now.AddDays(-14);
		EndDateTime = DateTime.Now;
		AvailableEvents = new List<string>();
		CurrentEventFlag = new List<bool>();
		AvailableAreas = new List<string>();
		AvailableBuilds = new List<string>();
		IgnoreDates = true;
	
		for (int i = 0; i < jsonList["event_id"].Count; i++)
		{
			try
			{
				string name;
				name = jsonList["event_id"][i].ToString();
				AvailableEvents.Add(name);	
				CurrentEventFlag.Add(false);
				
			}
			catch
			{
				// JSON format error
				GA.LogError("GameAnalytics: Error in parsing JSON data from server");
			}
		}
		

		for (int j = 0; j < jsonList["area"].Count; j++)
		{
			try
			{
				string name;
				name = jsonList["area"][j].ToString();
				AvailableAreas.Add(name);		
				
			}
			catch
			{
				// JSON format error
				GA.LogError("GameAnalytics: Error in parsing JSON data from server");
			}
		}
		
		
		for (int k = 0; k < jsonList["build"].Count; k++)
		{
			try
			{
				string name;
				name = jsonList["build"][k].ToString();
				AvailableBuilds.Add(name);		
				
			}
			catch
			{
				// JSON format error
				GA.LogError("GameAnalytics: Error in parsing JSON data from server");
			}
		}
	}

	public void OnErrorDownloadInfo(string msg)
	{
		GA.Log("GameAnalytics: Download failed: "+msg);
	}
	
	public void UpdateIndexData ()
	{
		GA.Log("Downloading index from server");
		GA.API.Request.RequestGameInfo(OnSuccessDownloadInfo, OnErrorDownloadInfo);
	}
}
