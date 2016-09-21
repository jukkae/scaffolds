using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject ball;

	private Vector3 offset;

	void Start () {
		offset = transform.position - ball.transform.position;
	}
	
	void Update () {
		transform.position = ball.transform.position + offset;
	}
}
