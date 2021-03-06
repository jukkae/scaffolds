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
	public Text scoreText;
	public Text timeText;

	private int numberOfCoins = 0;
	private int numberOfCoinsTotal;
	private float time = 0;

	private bool gameActive = true;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		winText.text = "";
		rb.maxAngularVelocity = 1000;
		numberOfCoinsTotal = GameObject.FindGameObjectsWithTag ("Pickup").Length;
		scoreText.text = "Score: " + numberOfCoins + " / " + numberOfCoinsTotal;
		timeText.text = "Time: " + time.ToString();
	}

	bool IsGrounded ()
	{
			// TODO dirty; use some sort of scale variable instead of localScale.x
			float groundDistance = GetComponent<SphereCollider> ().radius * transform.localScale.x;

			RaycastHit rayHit;
			Vector3 startPoint = transform.position;
			return Physics.SphereCast (startPoint, groundDistance - 0.01f, Vector3.down, out rayHit, 0.02f);  
	}
	
	void FixedUpdate () {
		if (gameActive) {
			if (IsGrounded()) {
				float moveHorizontal = Input.GetAxis ("Horizontal");
				Vector3 movement = new Vector3 (moveHorizontal, .0f, .0f);

				rb.AddForce (movement * speed);
				//rb.AddTorque (.0f, .0f, moveHorizontal * speed * -1);
			}
		}
	}

	void Update () {
		if (gameActive) {
			if (Input.GetKey (KeyCode.Space)) { // charge jumps midair as well
				if (jmp < jumpspeed) {
					jmp += .5f;
				}
			}
			if (IsGrounded ()) { // but only allow jumps from ground
				if (Input.GetKeyUp (KeyCode.Space)) {
					rb.AddForce (new Vector3 (.0f, jmp, .0f), ForceMode.Impulse);
					jmp = 0;
				}
			}
			time = (int)Time.time;
			timeText.text = "Time: " + time.ToString ();
		}
	}

	void OnTriggerEnter(Collider other) {
		if (gameActive) {
			if (other.gameObject.CompareTag ("Goal")) {
				winText.text = "THE WINNER IS YOU";
				gameActive = false;
			}
			if (other.gameObject.CompareTag ("Boundary")) {
				winText.text = "YOU LOSE";
				gameActive = false;
			}
			if (other.gameObject.CompareTag ("Pickup")) {
				other.gameObject.SetActive (false);
				numberOfCoins++;
				scoreText.text = "Score: " + numberOfCoins + " / " + numberOfCoinsTotal;
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
			if (other.gameObject.CompareTag ("Destructible")) {
				other.gameObject.SetActive (false);
			}
		}
	}
}
