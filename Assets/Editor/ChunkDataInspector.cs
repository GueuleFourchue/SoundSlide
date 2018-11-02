using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ChunkData))]
[CanEditMultipleObjects]
public class ChunkDataInspector : Editor
{
    ChunkData myCurrentlyInspectedThing;
    private GameObject laneCreate;
    private GameObject laneCreate2;
    public GameObject laneParent;

    void OnEnable()
    {
        myCurrentlyInspectedThing = target as ChunkData;
    }

    public override void OnInspectorGUI()
    {
        // base.OnInspectorGUI();
        Undo.RecordObject(myCurrentlyInspectedThing, "tweaking");


        myCurrentlyInspectedThing.rightLane = EditorGUILayout.Toggle("Droite de la Lane :", myCurrentlyInspectedThing.rightLane);
        myCurrentlyInspectedThing.leftLane = EditorGUILayout.Toggle("Gauche de la Lane :", myCurrentlyInspectedThing.leftLane);
        myCurrentlyInspectedThing.sizeLane = EditorGUILayout.FloatField("Taille de la Lane", myCurrentlyInspectedThing.sizeLane);
        myCurrentlyInspectedThing.angleLane = EditorGUILayout.FloatField("Angle de la Lane", myCurrentlyInspectedThing.angleLane);
        myCurrentlyInspectedThing.distLane = EditorGUILayout.FloatField("Dist de la Lane", myCurrentlyInspectedThing.distLane);
        myCurrentlyInspectedThing.coef = EditorGUILayout.FloatField("Coef :", myCurrentlyInspectedThing.coef);

        EditorGUILayout.BeginHorizontal();

		if (laneParent != null) 
		{
			 EditorGUILayout.TextField ("Parent Name: ", laneParent.name);

		}else{
			 EditorGUILayout.TextField ("Parent Name: ", " No parent");
		}
		
        if (GUILayout.Button("Parent Lane", EditorStyles.miniButton))
        {
            SelectParent();
        }
        EditorGUILayout.EndHorizontal();


		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("Generate Lane", EditorStyles.miniButton))
		{
			laneCreate = null;
			CreationLine();
		}
		EditorGUILayout.EndHorizontal();

