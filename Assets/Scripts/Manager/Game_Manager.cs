using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Game_Manager : MonoBehaviour {

	public CanvasManager cmanager;
	public PlayerManager pmanager;
	public TriggersManager tmanager;

	// Use this for initialization
	void Start () {
		cmanager.StartToGoOn ();
        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update () {
		if ((Input.GetKey(KeyCode.Space) && cmanager.playLevelTextactive))
		{
			PlayMoment ();
		}
	}

	public void PlayMoment(){
		cmanager.StartToGoOff ();
		pmanager.PlayerStart ();
	}

	public void DeadMoment(){
		tmanager.BuildLevel ();
		cmanager.StartToGoOn ();
	}

	public void EffectPlay(){
		cmanager.ScalePlay ();
	}
}
