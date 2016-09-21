using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

	Rigidbody rb;

	public float speed;

	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		Vector3 movement = new Vector3 (moveHorizontal, .0f, .0f);

		rb.AddForce (movement * speed);
	}
}
