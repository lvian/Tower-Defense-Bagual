using UnityEngine;
using UnityEditor;

public class GA_Menu : MonoBehaviour
{
	[MenuItem ("Window/GameAnalytics/Select GA_Settings", false, 0)]
    static void SelectGASettings ()
    {
        Selection.activeObject = GA.Settings;
    }
	
	[MenuItem ("Window/GameAnalytics/GA Setup Wizard", false, 100)]
    static void SetupAndTour ()
    {
        GA_SetupWizard wizard = ScriptableObject.CreateInstance<GA_SetupWizard>();
		wizard.ShowUtility();
		wizard.position = new Rect(150, 150, 420, 340);
	}
	
	[MenuItem ("Window/GameAnalytics/GA Example Tutorial", false, 101)]
    static void ExampleTutorial ()
    {
        GA_ExampleTutorial tutorial = ScriptableObject.CreateInstance<GA_ExampleTutorial>();
		tutorial.ShowUtility();
		tutorial.position = new Rect(150, 150, 420, 340);
	}
	
	[MenuItem ("Window/GameAnalytics/Create GA_SystemTracker", false, 200)]
    static void AddGASystemTracker ()
    {
        if (FindObjectOfType(typeof(GA_SystemTracker)) == null)
		{
			GameObject systemTracker = Instantiate(Resources.Load("Prefabs/GA_SystemTracker", typeof(GameObject))) as GameObject;
			systemTracker.name = "GA_SystemTracker";
			Selection.activeObject = systemTracker;
		}
		else
		{
			GA.LogWarning("A GA_SystemTracker already exists in this scene - you should never have more than one per scene!");
		}
    }
	
	[MenuItem ("Window/GameAnalytics/Create GA_Heatmap", false, 201)]
    static void AddHeatMap ()
    {
		GameObject heatmap = Instantiate(Resources.Load("Prefabs/GA_HeatMap", typeof(GameObject))) as GameObject;
		heatmap.name = "GA_HeatMap";
		Selection.activeObject = heatmap;
    }
	
	[MenuItem ("Window/GameAnalytics/Add GA_Tracker to Object", false, 202)]
    static void AddGATracker ()
    {
		if (Selection.activeGameObject != null)
		{
			if (Selection.activeGameObject.GetComponent<GA_Tracker>() == null)
			{
				Selection.activeGameObject.AddComponent<GA_Tracker>();
			}
			else
			{
				GA.LogWarning("That object already contains a GA_Tracker component.");
			}
		}
		else
		{
			GA.LogWarning("You must select the gameobject you want to add the GA_Tracker component to.");
		}
    }
	
	[MenuItem ("Window/GameAnalytics/Open GA_Status Window", false, 300)]
    static void OpenGAStatus ()
    {
        GA_Status status = ScriptableObject.CreateInstance<GA_Status>();
		status.Show();
	}
}