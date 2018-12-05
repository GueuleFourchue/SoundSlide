// #if UnityEditor
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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
        var allMeshes = GetAllMeshRendererInScene();

        Debug.Log("--- BatchedMaterialSetter: " + allMeshes.Count + " meshes");
        int count = 0;

        foreach (MeshRenderer m in allMeshes)
        {
            Debug.Log("---" + count + "/" + allMeshes.Count);
            count++;

            if (m.sharedMaterial.shader != StandardShader)
                return;

            if (m.sharedMaterial.GetTexture("_MainTex") != null)
                return;

            BatchedMaterial batchedMaterial = m.gameObject.AddComponent(typeof(BatchedMaterial)) as BatchedMaterial;

            batchedMaterial.Albedo = m.sharedMaterial.GetColor("_Color");
            batchedMaterial.Emission = m.sharedMaterial.GetColor("_EmissionColor");
            batchedMaterial.Metallic = m.sharedMaterial.GetFloat("_Metallic");
            batchedMaterial.Smoothness = m.sharedMaterial.GetFloat("_Glossiness");

            m.sharedMaterial = BatchedMaterial;
        }

        Debug.Log("--- BatchedMaterialSetter FINISHED");
    }

    List<MeshRenderer> GetAllMeshRendererInScene()
    {
        List<MeshRenderer> objectsInScene = new List<MeshRenderer>();

        foreach (MeshRenderer go in Resources.FindObjectsOfTypeAll(typeof(MeshRenderer)) as MeshRenderer[])
        {
            if (go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave)
                continue;

            if (!EditorUtility.IsPersistent(go.gameObject.transform.root.gameObject))
                continue;

            objectsInScene.Add(go);
        }

        return objectsInScene;
    }
}
// #endif