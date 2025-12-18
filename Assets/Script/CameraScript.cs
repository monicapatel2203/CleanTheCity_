using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraScript : MonoBehaviour {

	public Transform EnemyCar_Transform;

	private Vector3 _cameraoffset;

	[Range(0.0f,1.0f)]
	public float Smoothfactor = 0.5f;

	public bool LookAtPlayer = false;

	// Use this for initialization
	void Start () {
		_cameraoffset = transform.position - EnemyCar_Transform.position;
	}
	
	//Function to follow position and rotation of player
	void LateUpdate () {
		Vector3 _newPos = EnemyCar_Transform.position + _cameraoffset;

		transform.position = Vector3.Slerp (transform.position, _newPos, Smoothfactor * Time.deltaTime);

		if (LookAtPlayer) {																//Follow the player transform
			transform.LookAt (EnemyCar_Transform);
		}
	}
}
