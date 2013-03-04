/// <summary>
/// The inspector for the GA prefab.
/// </summary>

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using System;

[CustomEditor(typeof(GA_HeatMapRenderer))]
public class GA_HeatMapRendererInspector : Editor
{
	
	
	 void OnSceneGUI () {
		
		GA_HeatMapRenderer render = target as GA_HeatMapRenderer;
		if(!render.ShowValueLabels)
			return;
		
        Handles.color = Color.blue;
		var data = render.datafilter.GetData();
		foreach(GA_DataPoint p in data)
		{
         	Handles.Label(p.position+Vector3.up*1,p.count.ToString(),EditorStyles.toolbarTextField);
		}
		
	}
	
	void OnEnable () {
		
		GA_HeatMapRenderer render = target as GA_HeatMapRenderer;
		
		if(render.Histogram == null)
			render.Histogram = new GA_Histogram();
		
					
	}

	void HandleRenderdatafilterOnDataUpdate (GA_HeatMapDataFilterBase sender)
	{
		GA_HeatMapRenderer render = target as GA_HeatMapRenderer;
		render.OnDataUpdate();
	}
	Rect lastrect = new Rect(0f,0f,0f,0f);
	
	override public void OnInspectorGUI ()
	{
		GA_HeatMapRenderer render = target as GA_HeatMapRenderer;
		
		
		if(render == null || render.Histogram == null)
			return;
		
		if (!EditorUtility.IsPersistent (target ))
		{
			PrefabUtility.DisconnectPrefabInstance(render.gameObject);
		}
		
		EditorGUIUtility.LookLikeControls();
		EditorGUI.indentLevel = 1;
		
		
		EditorGUILayout.Space();
		
		if(render.transform.position != Vector3.zero)
		{
			// not placed in zero and heatmap data is off. It might be useful to move the heatmap to create layers, but we should warn the user.
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Heatmap is not in (0,0,0) - the visualization will be offset!");
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			if(GUILayout.Button("Reset position"))
				render.transform.position = Vector3.zero;
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.Space();
		}
		
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Histogram of dataset",EditorStyles.largeLabel);
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Shows the data point count for event occurrences in the dataset",EditorStyles.miniLabel);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.BeginHorizontal();
		
		
		
		GUILayout.Box("",GUIStyle.none,GUILayout.Width(20));//layout hack
		GUILayout.Box("",GUILayout.MinWidth(50),GUILayout.MaxWidth(700),GUILayout.MinHeight(200));
		if(Event.current.type == EventType.Repaint)
		{
			lastrect = GUILayoutUtility.GetLastRect();
		}
		
		GUILayout.Box("",GUIStyle.none,GUILayout.Width(15));//layout hack to get right margin
		
		if(render.Histogram != null && (render.Histogram.Data == null || render.Histogram.Data.Length==0))
		{
			GUI.Label(new Rect(lastrect.x+lastrect.width/2-50,lastrect.y+lastrect.height/2,100,40),"No data loaded",EditorStyles.largeLabel);
		}
		
		Vector2 textPos = new Vector2(lastrect.xMin+lastrect.height/2-63, lastrect.yMax-20);
		Vector2 textGuiPos = EditorGUIUtility.GUIToScreenPoint(textPos);
		GUIUtility.RotateAroundPivot(-90f,new Vector2(lastrect.xMin,lastrect.yMax));
		if (textGuiPos.y > 125)
		{
			int maxChars = ((int)textGuiPos.y - 125) / 5;
			GUI.Label(new Rect(textPos.x, textPos.y, 200, 20),"Number of data points".Substring(0, Mathf.Max(0, Mathf.Min(maxChars, 21))),EditorStyles.label);
		}
		GUIUtility.RotateAroundPivot(90f,new Vector2(lastrect.xMin,lastrect.yMax));
		GUI.Label(new Rect(lastrect.xMin+lastrect.width/2-85,lastrect.yMax+40,200,20),"Frequency / event occurrences",EditorStyles.label);
		
		if (render.Histogram.Data != null)
		{
			float margin = lastrect.width/(render.Histogram.Data.Length);
				
			int numLabels = 10;
			int labelInterval = Mathf.FloorToInt(render.Histogram.Data.Length/(float)numLabels);
			labelInterval = Mathf.Max(1,labelInterval);
				
			for(int i=0;i<render.Histogram.Data.Length;i++)
			{
				float lineWidth = Mathf.Max(2,margin-2);
				
				if (Screen.width > 90 + i * lineWidth)
				{
					float c = render.Histogram.Data[i]*lastrect.height;
					float x = lastrect.x+(i+0.5f)*margin;
					float y = lastrect.y+lastrect.height;
					float pct = i/(float)render.Histogram.Data.Length;
					
					Color nonSelectedColor = new Color(0.6f,0.6f,0.6f,0.3f);
					
					float rangePct = (pct-render.RangeMin)/(render.RangeMax-render.RangeMin);
						
					float line,linePct,colorPct,barHeight;
					Color color;
					for (int xline = 0; xline < lineWidth; xline++)
					{
						Vector2 guiPos = EditorGUIUtility.GUIToScreenPoint(new Vector2(x, y));
						barHeight = Mathf.Min(c, guiPos.y - 93); //cut bar height if too high for inspector window
						barHeight = Mathf.Max(0f, barHeight);
						line = x-lineWidth/2+xline;
						linePct = pct+1f/render.Histogram.Data.Length*(xline/lineWidth);
						colorPct = rangePct+1f/render.Histogram.Data.Length*(xline/lineWidth);
						color = Color.Lerp(render.MinColor,render.MaxColor,colorPct);
						if(linePct<=render.RangeMin || linePct>=render.RangeMax)
							color = nonSelectedColor;
						GA_GUIHelper.DrawLine(new Vector2(line, y), new Vector2(line, y-barHeight), color);
					}
				}
			}
		}
			
			
		/// 0% label
		Vector2 label = new Vector2(lastrect.xMin,lastrect.yMax+10);
		GUIUtility.RotateAroundPivot(50f,label);
		GUI.Label(new Rect(label.x,label.y,40,20),"0%",EditorStyles.miniLabel);
		GUIUtility.RotateAroundPivot(-50f,label);
		// 50% label
		label = new Vector2(lastrect.center.x,lastrect.yMax+10);
		GUIUtility.RotateAroundPivot(50f,label);
		GUI.Label(new Rect(label.x,label.y,40,20),"50%",EditorStyles.miniLabel);
		GUIUtility.RotateAroundPivot(-50f,label);
	
		// 100% label
		label = new Vector2(lastrect.xMax-14,lastrect.yMax+10);
		GUIUtility.RotateAroundPivot(50f,label);
		GUI.Label(new Rect(label.x,label.y,40,20),"100%",EditorStyles.miniLabel);
		GUIUtility.RotateAroundPivot(-50f,label);
		
		// 100% label. real value
		label = new Vector2(lastrect.xMax-40,lastrect.yMin-15);
		
		GUI.Label(new Rect(label.x,label.y,50,20),render.Histogram.RealDataMax.ToString("G5"),EditorStyles.miniLabel);
		
		// 0% label. real value
		label = new Vector2(lastrect.xMin+10,lastrect.yMin-15);
		GUI.Label(new Rect(label.x,label.y,50,20),render.Histogram.RealDataMin.ToString("G5"),EditorStyles.miniLabel);
			

		 GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Box("",GUIStyle.none,GUILayout.Width(8));//layout hack
		EditorGUILayout.MinMaxSlider(ref render.RangeMin,ref render.RangeMax,0f,1f,GUILayout.Width(lastrect.width+15));

		GUILayout.EndHorizontal();
		

		render.MinColor= EditorGUI.ColorField(new Rect(lastrect.xMin-10,lastrect.yMax+44,50,18),render.MinColor);
		render.MaxColor= EditorGUI.ColorField(new Rect(lastrect.xMax-45,lastrect.yMax+44,50,18),render.MaxColor);
		
		GUILayout.BeginHorizontal();
		GUILayout.Box("",GUIStyle.none,GUILayout.Height(70));//layout hack
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Show heatmap:");
		if (render.BillBoard != null)
			render.BillBoard.GetComponent<MeshRenderer>().enabled = EditorGUILayout.Toggle(render.BillBoard.GetComponent<MeshRenderer>().enabled);
		else
			EditorGUILayout.Toggle(true);
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Histogram scale:");
		GA_Histogram.HistogramScale oldScale = render.HistogramScale;
		
		render.HistogramScale = (GA_Histogram.HistogramScale)EditorGUILayout.EnumPopup(render.HistogramScale);
		
		if(render.HistogramScale != oldScale)
			render.OnScaleChanged();
		
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
		
		EditorGUILayout.PrefixLabel("Render model:");
		GA_HeatMapRenderer.RenderModel oldRenderModel = render.CurrentRenderModel;
		render.CurrentRenderModel = (GA_HeatMapRenderer.RenderModel)EditorGUILayout.EnumPopup(render.CurrentRenderModel);
		if(render.CurrentRenderModel != oldRenderModel)
			render.RenderModelChanged();
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Point radius:");
		
		float oldMaxRadius = render.MaxRadius;
		render.MaxRadius = EditorGUILayout.Slider(render.MaxRadius,0f,10f);
		if(render.MaxRadius != oldMaxRadius)
		{
			render.SetMaterialVariables();
		}
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Show values (slow):");
		render.ShowValueLabels = EditorGUILayout.Toggle(render.ShowValueLabels);
		GUILayout.EndHorizontal();
		
		EditorGUILayout.Space();
		
		if(GUI.changed)
		{
			if(SystemInfo.graphicsShaderLevel < 20)
			{
				GA.Log ("GameAnalytics: GPU does not support shader needed");
			}
			
			EditorUtility.SetDirty(target);
			render.SetMaterialVariables();
		}
	}
}