import UnityEngine

class mainMenuBoo (MonoBehaviour):
	public enum State:
		Setup
		Main
		Options
		Credits
		Ready
	
	public background as Texture2D
	public logo as Texture2D
	public newGameSound as AudioClip
	public optionSound as AudioClip
	public creditSound as AudioClip
	public exitSound as AudioClip
	
	private _state as State
	private _ready as bool

	def Start ():
		_state = State.Setup
	
	def Update ():
		if Input.GetKeyDown("escape"):
			if _state == State.Options :
				_state = State.Main
			if _state == State.Credits :
				_state = State.Main
				
		if Time.realtimeSinceStartup > 0.0F:
			_ready = true

	def OnGUI():
		if _state == State.Setup:
			setup()
			_state = State.Main
			
		if _state == State.Main:
			main()
			
		if _state == State.Options:
			options()
			
		if _state == State.Credits:
			credits()
			
		if _state == State.Ready:
			//AudioSource.PlayClipAtPoint(newGameSound, transform.position , 1)
			Application.LoadLevel("FoodFactory")
		
	def setup():
		_ready = false
		_state = State.Options
		
	def main():
		if background != null:
			GUI.DrawTexture(Rect(0,0,Screen.width, Screen.height), background)
		if logo != null:
			GUI.DrawTexture(Rect(Screen.width/2 -75, Screen.height * 0.01F, 150, 150),logo)
		
		GUI.BeginGroup(Rect(Screen.width /2 -100, Screen.height/2 -67.5F , 200 , 135 ))
		
		#Make a box at the center of t he screen
		GUI.Box(Rect(0,0,200,135), "Main Menu")
			
		# Options button
		if GUI.Button(Rect( 10 , 30 , 180 , 30), "Options"):
			//AudioSource.PlayClipAtPoint(optionSound, transform.position , 1)
			_state = State.Options
			
		# Credits button
		if GUI.Button(Rect( 10 , 65 , 180 , 30), "Credits"):
			//AudioSource.PlayClipAtPoint(creditSound, transform.position , 1)
			_state = State.Credits
			
		# Feedback button
		if GUI.Button(Rect( 10 , 100 , 180 , 30), "Feedback"):
			Application.OpenURL("https://www.facebook.com/SturdyDef")
			
		GUI.EndGroup()
		
		if _ready:
			if GUI.Button(Rect( Screen.width /2 - 90, Screen.height - 50, 180 , 30), "Play"):
				_state = State.Ready
		
	def options():
		if background != null:
			GUI.DrawTexture(Rect(0,0,Screen.width , Screen.height), background)
		if logo != null:
			GUI.DrawTexture(Rect(Screen.width/2 -75, Screen.height * 0.01F, 150, 150),logo)
			
		GUI.BeginGroup(Rect(Screen.width /2 -150, Screen.height/2 -150 , 300 ,300 ))
		
		GUI.Box(Rect(0,0,300,300), "Options")
		
		# Dificulty option
		#dificultyOptions = ["Beginner", "Easy", "Hard" ]
		#dificultyOption  = PlayerPrefs.GetInt("Dificulty", 0);
		#if ++dificultyOption >= dificultyOptions.Count:
		#		dificultyOption = 0
		#GUI.Label(Rect( 10 , 30 , 80 , 30), "Dificulty:")
		#if GUI.Button(Rect( 120 , 25 , 100 , 30), dificultyOptions[dificultyOption] as string):
		#	PlayerPrefs.SetInt("Dificulty", dificultyOption)
		
		# Mute option
		muteOptions = ["ON", "OFF"]
		muteOption  = PlayerPrefs.GetInt("Mute", 0);
		if ++muteOption >= muteOptions.Count:
				muteOption = 0
		GUI.Label(Rect( 10 , 30 , 50 , 30), "Mute")
		if GUI.Button(Rect( 120 , 30 , 100 , 30), muteOptions[muteOption] as string):
			PlayerPrefs.SetInt("Mute", muteOption)
		
		#GUI.Label(Rect( 10 , 100 , 80 , 30), "Audio:")
		# Background volume option
		GUI.Label(Rect( 10 , 65 , 80 , 30), "Volume")
		audioVolume = PlayerPrefs.GetFloat("AudioVolume",100.0F)
		audioVolume = GUI.HorizontalSlider(Rect(120, 70, 100, 30), audioVolume, 0.0F, 100.0F)
		PlayerPrefs.SetFloat("AudioVolume", audioVolume)
		
		# back button
		if GUI.Button(Rect( 10 , 260 , 280 , 30), "< Go Back"):
			_state = State.Main
		
		
		GUI.EndGroup()
		
		
	def credits():
		if background != null:
			GUI.DrawTexture(Rect(0,0,Screen.width , Screen.height), background)
		if logo != null:
			GUI.DrawTexture(Rect(Screen.width/2 -75, Screen.height * 0.01F, 150, 150),logo)
		
		GUI.BeginGroup(Rect(Screen.width /2 -255, Screen.height/2 -50 - 105, 250 , 100 ))
		
		GUI.Box(Rect(0,0,250,100), "Programmers")
		GUI.Label(Rect( 10 , 30 , 180 , 30), "Leandro Vian")
		GUI.Label(Rect( 10 , 65 , 180 , 30), "Carlos Colombo")
		
		GUI.EndGroup()
		
		GUI.BeginGroup(Rect(Screen.width /2 + 5, Screen.height/2 -50 - 105 , 250 , 100 ))
		
		GUI.Box(Rect(0,0,250,100), "Art")
		GUI.Label(Rect( 10 , 30 , 230 , 30), "Juliano Silveira Britto")
		GUI.Label(Rect( 10 , 65 , 230 , 30), "Pablo Alexandre Silveira Medeiros")
		
		GUI.EndGroup()
		
		GUI.BeginGroup(Rect(Screen.width /2 -125, Screen.height/2 -50, 250 , 100 ))
		GUI.Box(Rect(0,0,250,100), "Special Thanks")
		GUI.Label(Rect( 10 , 30 , 180 , 30), "Johnny Carvalho")
		
		GUI.EndGroup()
		
		if GUI.Button(Rect( 50, Screen.height - 50, 30 , 30), "<"):
				_state = State.Main