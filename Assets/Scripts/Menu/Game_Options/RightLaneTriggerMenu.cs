using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightLaneTriggerMenu : MonoBehaviour {

	public AvatarMenu managerLane;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Lane")) 
		{
            managerLane.rightLane = other.gameObject.transform;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Lane") 
		{
			managerLane.rightLane = null;
		}
	}
}
