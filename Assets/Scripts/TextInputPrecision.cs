using UnityEngine;
using UnityEngine.Sprites;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class TextInputPrecision : MonoBehaviour {

	public Sprite s5;
	public Sprite s10;
	public Sprite s15;
	public Sprite s30;

	public SpriteRenderer spriteRend;

	float originYPosition;

	void Start()
	{
		originYPosition = transform.localPosition.y;
	}

	public void TextPop(Sprite spr)
	{
		transform.DOKill ();
		spriteRend.DOKill ();

		transform.localPosition = new Vector3 (transform.localPosition.x, originYPosition, transform.localPosition.z);

		spriteRend.sprite = spr;

		Color color = spriteRend.color;
		color.a = 1f;
		spriteRend.color = color;

		transform.DOLocalMoveY (transform.localPosition.y + 0.25f, 0.4f);

		StartCoroutine (FadeAlpha ());
	}

	IEnumerator FadeAlpha()
	{
		yield return new WaitForSeconds (0.17f);
		spriteRend.DOFade (0, 0.4f);
	}
}
