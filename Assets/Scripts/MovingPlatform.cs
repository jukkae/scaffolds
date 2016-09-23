using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	Rigidbody rb;
	float displacement = 0f;
	Vector3 originalPosition;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		originalPosition = rb.position;
	}
	
	// Update is called once per frame
	void Update () {
		rb.MovePosition (new Vector3(rb.position.x + 0.1f, rb.position.y, rb.position.z));
		displacement += .1f;
		if (displacement > 10) {
			rb.MovePosition (originalPosition);
			displacement = 0f;
		}
	}
}
