using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TutoPlayerMove : MonoBehaviour {

    public float forwardSpeed;
    public CanvasGroup canvasGroup;
    public TutoManager tutoManager;
    bool move;

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !move)
        {
            Activation();
        }

        if (move)
            MoveForward();
	}

    
    void Activation()
    {
        move = true;
        canvasGroup.DOFade(0, 0.5f);
        tutoManager.PlaySFX(tutoManager.Submit);
    }

    void MoveForward()
    {
        transform.Translate(transform.forward * Time.deltaTime * forwardSpeed);
    }
}
