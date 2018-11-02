using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class info_managerscript : MonoBehaviour {

	public static info_managerscript instance = null;  

	public bool info_peaceful;
	public bool info_normal;
	public bool info_flawless;
	public bool info_speed75;
	public bool info_speed100;
	public bool info_speed125;
	public bool info_speed150;
	public bool info_noFarLanes;
	public bool info_noNearLanes;

	void Awake() {
		
		if (instance == null)
		{
			DontDestroyOnLoad(gameObject);
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);   
		}
	}

	public void getInfoGame(bool peaceful,bool normal,bool flawless,bool speed75,bool speed100,bool speed125,bool speed150,bool noFarLanes,bool noNearLanes){
		info_peaceful = peaceful;
		info_normal = normal;
		info_flawless = flawless;
		info_speed75 = speed75;
		info_speed100 = speed100;
		info_speed125 = speed125;
		info_speed150 = speed150;
		info_noFarLanes = noFarLanes;
		info_noNearLanes = noNearLanes;
	}
}
