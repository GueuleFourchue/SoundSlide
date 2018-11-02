using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LD_giz : MonoBehaviour
{
    public Color col;

    private void OnDrawGizmos()
    {
        Gizmos.color = col;
        Gizmos.DrawLine
            (
                transform.position + transform.right,
                transform.position + transform.right * - 1
            );
    }
}
