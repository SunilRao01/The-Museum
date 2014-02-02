using UnityEngine;
using System.Collections;

public class PlayerIntroduction : MonoBehaviour 
{
	public Texture customCursor;
	
	private Vector3 movementDirection;
	private Vector3 rotationDirection;
	
	public float moveSpeed;
	public float rotationSpeed;
	
	public bool lockMovement;
	
	// New FPS Controls
	public float speed = 10.0f;
	public float gravity = 10.0f;
	public float maxVelocityChange = 10.0f;
	
	void Awake () 
	{		
		// Sets up rigidbody
		rigidbody.freezeRotation = true;
	    rigidbody.useGravity = false;
		
		Screen.lockCursor = true;
		//Screen.showCursor = false;
	}
	
	void Update()
	{
		handleInput();
	}
	
	void FixedUpdate () 
	{
	    handleMovement();
	}
	
	void handleMovement()
	{
		if (lockMovement)
		{
			rigidbody.velocity = Vector3.zero;
		}
		
		// Movement
		
	        // Calculate how fast we should be moving
	        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
	        targetVelocity = transform.TransformDirection(targetVelocity);
	        targetVelocity *= speed;
 
	        // Apply a force that attempts to reach our target velocity
	        Vector3 velocity = rigidbody.velocity;
	        Vector3 velocityChange = (targetVelocity - velocity);
	        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
	        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
			velocityChange.y = Mathf.Clamp(velocityChange.y, -maxVelocityChange, maxVelocityChange);
	        //velocityChange.y = 0;
			
			if (!lockMovement)
			{
	        	rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
			}
 
	    
	    // Ignore gravity for introduction
	    rigidbody.AddForce(new Vector3 (0, -gravity * rigidbody.mass, 0));
		
		// Looking Around
		// Player Sight
		rotationDirection.x = -Input.GetAxis("Mouse Y") * rotationSpeed;
		rotationDirection.y = Input.GetAxis("Mouse X") * rotationSpeed;
		rotationDirection *= Time.deltaTime;
		
		transform.Rotate(rotationDirection);
		// Lock Z axis of sight movement
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
 		
	}
	
	private void handleInput()
	{		
		if (Input.GetMouseButtonDown(0))
		{
			Screen.lockCursor = true;
			//Screen.showCursor = false;
		}
		
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Screen.lockCursor = false;
			//Screen.showCursor = true;
		}
	}
	
	void OnGUI()
	{
		float leftPosition = Camera.main.pixelWidth/2;
		float rightPosition = Camera.main.pixelHeight/2;
		
		//GUI.drawTexture(Rect( (Screen.width - myTextureWidth)*0.5,(Screen.height - myTextureHeight)*0.5, myTextureWidth, myTextureHeight), myTexture);
		GUI.DrawTexture(new Rect(leftPosition - 64, rightPosition - 82, 128, 128), customCursor);
	}

}
