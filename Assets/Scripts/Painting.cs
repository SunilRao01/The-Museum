using UnityEngine;
using System.Collections;
using System.Linq;

public class Painting : MonoBehaviour 
{
	// Player
	private Player player;
	
	// Alpha
	private float promptAlpha;
	//private float textAlpha;
		
	// Zen
	public int maxZen;
	private int currentZen = 0;
	
	// Word Processing
	public string paintingIdea;
	public string [] validWords;
	private ArrayList processedWords;
	private bool finished;
	
	// GUI Styles
	private string promptText;
	public GUIStyle promptStyle;
	public GUIStyle zenStyle;
	
	// Painting/Text
	public Material painting;
	
	// Text Input
	public TextMesh textInput;
	public Material textMaterial;
	private bool canType;
	private bool hasEntered;
	private bool allComplete;
	
	
	void Start ()
	{
		player = GameObject.Find("Player").GetComponent<Player>();
		
		allComplete = false;
		renderer.material = painting;
		//renderer.material.color = Color.grey;
		//textMaterial.color = new Color(0.8f, 0.8f, 0.8f, 1);
		
		processedWords = new ArrayList();
		finished = false;
		canType = false;

		promptText = "Press spacebar to interact";
		hasEntered = false;
		
		//textAlpha = 0;
	}
	
	void Update ()
	{		
		
		// Puts all player input into 3d text
		foreach (char c in Input.inputString)
		{
			if (canType)
			{
				// Valid keys for input
				
				// Backspace key
				if (c == "\b"[0]) 
				{
					if (textInput.text.Length != 0)
					{
						textInput.text = textInput.text.Substring(0, textInput.text.Length - 1);
					}
				}
				// Return key
				else if (c == "\n"[0] || c == "\r"[0])
				{
					checkPhrase(textInput.text);
					textInput.text = "";
				}
				// Every other key is valid
				else
				{
					textInput.text += c;
				}
			}
		}
		
		// Painting is finished when the word count == valid words count (Needs to be changed)
		if (currentZen >= maxZen)
		{
			finished = true;
		}
		
		// Checks if painting is finished
		if (finished)
		{
			renderer.material.color = new Color(0.1f, 0.1f, 0.1f, 1);
			if (!allComplete)
			{
				promptText = "Painting complete press Shift to exit";
				promptStyle.normal.textColor = Color.white;
				player.addWord(paintingIdea);
				
			}
			allComplete = true;
			
			canType = false;
		}
		else
		{
			renderer.material.color = new Color(0.65f, 0.65f, 0.65f, 1);
		}
		
		handleInput();
	}
	
	void handleInput()
	{
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			if (hasEntered)
			{				
				if (player.lockMovement)
				{
					player.lockMovement = false;
					promptText = "";
				}
				else
				{
					player.lockMovement = true;
				}
				
				if (!finished)
				{
					if (canType)
					{
						//textInput.color = Color.grey;
						canType = false;
					}
					else if (!canType)
					{
						//textInput.color = Color.black;
						promptText = "Press shift to exit";
						canType = true;
					}
				}
			}
		}
	}
	
	
	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && !finished)
		{
			promptAlpha = 1;
			promptText = "Press shift to interpret";
			hasEntered = true;
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if (!finished)
			{
				promptAlpha = 0;
				hasEntered = false;
			}
			else if (finished)
			{
				promptText = "";
				promptAlpha = 0;
				hasEntered = false;
			}
		}
	}
	
	void OnGUI()
	{		
		Color inColor = new Color(Color.black.r, Color.black.g, Color.black.b, promptAlpha);
		Color outColor = new Color(Color.white.r, Color.white.g, Color.white.b, promptAlpha);
		
		ShadowAndOutline.DrawOutline(new Rect((Screen.width/2), (Screen.height/2), 500, 100), promptText, 
			promptStyle, outColor, inColor, 1);
		
		
		string zenText = currentZen + "/" + maxZen;
		ShadowAndOutline.DrawOutline(new Rect((Screen.width/2), (Screen.height/10), 500, 100), zenText, zenStyle, outColor, inColor, 1);

	}
	
	// Checks if inputted word is valid
	void checkPhrase(string input)
	{
		string text = input;
		
		// Checks if VALID word
		for (int i = 0; i < validWords.Length; i++)
		{
			if (text == validWords[i])
			{
				
				processedWords.Add(validWords[i]);
				player.addZen(ref currentZen);
				
				// Remove element from array
				validWords = validWords.Where(w => w != validWords[i]).ToArray();
				
				return;
			}
			
		}
	}
	
	
	
	
}
