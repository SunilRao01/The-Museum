using UnityEngine;
using System.Collections;

public class IntroductionRift : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			Application.LoadLevel("Trip Introduction");
		}
	}
}
