using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class ObstacleDialogue : MonoBehaviour 
{
	public string solution;
	
	// Dialogue Box
	public Texture2D dialogueBoxImage;
	private float _alpha = 0;
	private string dialogue;
	public GUIStyle dialogueStyle;
	public float dialogueWidth;
	public float dialogueHeight;
	
	// Variables
	public bool complete = false;
	public bool active = false;
	
	// Scrolling Text Effect
	public float letterPause = 0.1f;
	public AudioClip letterSound;
	private AudioClip sound;
	
	private string dialogueText;
	
	private bool scrollComplete;
	
	private dialoguePiece[] dialogues;
	public string[] stringArray;
	private int iterator = 0;
	
	// Text input field
	private float tryAgainLabelAlpha = 0;
	private float inputAlpha = 0;
	private string playerInput = "";
	
		
	// Use this for initialization
	void Start () 
	{	
		dialogues = new dialoguePiece[stringArray.Length];
		for (int i = 0; i < stringArray.Length; i++)
		{
			dialogues[i].dialogueText = stringArray[i];
		}
		
		scrollComplete = false;
		
		// Starting dialogue
		dialogue = dialogues[iterator].dialogueText;

		sound = letterSound;
	}
	
	void Update () 
	{
		if (_alpha == 1 || inputAlpha == 1)
		{
			active = true;
		}
		
		if (iterator == stringArray.Length-1 && !complete)
		{
			inputAlpha = 1;
			
			GetComponent<Obstacle>().promptAlpha = 1;
			GetComponent<Obstacle>().promptText = "Press Escape to exit";
		}
		
		// Progress dialogue with the 'E' key
		if (!complete)
		{
			// Handles Input
			if (Input.GetKeyDown(KeyCode.E) && scrollComplete)
			{
				iterator++;
				dialogueText = "";
				
				if (iterator < stringArray.Length)
				{
					dialogue = dialogues[iterator].dialogueText;
					StartCoroutine(TypeText ());
				}
			}
		}

	}
	
	public void startDialogue()
	{
		scrollComplete = false;

		
		// Starting dialogue
		iterator = 0;
		dialogue = dialogues[iterator].dialogueText;
				
		_alpha = 1;
		inputAlpha = 0;
		dialogueText = "";
		StartCoroutine(TypeText());
	}
	
	void OnGUI()
	{
		// Make GUI visible/invisible
		GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, _alpha);
		// Display Dialogue Box
		GUI.Box(new Rect((Screen.width/2) - 250, (Screen.height/2) + 150, 600, 120), "");
		// Dialogue text
		GUI.Label(new Rect((Screen.width/2) - 200, (Screen.height/2) + 170, dialogueWidth, dialogueHeight), dialogueText, dialogueStyle);
	
		// Try again label when player inputs incorrect solution
		GUIStyle labelStyle = dialogueStyle;
		//labelStyle.normal.textColor = Color.red;
		GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, tryAgainLabelAlpha);
		GUI.Label(new Rect((Screen.width/2) - 10, (Screen.height/2) + 220, 140, 30), "Try again", labelStyle);
		
		// Modify visibility of text field where appropriate
		GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, inputAlpha);
		GUI.SetNextControlName("player input");
		// Player input text field
		playerInput = GUI.TextField(new Rect((Screen.width/2) - 30, (Screen.height/2) + 220, 140, 30), playerInput);
		playerInput = Regex.Replace(playerInput, @"[^a-z]", "");
		
		if (inputAlpha == 1 && _alpha == 1)
		{
			GUI.FocusControl("player input");
		}
		
		// Create focus for game
		GUI.SetNextControlName("game");
		
		// Handle player input
		Event e = Event.current;
		Player playerScript = GameObject.Find("Player").GetComponent<Player>();
		// If player presses 'enter/return'
		if (inputAlpha == 1 && e.keyCode == KeyCode.Return) 
		{
			// If correct
			if (playerInput == solution && playerScript.currentWords.Contains(solution))
			{
				GetComponent<Obstacle>().obstacleComplete = true;
				complete = true;
				
				// Update level key information
				GameObject.Find("Goal").GetComponent<KeyInformation>().currentKeys++;
				
				tryAgainLabelAlpha = 0;
				
				// Make everything disappear (prompt, dialogue box, input text field)
				_alpha = 0;
				inputAlpha = 0;
				tryAgainLabelAlpha = 0;
				iterator = 0;
				GetComponent<Obstacle>().promptAlpha = 0;
				playerInput = "";
				
				// Shift focus to game
				GUI.FocusControl("game");
				
				// Unlock camera/player movement
				FirstPersonCamera cameraScript = GameObject.Find("Main Camera").GetComponent<FirstPersonCamera>();
				cameraScript.lockRotation = false;
				
				// Lock player movement
				playerScript.lockMovement = false;
				
				active = false;
			}
			// If wrong
			else
			{
				// If the solution is incorrect, clear text field
				playerInput = "";
				tryAgainLabelAlpha = 0.5f;
				
			}
   		}
		// If player presses the escape key
		else if (inputAlpha == 1 && e.keyCode == KeyCode.Escape)
		{	
			// Make everything disappear (dialogue box, input text field)
			_alpha = 0;
			inputAlpha = 0;
			tryAgainLabelAlpha = 0;
			iterator = 0;
			playerInput = "";
			
			// Shift focus to game
			GUI.FocusControl("game");
			
			// Unlock camera/player movement
			FirstPersonCamera cameraScript = GameObject.Find("Main Camera").GetComponent<FirstPersonCamera>();
			cameraScript.lockRotation = false;
			
			// Lock player movement
			playerScript.lockMovement = false;
			
			active = false;
			
			GetComponent<Obstacle>().promptText = "Press E to engage " + gameObject.name;
			GetComponent<Obstacle>().promptAlpha = 0;
		}
	}
	
	IEnumerator TypeText () 
	{
		scrollComplete = false;
		int count = 1;
		
		foreach (char letter in dialogue.ToCharArray()) 
		{
			dialogueText += letter;
			
			if (sound && count % 10 == 0)
			{
				//audio.PlayOneShot (sound);
				yield return 0;
			}
			yield return new WaitForSeconds (letterPause);
			
			count++;
		}
		
		scrollComplete = true;
	}
	
	
	
}
