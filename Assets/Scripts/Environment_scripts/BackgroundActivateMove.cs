using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundActivateMove : MonoBehaviour {

	public GameObject BlockToMove;
	public KeyCode keyToPress;

	public BackgroundMove backgroundMove;

	private bool playerIn;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (keyToPress) && playerIn) 
		{
			Debug.Log ("ça marche");
			backgroundMove.enabled = true;
		}
	}

	void OnTriggerEnter (Collider coll)
	{
		if (coll.gameObject.tag == "Player") 
		{
			playerIn = true;

		}
	}
	void OnTriggerExit (Collider coll)
	{
		if (coll.gameObject.tag == "Player") 
		{
			playerIn = false;

		}
	}
}
