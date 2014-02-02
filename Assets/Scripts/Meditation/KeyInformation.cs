using UnityEngine;
using System.Collections;

public class KeyInformation : MonoBehaviour 
{
	public float totalKeys;
	public float currentKeys;
	
	public GUIText keyLabel;
	private string keyText;
	public GUIStyle keyStyle;
	
	public bool unlock = false;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		keyText = "Keys: " + currentKeys + "/" + totalKeys;
				
		if (currentKeys == totalKeys)
		{
			unlock = true;
		}
	}
	
	void OnGUI()
	{
		ShadowAndOutline.DrawOutline(new Rect(10, 10, 100, 50), keyText, keyStyle, Color.white, Color.black, 1);
	}
}
