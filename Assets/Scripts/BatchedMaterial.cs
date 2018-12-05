using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BatchedMaterial : MonoBehaviour
{
    [Header("Settings")]
    public Color Albedo;
    [ColorUsage(true, true, 0, 8, 0.125f, 3)]
    public Color Emission;
    [Range(0, 1)]
    public float Metallic;
    [Range(0, 1)]
    public float Smoothness = 0.5f;

    private MeshRenderer _meshRenderer;
    private MaterialPropertyBlock _propertyBlock;

    // Use this for initialization
    void Start()
    {
        if (GetComponent<MeshRenderer>() == null)
        {
            Debug.LogError("No Mesh Renderer!", this);
            return;
        }

        _meshRenderer = GetComponent<MeshRenderer>();

        SetProperties();
    }

    void SetProperties()
    {
        if (GetComponent<MeshRenderer>() == null)
        {
            Debug.LogError("No Mesh Renderer!", this);
            return;
        }

        _meshRenderer = GetComponent<MeshRenderer>();

        if (_propertyBlock == null)
            _propertyBlock = new MaterialPropertyBlock();

        _propertyBlock.SetColor("_Albedo", Albedo);
        _propertyBlock.SetColor("_Emission", Emission);

        _propertyBlock.SetFloat("_Metallic", Metallic);
        _propertyBlock.SetFloat("_Smoothness", Smoothness);

        _meshRenderer.SetPropertyBlock(_propertyBlock);
    }

    void OnValidate()
    {
        SetProperties();
    }
}
