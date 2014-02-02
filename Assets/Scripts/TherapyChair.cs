using UnityEngine;
using System.Collections;

public class TherapyChair : MonoBehaviour 
{
	private float promptAlpha = 0;
	public GUIStyle promptStyle;
	private string promptText;
	
	private float dialogueAlpha = 1;
	
	private bool canSit = false;
	private bool canLeave = false;
	
	private Vector3 originalPlayerPosition;
	
	public bool sessionComplete = false;
	
	// Use this for initialization
	void Start () 
	{
		promptText = "Press E to begin therapy";
	}
	

	
	// Update is called once per frame
	void Update () 
	{
		Transform playerPosition = GameObject.Find("Player").GetComponent<Transform>();
		Collider playerCollider = GameObject.Find("Player").GetComponent<Collider>();
		
		// Once all dialogue is finished
		if (GameObject.Find("Dialogue").GetComponent<Dialogue>().complete && !sessionComplete)
		{
			promptAlpha = 1;
			promptText = "Press E to exit";
			canLeave = true;			
		}
		
		if (Input.GetKeyDown(KeyCode.E) && canSit)
		{			
			// Record player position (used when exiting chair)
			originalPlayerPosition = playerPosition.position;
			
			// Lock camera rotation
			FirstPersonCamera cameraScript = GameObject.Find("Main Camera").GetComponent<FirstPersonCamera>();
			cameraScript.lockRotation = true;
			Camera.main.transform.eulerAngles = new Vector3(0, 180, 0);
			
			// Lock player movement
			Player playerScript = GameObject.Find("Player").GetComponent<Player>();
			playerScript.lockMovement = true;
			
			// Stop velocity of player
			Rigidbody playerBody = GameObject.Find("Player").GetComponent<Rigidbody>();
			playerBody.velocity = Vector3.zero;
			
			// Disable chair collider 
			Physics.IgnoreCollision(gameObject.GetComponentInChildren<MeshCollider>(), playerCollider, true);
			
			// Set player position to chair view
			playerPosition.position = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z - 12);
			canSit = false;
			
			// Remove prompt
			promptAlpha = 0;
			
			// Start Dialogue when player sits in chair
			GameObject.Find("Dialogue").GetComponent<Dialogue>().start();
			
		}
		
		if (Input.GetKeyDown(KeyCode.E) && canLeave)
		{
			// Move player back to original position
			playerPosition.position = originalPlayerPosition;
			
			// Unlock camera rotation
			FirstPersonCamera cameraScript = GameObject.Find("Main Camera").GetComponent<FirstPersonCamera>();
			cameraScript.lockRotation = false;
			
			// Unlock player movement
			Player playerScript = GameObject.Find("Player").GetComponent<Player>();
			playerScript.lockMovement = false;
			
			// Enable chair collider 
			Physics.IgnoreCollision(gameObject.GetComponentInChildren<MeshCollider>(), playerCollider, false);
			
			// Set player position to chair view
			playerPosition.position = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z - 12);
			
			// Remove prompt
			promptAlpha = 0;			
			
			sessionComplete = true;
		}
		
		if (sessionComplete)
		{
			promptAlpha = 0;
			canSit = false;
			canLeave = false;
		}

	}
	
	
	
	void OnGUI()
	{
		GUI.color = new Color(GUI.color.r, GUI.color.g, dialogueAlpha);
		
		// Sit Prompt
		GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, promptAlpha);
		GUI.Label(new Rect((Screen.width/2) - 250, (Screen.height/2) - 50, 500, 100), promptText, promptStyle);
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			// If player enters interact zone
			promptAlpha = 1;
			canSit = true;
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			promptAlpha = 0;
			canSit = false;
		}
	}
}
