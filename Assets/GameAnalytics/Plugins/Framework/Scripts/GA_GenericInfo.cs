/// <summary>
/// This class handles user ID, session ID, time stamp, and sends a user message, optionally including system specs, when the game starts
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using System.Net;

#if !UNITY_WEBPLAYER
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
#endif

public  class GA_GenericInfo
{
	#region public values
	
	/// <summary>
	/// The ID of the user/player. A unique ID will be determined the first time the player plays. If an ID has already been created for a player this ID will be used.
	/// </summary>
	public  string UserID
	{
		get {
			if (_userID == null && !GA.Settings.CustomUserID)
			{
				if (PlayerPrefs.HasKey("GA_uid"))
				{
					_userID = PlayerPrefs.GetString("GA_uid");
				}
				else
				{
					_userID = GetUserUUID();
					PlayerPrefs.SetString("GA_uid", _userID);
					PlayerPrefs.Save();
				}
			}
			return _userID;
		}
	}
	
	/// <summary>
	/// The ID of the current session. A unique ID will be determined when the game starts. This ID will be used for the remainder of the play session.
	/// </summary>
	public  string SessionID
	{
		get {
			if (_sessionID == null)
			{
				_sessionID = GetSessionUUID();
			}
			return _sessionID;
		}
	}
	
