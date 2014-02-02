using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour 
{
	public float moveSpeed;
	public float moveForce;
	
	private Vector3 movementDirection;
	private float zDirection;
	
	// Use this for initialization
	void Start () 
	{
		zDirection = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () 
	{
		handleMovement();
		
		handleRotation();
	}
	
	void handleMovement()
	{
		movementDirection.y = Input.GetAxis("Vertical");
		movementDirection.x = Input.GetAxis("Horizontal");
		movementDirection.z = 0;
		movementDirection *= moveSpeed;
		movementDirection *= Time.deltaTime;
		
		rigidbody.AddForce(movementDirection * moveForce);
		
		// Lock z-movement
		transform.position = new Vector3(transform.position.x, transform.position.y, zDirection);
	}
	
	void handleRotation()
	{
		if (movementDirection != Vector3.zero)
		{
			//transform.TransformDirection(movementDirection);
			//transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
			//transform.LookAt(movementDirection);
		}
	}
}
