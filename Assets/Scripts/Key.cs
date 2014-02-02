using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {

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
			// Find player from hierarchy
			GameObject player = GameObject.Find("Player");
			Player playerScript = (Player) player.GetComponent(typeof(Player));
			
			playerScript.hasKey = true;
			
			Destroy(gameObject);
		}
	}
}
