using UnityEngine;
using System.Collections;

public class IdeaEntrance : MonoBehaviour 
{
	private KeyInformation keyInfo;
	public GUIStyle promptStyle;
	private string promptText;
	private float promptAlpha = 0;
	private bool canEnter = false;
	
	// Use this for initialization
	void Start () 
	{
		keyInfo = GameObject.Find("Keys").GetComponent<KeyInformation>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (keyInfo.currentKeys < keyInfo.totalKeys)
		{
			promptText = "You need " + (keyInfo.totalKeys - keyInfo.currentKeys) + " more keys";
		}
		else if (keyInfo.currentKeys == keyInfo.totalKeys)
		{
			promptText = "Press E to enter";
			canEnter = true;
		}
		
		if (canEnter && Input.GetKeyDown(KeyCode.E))
		{
			Destroy(gameObject);
		}
	}
	
	void OnGUI()
	{
		// Enter Prompt
		GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, promptAlpha);
		GUI.Label(new Rect((Screen.width/2) - 250, (Screen.height/2) - 50, 500, 100), promptText, promptStyle);
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "Player")
		{
			promptAlpha = 1;
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.name == "Player")
		{
			promptAlpha = 0;
		}
	}
}
