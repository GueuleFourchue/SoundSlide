using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public ManagerMoveLanes mmLane;
    public ManagerMovePlayer mmPlayer;

    public void PlayerStart()
    {
        if (!mmPlayer.gameObject.activeSelf)
            return;

        mmPlayer.PlayGame();
        //mmPlayer.PlaySound();
        mmPlayer.StartCoroutine("PlaySoundRoutine");
    }
}
