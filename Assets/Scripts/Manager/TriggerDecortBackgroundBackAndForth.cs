using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDecortBackgroundBackAndForth : MonoBehaviour {

	public float FirstposZ;
	public DecBackgroundBackAndForth gameObjectActivate;
	private Transform player;
	private int number = 0;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		ActivateScript ();
	}

	void ActivateScript()
	{
		float playerZposition = player.position.z;

		for (int i = number; i < gameObjectActivate.pos.Length; i ++) 
		{
			if (gameObjectActivate.pos[i] < playerZposition) 
			{
				gameObjectActivate.BBAF [i].enabled = true;
				number = number + 1;

				if(i == gameObjectActivate.pos.Length-1)
				{
					this.enabled = false;
				}
			}

		}
	}

	public void DesactivateScript()
	{
		float playerZposition = player.position.z;

		for (int i = 0; i < gameObjectActivate.pos.Length; i ++) 
		{
			if (gameObjectActivate.pos[i] > playerZposition) 
			{
				gameObjectActivate.BBAF [i].enabled = false;
			}

		}
		number = 0;
	}
}

[System.Serializable]
public struct DecBackgroundBackAndForth
{
	public float[] pos;
	public  BackgroundBackAndForth[] BBAF;
}
