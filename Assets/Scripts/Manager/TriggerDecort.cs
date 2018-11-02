using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDecort : MonoBehaviour {

	public GameObject[] gameObjectActivate;

	void ActivateScript()
	{
		for(int i = 0; i < gameObjectActivate.Length; i++)
		{
			gameObjectActivate [i].SetActive (true);
		}	
	}

	public void DesactivateScript()
	{
		for(int i = 0; i < gameObjectActivate.Length; i++)
		{
			gameObjectActivate [i].SetActive (false);
		}	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			ActivateScript();
		}
	}
}


