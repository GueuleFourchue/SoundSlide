using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class HaloPulse : MonoBehaviour {

	SpriteRenderer spriteRend;
	Color newColor;
	public float speed = 2;
	bool alphaUp;

	// Use this for initialization
	void Start () 
	{
		spriteRend = GetComponent<SpriteRenderer>();
		newColor = spriteRend.color;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (alphaUp)
			ChangeAlphaUp ();
		if (!alphaUp)
			ChangeAlphaDown ();
	}

	void ChangeAlphaUp()
	{
		newColor.a += Time.deltaTime * speed;
		spriteRend.color = newColor;

		if (newColor.a > 0.3f)
			alphaUp = false;
	}

	void ChangeAlphaDown()
	{
		newColor.a -= Time.deltaTime * speed;
		spriteRend.color = newColor;

		if (newColor.a < 0.05f) 
			alphaUp = true;	
	}
}
