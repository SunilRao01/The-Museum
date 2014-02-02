using UnityEngine;
using System.Collections;

public class Monologue : MonoBehaviour 
{
	public float maxDistance;
	
	public bool xDirection;
	public bool yDirection;
	public bool zDirection;
	
	private bool mouseOn;
	public bool debugMode;
	
	// Use this for initialization
	void Start () 
	{
		Screen.lockCursor = true;
		Screen.showCursor = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (debugMode)
		{
			mouseOn = true;
		}
		
		if (mouseOn && xDirection)
		{
			transform.position = new Vector3(transform.position.x + (Mathf.Sin(Time.time) * maxDistance), transform.position.y, transform.position.z);
		}
		if (mouseOn && yDirection)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y + (Mathf.Sin(Time.time) * maxDistance), transform.position.z);
		}
		if (mouseOn && zDirection)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (Mathf.Sin(Time.time) * maxDistance));
		}
		
		Debug.Log(mouseOn.ToString());
	}
	
	void OnMouseOver()
	{
		mouseOn = true;
	}
}
