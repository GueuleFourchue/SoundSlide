using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineTree : MonoBehaviour
{
    public lineTree ChunckLeft;
    public lineTree ChunckRight;
   // public ChunkLevel currentlane;

  //  public float sizeLane;
   // public float angleLane;
   // public float distLane;
   // public float coef;

	private float mindist;
	private float maxdist;

	public float returnMaxR(){

		float value = (transform.position.z + (transform.localScale.z/2));

		return value;
	}

	public float returnMinR(){
		float value = (ChunckRight.gameObject.transform.position.z - (ChunckRight.gameObject.transform.localScale.z / 2));

		return value;
	}

	public float returnMaxL(){

		float value = (transform.position.z + (transform.localScale.z/2));

		return value;
	}

	public float returnMinL(){
		float value = (ChunckLeft.gameObject.transform.position.z - (ChunckLeft.gameObject.transform.localScale.z / 2));

		return value;
	}
    // Use this for initialization
}
