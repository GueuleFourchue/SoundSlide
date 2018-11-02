using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScorePopText : MonoBehaviour {

    private void Awake()
    {
		transform.DOLocalMove(transform.up * 3, 0.5f);
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(1);
        //Destroy(this.gameObject);
    }
}
