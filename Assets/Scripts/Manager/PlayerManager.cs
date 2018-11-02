using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	public ManagerMoveLanes mmLane;
	public ManagerMovePlayer mmPlayer;

	public void PlayerStart(){
		mmPlayer.PlayGame();
        //mmPlayer.PlaySound();
        mmPlayer.StartCoroutine("PlaySoundRoutine");
    }
}
