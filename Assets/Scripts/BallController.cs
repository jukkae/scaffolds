using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BallController : MonoBehaviour {

	Rigidbody rb;

	public float speed;
	public float jumpspeed;

	public Text winText;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		winText.text = "";
	}
	
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		Vector3 movement = new Vector3 (moveHorizontal, .0f, .0f);

		rb.AddForce (movement * speed);

		if (Input.GetKeyDown (KeyCode.Space)) {
			Vector3 jump = new Vector3 (.0f, jumpspeed, .0f);
			rb.AddForce (jump);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Goal")) {
			winText.text = "THE WINNER IS YOU";
		}
		if (other.gameObject.CompareTag ("Boundary")) {
			winText.text = "YOU LOSE";
		}
	}
}
