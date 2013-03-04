using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class GA_Status : EditorWindow
{
	private GUIContent _resetButton 			= new GUIContent("Reset Values", "Resets all the \"Messages Sent\" values.");
	private GUIContent _selectSettingsButton 	= new GUIContent("Select GA_Settings", "Select the GA_Settings object.");
	
	void OnGUI ()
	{
		GUILayout.Label("GameAnalytics Status", EditorStyles.whiteLargeLabel);
		
		GUILayout.Space(5);
		
		GUILayout.Label("Setup Status", EditorStyles.boldLabel);
		
		GUILayout.BeginHorizontal();
	    GUILayout.Label("Game Key inserted:", GUILayout.Width(145));
		GUI.enabled = false;
		GUILayout.Toggle(GA.Settings.GameKey != "", "");
		GUI.enabled = true;
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
		GUILayout.Label("Secret Key inserted:", GUILayout.Width(145));
		GUI.enabled = false;
		GUILayout.Toggle(GA.Settings.SecretKey != "", "");
		GUI.enabled = true;
		GUILayout.EndHorizontal();
		
		if (GUILayout.Button(_selectSettingsButton, GUILayout.Width(163)))
		{
			Selection.activeObject = GA.Settings;
		}
		
		GUILayout.Space(5);
		
		GUILayout.Label("Events Sent", EditorStyles.boldLabel);
		
		GUILayout.BeginHorizontal();
	    GUILayout.Label("Total Succeeded:", GUILayout.Width(145));
		GUILayout.Label(GA.Settings.TotalMessagesSubmitted.ToString());
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
	    GUILayout.Label("Total Failed:", GUILayout.Width(145));
		GUILayout.Label(GA.Settings.TotalMessagesFailed.ToString());
		GUILayout.EndHorizontal();
		
		EditorGUILayout.Space();
		
		GUILayout.BeginHorizontal();
	    GUILayout.Label("Design Succeeded:", GUILayout.Width(145));
		GUILayout.Label(GA.Settings.DesignMessagesSubmitted.ToString());
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
	    GUILayout.Label("Design Failed:", GUILayout.Width(145));
		GUILayout.Label(GA.Settings.DesignMessagesFailed.ToString());
		GUILayout.EndHorizontal();
		
		EditorGUILayout.Space();
		
		GUILayout.BeginHorizontal();
	    GUILayout.Label("Quality Succeeded:", GUILayout.Width(145));
		GUILayout.Label(GA.Settings.QualityMessagesSubmitted.ToString());
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
	    GUILayout.Label("Quality Failed:", GUILayout.Width(145));
		GUILayout.Label(GA.Settings.QualityMessagesFailed.ToString());
		GUILayout.EndHorizontal();
		
		EditorGUILayout.Space();
		
		GUILayout.BeginHorizontal();
	    GUILayout.Label("Business Succeeded:", GUILayout.Width(145));
		GUILayout.Label(GA.Settings.BusinessMessagesSubmitted.ToString());
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
	    GUILayout.Label("Business Failed:", GUILayout.Width(145));
		GUILayout.Label(GA.Settings.BusinessMessagesFailed.ToString());
		GUILayout.EndHorizontal();
		
		EditorGUILayout.Space();
		
		GUILayout.BeginHorizontal();
	    GUILayout.Label("User Succeeded:", GUILayout.Width(145));
		GUILayout.Label(GA.Settings.UserMessagesSubmitted.ToString());
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
	    GUILayout.Label("User Failed:", GUILayout.Width(145));
		GUILayout.Label(GA.Settings.UserMessagesFailed.ToString());
		GUILayout.EndHorizontal();
		
		GUILayout.Space(3);
		
		if (GUILayout.Button(_resetButton, GUILayout.Width(163)))
		{
			GA.Settings.TotalMessagesSubmitted = 0;
			GA.Settings.TotalMessagesFailed = 0;
			
			GA.Settings.DesignMessagesSubmitted = 0;
			GA.Settings.DesignMessagesFailed = 0;
			GA.Settings.QualityMessagesSubmitted = 0;
			GA.Settings.QualityMessagesFailed = 0;
			GA.Settings.BusinessMessagesSubmitted = 0;
			GA.Settings.BusinessMessagesFailed = 0;
			GA.Settings.UserMessagesSubmitted = 0;
			GA.Settings.UserMessagesFailed = 0;
			
			EditorUtility.SetDirty(GA.Settings);
		}
    }
	
	void OnInspectorUpdate()
	{
		EditorUtility.SetDirty(GA.Settings);
		Repaint();
	}
}
