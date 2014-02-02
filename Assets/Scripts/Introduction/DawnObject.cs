using UnityEngine;
using System.Collections;

public class DawnObject : MonoBehaviour 
{
	public bool isInteractable;
	
	private Material interactMaterial;
	private GameObject interactField;
	
	private Color originalColor;
	private bool mouseOver;
	private Vector3 originalPositon;
	
	/* 
	 	To find object size:
		renderer.bounds.size
		collider.bounds.size
	*/
	
	// Use this for initialization
	void Start ()
	{
		originalColor = renderer.material.color;
		mouseOver = false;
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (mouseOver && Input.GetKeyDown(KeyCode.Space))
		{
			transform.position = new Vector3(originalPositon.x + 5, originalPositon.y + 5, originalPositon.z + 5);
			//Debug.Log("Begin interaction");
		}
	}
	
	void OnMouseEnter()
	{
		renderer.material.color = Color.white;
		mouseOver = true;
	}
	
	void OnMouseExit()
	{
		renderer.material.color = originalColor;
		mouseOver = false;
	}
}
