using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundActivateRotate : MonoBehaviour
{
    public BackgroundRotate backgroundRotate;
	public BackgroundRotate backgroundRotate2;

   
    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
			backgroundRotate.enabled = true;
			backgroundRotate2.enabled = true;
        }
    }

}
