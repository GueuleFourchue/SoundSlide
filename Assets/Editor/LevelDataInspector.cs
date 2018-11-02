
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ChunkLevel))]
[CanEditMultipleObjects]
public class LevelDataInspector : Editor
{
    ChunkLevel myCurrentlyInspectedThing;
    private GameObject parentLane;
    private lineTree uselane;
    private List<lineTree> lineTreeselect;

    void OnEnable()
    {
        myCurrentlyInspectedThing = target as ChunkLevel;
    }

    public override void OnInspectorGUI()
    {
        Undo.RecordObject(myCurrentlyInspectedThing, "tweaking");

        EditorGUILayout.BeginHorizontal();

		if (parentLane != null) 
		{
			EditorGUILayout.TextField ("Parent Name: ", parentLane.name);

		}else{
			EditorGUILayout.TextField ("Parent Name: ", " No parent");
		}
        if (GUILayout.Button("Choise parent lane", EditorStyles.miniButton))
        {
            SelectParent();
        }
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Change parent et ajoute LineTree si il n'existe pas", EditorStyles.miniButton))
        {
            AddParent();
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Change name", EditorStyles.miniButton))
        {
            NameLane();
        }

        EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("Change order", EditorStyles.miniButton))
		{
			ChangeOrder ();
		}

		EditorGUILayout.EndHorizontal();



        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Chemin Chemin", EditorStyles.miniButton))
        {
            CreateTree();
        }

        EditorGUILayout.EndHorizontal();

        EditorUtility.SetDirty(myCurrentlyInspectedThing);
    }

	public void ChangeOrder(){

		bool Tryokc = false;
		GameObject[] objs = new GameObject[Selection.gameObjects[0].transform.childCount];


		for (int i=0; i<Selection.gameObjects[0].transform.childCount; i++) {
			objs[i]=Selection.gameObjects[0].transform.GetChild(i).gameObject;
		}


		while (!Tryokc)
		{
			Tryokc = true;
			for (int i = 0; i < objs.Length - 1; i++)
			{
				if (objs[i].gameObject.transform.position.z > objs[i + 1].gameObject.transform.position.z)
				{
					GameObject c2 = objs[i + 1];
					objs[i + 1] = objs[i];
					objs[i] = c2;
					Tryokc = false;
				}
			}
		}

		for (int i = 0; i < objs.Length - 1; i++)
		{
			objs[i].transform.SetSiblingIndex(i);
		}
	}

    public void CreateTree()
    {
        bool Tryokc = false;

		GameObject[] objs = new GameObject[Selection.gameObjects[0].transform.childCount];


		for (int i=0; i<Selection.gameObjects[0].transform.childCount; i++) {
			objs[i]=Selection.gameObjects[0].transform.GetChild(i).gameObject;
		}


        while (!Tryokc)
        {
            Tryokc = true;
            for (int i = 0; i < objs.Length - 1; i++)
            {
                if (objs[i].gameObject.transform.position.z > objs[i + 1].gameObject.transform.position.z)
                {
                    GameObject c2 = objs[i + 1];
                    objs[i + 1] = objs[i];
                    objs[i] = c2;
                    Tryokc = false;
                }
            }
        }

        for (int i = 0; i < objs.Length - 1; i++)
        {
            objs[i].GetComponent<lineTree>().ChunckRight = null;
            objs[i].GetComponent<lineTree>().ChunckLeft = null;
        }

        for (int i = 0; i < objs.Length - 1; i++)
        {
            for (int j = i + 1; j < i + 10; j++)
            {
                if (j < objs.Length)
                {
                    float valueMax = (objs[i].transform.position.z + (objs[i].transform.localScale.z / 2));
                    float valueMin = (objs[j].transform.position.z - (objs[j].transform.localScale.z / 2));
                    if (valueMax > valueMin)
                    {
                        GameObject exemple;

                        float solution1 = objs[i].transform.position.x;
                        float solution2 = objs[j].transform.position.x;

                        float result = Mathf.Abs(solution2 - solution1);

						float solution3 = objs[i].transform.position.y;
						float solution4 = objs[j].transform.position.y;

						float result2 = Mathf.Abs(solution4 - solution3);

                        exemple = Instantiate(objs[j], objs[j].transform.position, Quaternion.identity);
                        exemple.transform.parent = objs[i].transform;

                        float valueposx = exemple.transform.localPosition.x;


						if (result < 1.5f && result2 < 1.5f)
                        {
                            if (valueposx < 0)
                            {
                                objs[i].GetComponent<lineTree>().ChunckLeft = objs[j].GetComponent<lineTree>();
                            }
                            else
                            {
                                objs[i].GetComponent<lineTree>().ChunckRight = objs[j].GetComponent<lineTree>();
                            }
                        }
                        DestroyImmediate(exemple);
                    }
                }
            }
        }
    }

    public void SelectParent()
    {
        parentLane = Selection.gameObjects[0];
    }

    public void NameLane()
    {
		GameObject[] objs = new GameObject[Selection.gameObjects[0].transform.childCount];


		for (int i=0; i<Selection.gameObjects[0].transform.childCount; i++) {
			objs[i]=Selection.gameObjects[0].transform.GetChild(i).gameObject;
		}


        foreach (GameObject obj in objs)
        {
            obj.name = "Lane z:" + obj.transform.position.z;
        }

    }

    public void AddParent()
    {

        GameObject[] objs = Selection.gameObjects;
        foreach (GameObject obj in objs)
        {
            if (obj.transform.parent != parentLane)
            {
                GameObject childlane = obj.transform.GetChild(0).transform.gameObject;

                lineTree ds = childlane.GetComponent(typeof(lineTree)) as lineTree;
               
                if (!ds)
                {
                    childlane.AddComponent(typeof(lineTree));
                }
                obj.transform.GetChild(0).transform.parent = parentLane.transform;
            }
        }

    }
}
