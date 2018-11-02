using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider_Cine : MonoBehaviour {

	public Avatar_Cine avatar;

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Lane")) 
		{
			StartCoroutine (avatar.MoveLane (other.transform));
		}
	}


}
