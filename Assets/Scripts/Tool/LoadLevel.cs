using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour {

	public TextAsset csvFile; 
	public GameObject lane;

	private char endLine = '\n'; 
	private char endChar = ','; 

	private float posX;
	private float posY;
	private float posZ;
	private float angleZ;

	void Start () {
		readCsv ();
	}
	
	private void readCsv()
	{
		string[] doc = csvFile.text.Split(endLine);
		foreach (string line in doc)
		{
			string[] Caracts = line.Split(endChar);
			float.TryParse (Caracts [0], out posX);
			float.TryParse (Caracts [1], out posY);
			float.TryParse (Caracts [2], out posZ);
			float.TryParse (Caracts [3], out angleZ);
			Debug.Log (angleZ);
			CreationLine (posX, posY,posZ,angleZ);
		}
	}

	public void CreationLine(float posX,float posY,float posZ,float rotZ){

		Vector3 newPos = new Vector3 (posX, posY, posZ);
		GameObject laneCreate = Instantiate(lane,newPos, Quaternion.identity) as GameObject;
		laneCreate.transform.Rotate(0,0,rotZ);
	}
}
