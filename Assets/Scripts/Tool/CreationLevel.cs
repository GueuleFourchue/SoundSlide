using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CreationLevel : MonoBehaviour {

	public int bpm = 120;
	public int nbUnite = 10;
	public GameObject lane;

	private float x = 0;
	private float y = 0;

	public TextAsset csvFile; 

	private char endLine = '\n'; 
	private char endChar = ';'; 

	private int nbline;
	private int dureeline;
	private int degres = 0;
	private float lastScale = 0;
	private float distance = 0;

	void Start () {
		readCsv();
	}

	void Update () {

	}
		
	private void readCsv()
	{
		string[] doc = csvFile.text.Split(endLine);
		foreach (string line in doc)
		{
			x=0;
			y=0;
			degres = 0;

			string[] Caracts = line.Split(endChar);
			dureeline = int.Parse(Caracts[0]);
			distance = nbUnite * bpm/60 * dureeline;
			lastScale = lastScale + distance;
			nbline = int.Parse(Caracts[1]);
			for(int i = 2;i<nbline+2;i++){
				degres = int.Parse(Caracts[i]) + degres;
				CreationLine();
			}
		}
	}
		
	public void CreationLine(){
		
		GameObject laneCreate = Instantiate(lane,lane.transform.position, Quaternion.identity) as GameObject;
		ScaleLane(laneCreate);
		PlacementLane(laneCreate);
		RotateLine(laneCreate);
	}

	public void ScaleLane(GameObject lanescale){
		
		lanescale.transform.localScale = new Vector3(lanescale.transform.localScale.x,lanescale.transform.localScale.y,distance);
	}

	public void PlacementLane(GameObject lanepos){
		
			float x1 =  (x) + (0.5f * Mathf.Cos(degres*Mathf.PI/180));
			float y1 = (y) + (0.5f*Mathf.Sin(degres*Mathf.PI/180));

		lanepos.transform.position = new Vector3(x1+ x,y1 + y,lastScale);

			x = x1;
			y = y1;
	}

	public void RotateLine(GameObject laneRot){
		laneRot.transform.Rotate(0,0,degres);
	}
}
