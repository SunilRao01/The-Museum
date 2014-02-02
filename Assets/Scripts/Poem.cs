using UnityEngine;
using System.Collections;

public class Poem : MonoBehaviour 
{
	public Player player;
	private bool hasEntered;
	
	// Prompts
	private string promptText;
	private int promptAlpha;
	public GUIStyle promptStyle;
	
	// Use this for initialization
	void Start () 
	{
		promptText = "Press Shift to interact";
		
		hasEntered = false;
	}
	
	// Update is called once per frame
	void Update()
	{
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
					//promptText = "";
				}
				else
				{
					player.lockMovement = true;
				}
				
				/*if (!finished)
				{
					if (canType)
					{
						textInput.color = Color.grey;
						canType = false;
					}
					else if (!canType)
					{
						textInput.color = Color.black;
						promptText = "Press shift to exit";
						canType = true;
					}
				}*/
			}
		}
	}
	
	void OnGUI()
	{
		GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, promptAlpha);
		GUI.Label(new Rect((Screen.width/2), (Screen.height/2), 500, 100), promptText, promptStyle);
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			promptAlpha = 1;
			hasEntered = true;
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			promptAlpha = 0;
			hasEntered = false;
		}
	}
}
