using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	private Vector3 movementDirection;
	private Vector3 rotationDirection;
		
	public float rotationSpeed;
	
	public ArrayList currentWords;
	public GUIText words;
	public GUIText zenGUI;
	private Color zenOriginalColor;
	
	public bool lockMovement;	
	
	// New FPS Controls
	public float moveSpeed = 10.0f;
	public float moveForce;
	
	public float zen;
	
	
	public bool hasKey = false;
	
	private float lerpTime = 0;
		
	public bool debug;
	
	//public ArrayList goals;
	public System.Collections.Generic.List<string> goals;
	
	// Notebook/Goals
	public Texture notebookTexture;
	private float notebookAlpha = 0;
	public GUIStyle notebookStyle;
	private string notebookText;
	
	
	void Awake () 
	{
	    // TODO: Get player's words
		// TODO: Get player's destinations (tab to open)
		
		goals.Add("To-do\n\n");
		
		if (PlayerPrefs.GetString("ideas") == null)
		{
			
		}
		else
		{
			
		}
		
		if (PlayerPrefs.GetString("goals") == null)
		{
			
		}
		else
		{
			goals.Add("Test objective 1");
			goals.Add("Test objective 2");
			goals.Add("Test objective 3");
		}
		
		zen = 0;
		
		if (zenGUI)
		{
			zenOriginalColor = zenGUI.color;
		}
		
		// Sets up rigidbody
	    //rigidbody.useGravity = false;
		
		currentWords = new ArrayList();
		
		Screen.lockCursor = true;
		Screen.showCursor = false;
	}
	
	void FixedUpdate () 
	{
	    handleMovement();
	}
	
	void Update()
	{
		handleInput();
		
		notebookText =  string.Join("\n", goals.ToArray());
		
		
		if (debug)
		{
			moveSpeed = 300;
		}
		
		lerpTime += Time.time;
	}
	
	void OnGUI()
	{
		GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, notebookAlpha);
		GUI.DrawTexture(new Rect((Screen.width/2) - 250, (Screen.height/2) - 250, 500, 500), notebookTexture);
		
		GUI.Label(new Rect((Screen.width/2) - 250 + 60, (Screen.height/2) - 250 + 100, 450, 450), notebookText, notebookStyle);
	}
	
	public void addZen(ref int currentZen)
	{
		currentZen++;
		
		// Show little effect
	}

	
	void handleMovement()
	{
		Vector3 moveDirection = new Vector3(0, 0, 0);
		
		if (Mathf.Abs(Input.GetAxis("Vertical")) > 0 && Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
		{
			moveDirection = (Camera.main.transform.forward * Input.GetAxisRaw("Vertical")) + (Camera.main.transform.right * Input.GetAxisRaw("Horizontal"));
		}
		else if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
		{
			moveDirection = Camera.main.transform.right * Input.GetAxisRaw("Horizontal");
		}
		else if (Mathf.Abs(Input.GetAxis("Vertical")) > 0)
		{
			moveDirection = Camera.main.transform.forward * Input.GetAxisRaw("Vertical");
		}
		
		moveDirection.y = 0;
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection.Normalize();
		moveDirection *= moveSpeed;
		
		if (!lockMovement)
		{
        	rigidbody.AddForce(moveDirection * Time.deltaTime * moveForce);
		}
 
        /*// Jumping
        if (canJump && Input.GetButton("Jump")) 
		{
            rigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
        }*/

 
	}
	

	
	private void handleInput()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Screen.lockCursor = false;
			Screen.showCursor = true;
		}
		
		// Handle notebook
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			if (notebookAlpha == 0)
			{
				notebookAlpha = 1;
			}
			else
			{
				notebookAlpha = 0;
			}
		}
		
		if (Input.GetMouseButtonDown(0))
		{
			Screen.lockCursor = true;
			Screen.showCursor = false;
		}
	}
	
	public void addWord(string word)
	{
		currentWords.Add(word);
		words.text += word + "\n";
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Painting"))
		{
			zenGUI.color = new Color(zenGUI.color.r, zenGUI.color.g, zenGUI.color.b, 1);
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Painting"))
		{
			zenGUI.color = new Color(zenGUI.color.r, zenGUI.color.g, zenGUI.color.b, 0);
		}
	}
}
