using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPlayerSpeed : MonoBehaviour {

    public GameObject avatar;

    public void StopPlayer()
    {
        avatar.SetActive(false);
    }
    
}