	/// <summary>
	/// The current UTC date/time in seconds
	/// </summary>
	public  string TimeStamp
	{
		get {
			return ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000).ToString();
		}
	}
	
	#endregion
	
	#region private values
	
	private  string _userID;
	private  string _sessionID;
	private  bool _settingUserID;
	
	#endregion
	
	#region public methods
	
	/// <summary>
	/// Gets generic system information at the beginning of a play session
	/// </summary>
	/// <param name="inclSpecs">
	/// Determines if all the system specs should be included <see cref="System.Bool"/>
	/// </param>
	/// <returns>
	/// The message to submit to the GA server is a dictionary of all the relevant parameters (containing user ID, session ID, system information, language information, date/time, build version) <see cref="Dictionary<System.String, System.Object>"/>
	/// </returns>
	public  List<Dictionary<string, object>> GetGenericInfo(string root)
	{
		List<Dictionary<string, object>> systemspecs = new List<Dictionary<string, object>>();
		
		/*
		 * Apple does not allow tracking of device specific data:
		 * "You may not use analytics software in your application to collect and send device data to a third party"
		 * - iOS Developer Program License Agreement: http://www.scribd.com/doc/41213383/iOS-Developer-Program-License-Agreement
		 */
		
		#if !UNITY_IPHONE
		
		systemspecs.Add(AddSystemSpecs("unity_wrapper", GA_Settings.VERSION, root));
		systemspecs.Add(AddSystemSpecs("os", SystemInfo.operatingSystem, root));
		systemspecs.Add(AddSystemSpecs("processor_type", SystemInfo.processorType, root));
		systemspecs.Add(AddSystemSpecs("gfx_name", SystemInfo.graphicsDeviceName, root));
		systemspecs.Add(AddSystemSpecs("gfx_version", SystemInfo.graphicsDeviceVersion, root));
		
		// Unity provides lots of additional system info which might be worth tracking for some games:
		//systemspecs.Add(AddSystemSpecs("process_count", SystemInfo.processorCount.ToString(), root));
		//systemspecs.Add(AddSystemSpecs("sys_mem_size", SystemInfo.systemMemorySize.ToString(), root));
		//systemspecs.Add(AddSystemSpecs("gfx_mem_size", SystemInfo.graphicsMemorySize.ToString(), root));
		//systemspecs.Add(AddSystemSpecs("gfx_vendor", SystemInfo.graphicsDeviceVendor, root));
		//systemspecs.Add(AddSystemSpecs("gfx_id", SystemInfo.graphicsDeviceID.ToString(), root));
		//systemspecs.Add(AddSystemSpecs("gfx_vendor_id", SystemInfo.graphicsDeviceVendorID.ToString(), root));
		//systemspecs.Add(AddSystemSpecs("gfx_shader_level", SystemInfo.graphicsShaderLevel.ToString(), root));
		//systemspecs.Add(AddSystemSpecs("gfx_pixel_fillrate", SystemInfo.graphicsPixelFillrate.ToString(), root));
		//systemspecs.Add(AddSystemSpecs("sup_shadows", SystemInfo.supportsShadows.ToString(), root));
		//systemspecs.Add(AddSystemSpecs("sup_render_textures", SystemInfo.supportsRenderTextures.ToString(), root));
		//systemspecs.Add(AddSystemSpecs("sup_image_effects", SystemInfo.supportsImageEffects.ToString(), root));
		
		#else
		
		systemspecs.Add(AddSystemSpecs("os", "iOS", root));
		
		#endif
		
		return systemspecs;
	}
	
	/// <summary>
	/// Gets a universally unique ID to represent the user. User ID should be device specific to allow tracking across different games on the same device:
	/// -- Android uses deviceUniqueIdentifier.
	/// -- iOS/PC/Mac uses the first MAC addresses available.
	/// -- Webplayer uses deviceUniqueIdentifier.
	/// Note: The unique user ID is based on the ODIN specifications. See http://code.google.com/p/odinmobile/ for more information on ODIN.
	/// </summary>
	/// <returns>
	/// The generated UUID <see cref="System.String"/>
	/// </returns>
	public  string GetUserUUID()
	{
		#if UNITY_WEBPLAYER
		
		return SystemInfo.deviceUniqueIdentifier;
		
		#else
		
		NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
		string mac = "";

        foreach (NetworkInterface adapter in nics)
        {
        	PhysicalAddress address = adapter.GetPhysicalAddress();
			if (address.ToString() != "" && mac == "")
			{
				byte[] bytes = Encoding.UTF8.GetBytes(address.ToString());
				SHA1CryptoServiceProvider SHA = new SHA1CryptoServiceProvider();
				mac = BitConverter.ToString(SHA.ComputeHash(bytes)).Replace("-", "");
			}
		}
		return mac;
		
		#endif
	}
	
	/// <summary>
	/// Gets a universally unique ID to represent the session.
	/// </summary>
	/// <returns>
	/// The generated UUID <see cref="System.String"/>
	/// </returns>
	public  string GetSessionUUID()
	{
		return Guid.NewGuid().ToString();
	}
	
	/// <summary>
	/// Do not call this method (instead use GA_static_api.Settings.SetCustomUserID)! Only the GA class should call this method.
	/// </summary>
	/// <param name="customID">
	/// The custom user ID - this should be unique for each user
	/// </param>
	public  void SetCustomUserID(string customID)
	{
		_userID = customID;
	}
	
	#endregion
	
	#region private methods
	
	/// <summary>
	/// Adds detailed system specifications regarding the users/players device to the parameters.
	/// </summary>
	/// <param name="parameters">
	/// The parameters which will be sent to the server <see cref="Dictionary<System.String, System.Object>"/>
	/// </param>
	private  Dictionary<string, object> AddSystemSpecs(string key, string type, string root)
	{
		string addRoot = "";
		if (root != "")
			addRoot = root + ":";
		
		Dictionary<string, object> parameters = new Dictionary<string, object>()
		{
			{ GA_ServerFieldTypes.Fields[GA_ServerFieldTypes.FieldType.EventID], addRoot + "system:" + key },
			{ GA_ServerFieldTypes.Fields[GA_ServerFieldTypes.FieldType.Message], type }
		};
		
		return parameters;
	}
	
	/// <summary>
	/// Gets the users system type
	/// </summary>
	/// <returns>
	/// String determining the system the user is currently running <see cref="System.String"/>
	/// </returns>
	private  string GetSystem()
	{
		#if UNITY_STANDALONE_OSX
		return "MAC";
		#endif

		#if UNITY_STANDALONE_WIN
		return "PC";
		#endif
		
		#if UNITY_WEBPLAYER
		return "WEBPLAYER";
		#endif
		
		#if UNITY_WII
		return "WII";
		#endif
		
		#if UNITY_IPHONE
		return "IPHONE";
		#endif

		#if UNITY_ANDROID
		return "ANDROID";
		#endif
		
		#if UNITY_PS3
		return "PS3";
		#endif

		#if UNITY_XBOX360
		return "XBOX";
		#endif
		
		#if UNITY_FLASH
		return "FLASH";
		#endif
	}
	
	#endregion
}