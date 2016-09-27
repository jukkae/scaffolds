﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class BallController : MonoBehaviour {

	Rigidbody rb;

	public float speed;
	public float jumpspeed;
	private float jmp = 0;

	public Text winText;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		winText.text = "";
		rb.maxAngularVelocity = 1000;
	}

	bool IsGrounded ()
	{
		// TODO dirty; move to some sort of scale variable
		float groundDistance = GetComponent<SphereCollider> ().radius * transform.localScale.x;
		// -> nice puzzle: two rails, need to grow/shrink the ball to a certain size so that the ball rides
		// between the rails and gets stuff from below the rails
		//return Physics.Raycast (transform.position, - Vector3.up, groundDistance + 0.1f);

		RaycastHit rayHit;
		Vector3 startPoint = transform.position;
		return Physics.SphereCast (startPoint, groundDistance-0.01f, Vector3.down, out rayHit, 0.02f);  
	}
	
	void FixedUpdate () {
		if (IsGrounded()) {
			float moveHorizontal = Input.GetAxis ("Horizontal");
			Vector3 movement = new Vector3 (moveHorizontal, .0f, .0f);

			rb.AddForce (movement * speed);
			//rb.AddTorque (.0f, .0f, moveHorizontal * speed * -1);
		}
	}

	void Update () {
		if (Input.GetKey (KeyCode.Space)) { // charge jumps midair as well
			if (jmp < jumpspeed) {
				jmp += .5f;
			}
		}
		if (IsGrounded ()) {
			if (Input.GetKeyUp (KeyCode.Space)) {
				rb.AddForce (new Vector3 (.0f, jmp, .0f), ForceMode.Impulse);
				jmp = 0;
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Goal")) {
			winText.text = "THE WINNER IS YOU";
			SceneManager.LoadScene ("2");
		}
		if (other.gameObject.CompareTag ("Boundary")) {
			winText.text = "YOU LOSE";
		}
		if (other.gameObject.CompareTag ("SmallerPowerup")) {
			transform.localScale = new Vector3 (.5f * transform.localScale.x,
												.5f * transform.localScale.y,
												.5f * transform.localScale.z);
			rb.mass *= .5f;
			other.gameObject.SetActive (false);
		}
		if (other.gameObject.CompareTag ("LargerPowerup")) {
			transform.localScale = new Vector3 (2.0f * transform.localScale.x,
												2.0f * transform.localScale.y,
												2.0f * transform.localScale.z);
			rb.mass *= 2.0f;
			other.gameObject.SetActive (false);
		}
	}
}
