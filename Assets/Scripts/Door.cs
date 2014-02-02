using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour 
{
	public int zenAmount;
	
	public GameObject door;
	private bool canUnlock = false;
	private bool hasEntered = false;
	
	// Use this for initialization
	void Start () 
	{
		GetComponent<TextMesh>().text = zenAmount.ToString();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Find player from hierarchy
		GameObject player = GameObject.Find("Player");
		Player playerScript = (Player) player.GetComponent(typeof(Player));
		
		// Check if player can open the door via a zen check
		if (playerScript.zen >= zenAmount)
		{
			canUnlock = true;
		}
		else
		{
			canUnlock = false;
		}
		
		// Handles opening of door
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			if (hasEntered && canUnlock)
			{
				GetComponent<TextMesh>().text = "";
				door.collider.enabled = false;
				door.renderer.enabled = false;
			}
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			hasEntered = true;
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			hasEntered = false;
		}
	}
}
