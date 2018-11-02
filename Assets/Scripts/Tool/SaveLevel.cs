using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class SaveLevel : MonoBehaviour {

	public GameObject LevelParent;
	public string NameLevel = "BOOUMBOUMBOUM";
	private List<string[]> rowData = new List<string[]>();


	void Start () {
		Save();
	}

	void Save(){

		string[] rowDataTemp = new string[4];
		//rowDataTemp[0] = "POS X";
		//rowDataTemp[1] = "POS Y";
		//rowDataTemp[2] = "POS Z";
		//rowDataTemp[3] = "ROT Z";
		//rowData.Add(rowDataTemp);

		// You can add up the values in as many cells as you want.
		for(int i = 0; i < LevelParent.transform.childCount; i++){
			rowDataTemp = new string[4];
			rowDataTemp [0] = LevelParent.transform.GetChild (i).transform.position.x.ToString();
			rowDataTemp [1] = LevelParent.transform.GetChild (i).transform.position.y.ToString();
			rowDataTemp [2] = LevelParent.transform.GetChild (i).transform.position.z.ToString();
			rowDataTemp [3] = LevelParent.transform.GetChild (i).transform.eulerAngles.z.ToString();
			rowData.Add(rowDataTemp);
		}

		string[][] output = new string[rowData.Count][];

		for(int i = 0; i < output.Length; i++){
			output[i] = rowData[i];
		}

		int     length         = output.GetLength(0);
		string     delimiter     = ",";

		StringBuilder sb = new StringBuilder();

		for (int index = 0; index < length; index++)
			sb.AppendLine(string.Join(delimiter, output[index]));


		string filePath = getPath();

		StreamWriter outStream = System.IO.File.CreateText(filePath);
		outStream.WriteLine(sb);
		outStream.Close();
	}

	// Following method is used to retrive the relative path as device platform
	private string getPath(){
		return Application.dataPath +"/LevelData/"+NameLevel+".csv";
		}
		
}