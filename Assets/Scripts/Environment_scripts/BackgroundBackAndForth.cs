using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BackgroundBackAndForth : MonoBehaviour
{

    public float endPosition;
    float moveTime;

    public bool canMove = true;
    public bool moveForward = true;

    float bpm = 150;

    // Use this for initialization
    void Start()
    {
        moveTime = (60 / bpm) / 2;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(BackAndForth());
    }

    IEnumerator BackAndForth()
    {
        if (canMove)
        {
            canMove = false;

            if (moveForward)
            {
                transform.DOLocalMoveZ(endPosition, moveTime);
                moveForward = false;
                yield return new WaitForSeconds(moveTime);
                canMove = true;
            }
            else
            {
                transform.DOLocalMoveZ(-endPosition, moveTime);
                moveForward = true;
                yield return new WaitForSeconds(moveTime);
                canMove = true;
            }
        }
    }
}
