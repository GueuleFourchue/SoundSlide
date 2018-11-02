using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CameraFollow : MonoBehaviour {
	 
	public Transform cameraSpot;
	public Transform avatar; 

	public float moveLerp = 0.1f;
	public float rotateLerp = 0.1f;

	public float xRotation = 15f;

	void Start () 
	{
		
	}
		
	void LateUpdate ()
	{
		// Z Position
		transform.position = new Vector3 (transform.position.x, transform.position.y, cameraSpot.transform.position.z);
			
		//Position
		transform.position = Vector3.Lerp (transform.position, cameraSpot.position, moveLerp);

		//Rotation
		transform.rotation = Quaternion.Lerp (transform.rotation, cameraSpot.rotation, rotateLerp);
		//transform.DOLocalRotate (cameraSpot.eulerAngles, rotateLerp).SetEase(Ease.Linear);
	}
}