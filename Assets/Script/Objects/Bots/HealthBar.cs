using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	
	private Transform target;
	private Vector3 pos;
	private Bot bot;
	public 	float maxHealth;
	public 	float curHealth;
	public 	float curHealthPercentage;
	
	
	// Use this for initialization
	void Start () {
		target = transform.parent;
		bot = target.GetComponent<Bot>();
		maxHealth = bot.getAttributeValue(BotAttributeName.MaxHealth);
	}
	
	// Update is called once per frame
	void Update () {
		curHealth = bot.getVitalValue(BotVitalsName.Health);
		if (target != null){
			curHealthPercentage  = (curHealth / maxHealth) * 100;
			if( curHealthPercentage < 75 && curHealthPercentage > 25)
			{
				renderer.material.SetColor("_TintColor", new Color( 1 , 1 , 0, 0.2f));
			}
			if( curHealthPercentage < 25)
			{
				renderer.material.SetColor("_TintColor", new Color( 1 , 0 , 0 , 0.2f));
			}
			if( curHealthPercentage <= 0)
			{
				renderer.enabled = false;
			}
			
			
		}
		
	}
	/*
	void OnGUI(){
		float maxHealth;
		float curHealth;
		Bot bot = null;
		if( target != null)
			bot = target.GetComponent<Bot>();
		if(bot == null){
			//Debug.Log("Bot is null");
			return;
		}
		else{
			maxHealth = bot.getAttributeValue(BotAttributeName.MaxHealth);
			
		}
		
		if(curHealth <= 0)
			return;
		
		// draw health bar background
		GUI.DrawTexture (new Rect(
			(pos.x * cam.GetScreenWidth())-(sizeX/2),
			(cam.GetScreenHeight() - (pos.y * cam.GetScreenHeight())) + target.GetComponentInChildren<SkinnedMeshRenderer>().bounds.size.y * (sizeY/2), 
			sizeX,
			sizeY), 
			background);
		
		//draw health bar foreground
		GUI.DrawTexture (new Rect(
			(pos.x * cam.GetScreenWidth())-(sizeX/2),
			(cam.GetScreenHeight() - (pos.y * cam.GetScreenHeight())) + target.GetComponentInChildren<SkinnedMeshRenderer>().bounds.size.y * (sizeY/2),
			sizeX * (curHealth / maxHealth),
			sizeY),
			foreground);
	}
	*/
}