		this.Repaint();
        EditorUtility.SetDirty(myCurrentlyInspectedThing);
    }

    public void SelectParent()
    {
        laneParent = Selection.gameObjects[0];
    }

    public void AddParent(GameObject lanechild)
    {
        if (laneParent != null)
        {
            lanechild.transform.parent = laneParent.transform;
        }
        else
        {
            Debug.Log("no parent");
        }
    }


    public void CreationLine()
    {
        if (myCurrentlyInspectedThing.rightLane)
        {
            laneCreate = Instantiate(Selection.gameObjects[0], Selection.gameObjects[0].transform.position, Quaternion.identity) as GameObject;
			Debug.Log (laneCreate);
            ScaleLane(laneCreate);
            PlacementLaneRight(laneCreate);
            RotateLineRight(laneCreate);
            Selction(laneCreate);
            NameLane(laneCreate);
            AddParent(laneCreate);
        }
        if (myCurrentlyInspectedThing.leftLane)
        {
            laneCreate2 = Instantiate(Selection.gameObjects[0], Selection.gameObjects[0].transform.position, Quaternion.identity) as GameObject;
            ScaleLane(laneCreate2);
            PlacementLaneLeft(laneCreate2);
            RotateLineLeft(laneCreate2);
            Selction(laneCreate2);
            NameLane(laneCreate2);
            AddParent(laneCreate2);
        }

    }

    public void NameLane(GameObject lanename)
    {
        lanename.name = "Lane z:" + lanename.transform.position.z;
    }
		

    public void ScaleLane(GameObject lanescale)
    {
        lanescale.transform.localScale = new Vector3(lanescale.transform.localScale.x, lanescale.transform.localScale.y, myCurrentlyInspectedThing.sizeLane);
    }

    public void PlacementLaneRight(GameObject lanepos)
    {
        float x = (0.5f * Mathf.Cos(Mathf.Abs(Selection.gameObjects[0].transform.eulerAngles.z) * Mathf.PI / 180));
        float y = (0.5f * Mathf.Sin(Mathf.Abs(Selection.gameObjects[0].transform.eulerAngles.z) * Mathf.PI / 180));
        float x1 = (0.5f * Mathf.Cos(Mathf.Abs((Selection.gameObjects[0].transform.eulerAngles.z + myCurrentlyInspectedThing.angleLane)) * Mathf.PI / 180));
        float y1 = (0.5f * Mathf.Sin(Mathf.Abs((Selection.gameObjects[0].transform.eulerAngles.z + myCurrentlyInspectedThing.angleLane)) * Mathf.PI / 180));
        float x2 = (myCurrentlyInspectedThing.coef * Mathf.Cos(Mathf.Abs((Selection.gameObjects[0].transform.eulerAngles.z + myCurrentlyInspectedThing.angleLane)) * Mathf.PI / 180));
        float y2 = (myCurrentlyInspectedThing.coef * Mathf.Sin(Mathf.Abs((Selection.gameObjects[0].transform.eulerAngles.z + myCurrentlyInspectedThing.angleLane)) * Mathf.PI / 180));


        if (Selection.gameObjects[0].transform.eulerAngles.z + myCurrentlyInspectedThing.angleLane < 0)
        {
            float posZfinal = Selection.gameObjects[0].transform.position.z + (Selection.gameObjects[0].transform.localScale.z / 2) + (myCurrentlyInspectedThing.sizeLane / 2) - myCurrentlyInspectedThing.distLane;
            lanepos.transform.position = new Vector3((Selection.gameObjects[0].transform.position.x + x1 + x + x2), (Selection.gameObjects[0].transform.position.y - y1 + y - y2), posZfinal);

        }
        else
        {
            float posZfinal = Selection.gameObjects[0].transform.position.z + (Selection.gameObjects[0].transform.localScale.z / 2) + (myCurrentlyInspectedThing.sizeLane / 2) - myCurrentlyInspectedThing.distLane;
            lanepos.transform.position = new Vector3((Selection.gameObjects[0].transform.position.x + x1 + x + x2), (Selection.gameObjects[0].transform.position.y + y1 + y + y2), posZfinal);
        }
    }

    public void PlacementLaneLeft(GameObject lanepos)
    {
        float angle = Selection.gameObjects[0].transform.localEulerAngles.z;
        angle = (angle > 180) ? angle - 360 : angle;

        if (Mathf.Round(Selection.gameObjects[0].transform.localEulerAngles.z) + Mathf.Round(myCurrentlyInspectedThing.angleLane) < 0)
        {

            float x = (0.5f * Mathf.Cos(Mathf.Abs(Selection.gameObjects[0].transform.eulerAngles.z) * Mathf.PI / 180));
            float y = (0.5f * Mathf.Sin(Mathf.Abs(Selection.gameObjects[0].transform.eulerAngles.z) * Mathf.PI / 180));
            float x1 = (0.5f * Mathf.Cos(Mathf.Abs((Selection.gameObjects[0].transform.eulerAngles.z + myCurrentlyInspectedThing.angleLane)) * Mathf.PI / 180));
            float y1 = (0.5f * Mathf.Sin(Mathf.Abs((Selection.gameObjects[0].transform.eulerAngles.z + myCurrentlyInspectedThing.angleLane)) * Mathf.PI / 180));
            float x2 = (myCurrentlyInspectedThing.coef * Mathf.Cos(Mathf.Abs((Selection.gameObjects[0].transform.eulerAngles.z + myCurrentlyInspectedThing.angleLane)) * Mathf.PI / 180));
            float y2 = (myCurrentlyInspectedThing.coef * Mathf.Sin(Mathf.Abs((Selection.gameObjects[0].transform.eulerAngles.z + myCurrentlyInspectedThing.angleLane)) * Mathf.PI / 180));

            float posZfinal = Selection.gameObjects[0].transform.position.z + (Selection.gameObjects[0].transform.localScale.z / 2) + (myCurrentlyInspectedThing.sizeLane / 2) - myCurrentlyInspectedThing.distLane;
            lanepos.transform.position = new Vector3((Selection.gameObjects[0].transform.position.x - x - x1 - x2), (Selection.gameObjects[0].transform.position.y - y1 + y - y2), posZfinal);
        }
        else
        {
            Selection.gameObjects[0].transform.Rotate(0, 0, (-2) * (Selection.gameObjects[0].transform.eulerAngles.z));

            float x = (0.5f * Mathf.Cos(Mathf.Abs(Selection.gameObjects[0].transform.eulerAngles.z) * Mathf.PI / 180));
            float y = (0.5f * Mathf.Sin(Mathf.Abs(Selection.gameObjects[0].transform.eulerAngles.z) * Mathf.PI / 180));
            float x1 = (0.5f * Mathf.Cos(Mathf.Abs((Selection.gameObjects[0].transform.eulerAngles.z + myCurrentlyInspectedThing.angleLane)) * Mathf.PI / 180));
            float y1 = (0.5f * Mathf.Sin(Mathf.Abs((Selection.gameObjects[0].transform.eulerAngles.z + myCurrentlyInspectedThing.angleLane)) * Mathf.PI / 180));
            float x2 = (myCurrentlyInspectedThing.coef * Mathf.Cos(Mathf.Abs((Selection.gameObjects[0].transform.eulerAngles.z + myCurrentlyInspectedThing.angleLane)) * Mathf.PI / 180));
            float y2 = (myCurrentlyInspectedThing.coef * Mathf.Sin(Mathf.Abs((Selection.gameObjects[0].transform.eulerAngles.z + myCurrentlyInspectedThing.angleLane)) * Mathf.PI / 180));

            float posZfinal = Selection.gameObjects[0].transform.position.z + (Selection.gameObjects[0].transform.localScale.z / 2) + (myCurrentlyInspectedThing.sizeLane / 2) - myCurrentlyInspectedThing.distLane;
            lanepos.transform.position = new Vector3(Selection.gameObjects[0].transform.position.x - x1 - x - x2, Selection.gameObjects[0].transform.position.y + y1 + y + y2, posZfinal);
            Selection.gameObjects[0].transform.Rotate(0, 0, (-2) * (Selection.gameObjects[0].transform.eulerAngles.z));
        }

    }

    public void RotateLineRight(GameObject laneRot)
    {
        laneRot.transform.Rotate(0, 0, Selection.gameObjects[0].transform.eulerAngles.z + myCurrentlyInspectedThing.angleLane);
    }

    public void RotateLineLeft(GameObject laneRot)
    {
        laneRot.transform.Rotate(0, 0, Selection.gameObjects[0].transform.eulerAngles.z - myCurrentlyInspectedThing.angleLane);
    }

    public void Selction(GameObject laneselec)
    {
        Selection.activeObject = laneselec;
    }
}
