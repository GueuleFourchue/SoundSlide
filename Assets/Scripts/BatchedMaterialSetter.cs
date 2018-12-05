// #if UnityEditor
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class BatchedMaterialSetter : MonoBehaviour
{
    [Header("Settings")]
    public bool Launch = false;
    public Shader StandardShader;
    public Material BatchedMaterial;

    void OnValidate()
    {
        if (Launch == true)
        {
            SetMaterials();
            Launch = false;
        }
    }

    void SetMaterials()
    {
        List<MeshRenderer> allMeshes = FindObjectsOfTypeAll<MeshRenderer>();

        Debug.Log("--- BatchedMaterialSetter: " + allMeshes.Count + " meshes");
        int count = 0;
        int changedCount = 0;

        foreach (MeshRenderer m in allMeshes)
        {
            count++;

            if (m.sharedMaterial.shader != StandardShader)
                continue;

            if (m.sharedMaterial.GetTexture("_MainTex") != null)
                continue;

            if (m.gameObject.GetComponent<BatchedMaterial>() != null)
                continue;

            BatchedMaterial batchedMaterial = m.gameObject.AddComponent(typeof(BatchedMaterial)) as BatchedMaterial;

            batchedMaterial.Albedo = m.sharedMaterial.GetColor("_Color");
            batchedMaterial.Emission = m.sharedMaterial.GetColor("_EmissionColor");
            batchedMaterial.Metallic = m.sharedMaterial.GetFloat("_Metallic");
            batchedMaterial.Smoothness = m.sharedMaterial.GetFloat("_Glossiness");

            m.sharedMaterial = BatchedMaterial;
            batchedMaterial.SetProperties();

            Debug.Log("--- " + count + "/" + allMeshes.Count + " --- " + m.gameObject.name, m.gameObject);
            changedCount++;
        }

        Debug.Log("--- BatchedMaterialSetter FINISHED with " + changedCount + " meshes changed!");
    }

    List<GameObject> GetAllObjectsInScene()
    {
        List<GameObject> objectsInScene = new List<GameObject>();

        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if (go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave)
                continue;

            if (!EditorUtility.IsPersistent(go.transform.root.gameObject))
                continue;

            objectsInScene.Add(go);
        }

        return objectsInScene;
    }

    public static List<T> FindObjectsOfTypeAll<T>()
    {
        List<T> results = new List<T>();
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            var s = SceneManager.GetSceneAt(i);
            if (s.isLoaded)
            {
                var allGameObjects = s.GetRootGameObjects();
                for (int j = 0; j < allGameObjects.Length; j++)
                {
                    var go = allGameObjects[j];
                    results.AddRange(go.GetComponentsInChildren<T>(true));
                }
            }
        }
        return results;
    }
}
// #endif