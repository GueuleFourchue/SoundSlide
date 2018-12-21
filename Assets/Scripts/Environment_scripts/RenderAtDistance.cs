using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class RenderAtDistance : MonoBehaviour
{
    [Header("Settings")]
    public bool overrideDistance = false;

    public float visibilityDistance = 20f;

    private float _refreshDuration = 0.5f;

    private List<MeshRenderer> _meshes = new List<MeshRenderer>();

    void Awake()
    {
        _meshes = GetComponentsInChildren<MeshRenderer>().ToList();
    }

    IEnumerator CheckVisibility()
    {

        yield return new WaitForSecondsRealtime(_refreshDuration);

        StartCoroutine(CheckVisibility());
    }

    void Show()
    {
        for (int i = 0; i < _meshes.Count; i++)
            if (!_meshes[i].enabled)
                _meshes[i].enabled = true;
    }

    void Hide()
    {
        for (int i = 0; i < _meshes.Count; i++)
            if (_meshes[i].enabled)
                _meshes[i].enabled = false;
    }
}
