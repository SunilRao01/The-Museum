using UnityEngine;
using System.Collections;

public class Idea : MonoBehaviour 
{
	public string nextLevel;
	
	public GUIStyle promptStyle;
	public string promptText;
	public float promptAlpha = 0;
	
	private bool canTeleport = false;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (canTeleport && Input.GetKeyDown(KeyCode.E))
		{
			Application.LoadLevel(nextLevel);
		}
	}
	
	void OnGUI()
	{
		// Sit Prompt
		GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, promptAlpha);
		GUI.Label(new Rect((Screen.width/2) - 250, (Screen.height/2) - 50, 500, 100), promptText, promptStyle);
		
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "Player")
		{
			canTeleport = true;
			promptAlpha = 1;
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.name == "Player")
		{
			canTeleport = false;
			promptAlpha = 0;
		}
	}
}
