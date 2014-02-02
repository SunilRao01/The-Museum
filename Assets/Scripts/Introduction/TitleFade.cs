using UnityEngine;
using System.Collections;

public class TitleFade : MonoBehaviour 
{
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (guiText.color.a > 0)
		{
			guiText.color = new Color(guiText.color.r, guiText.color.g, guiText.color.b, 
				(guiText.color.a - 0.03f));
		}
	}
}
