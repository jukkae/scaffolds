using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class BallController : MonoBehaviour {

	Rigidbody rb;

	public float speed;
	public float jumpspeed;
	private float jmp = 0;

	public Text winText;

	private float groundDistance = .5f;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		winText.text = "";
		rb.maxAngularVelocity = 50;
	}

	bool IsGrounded ()
	{
		return Physics.Raycast (transform.position, - Vector3.up, groundDistance + 0.1f);
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
		if (IsGrounded ()) {
			if (Input.GetKey (KeyCode.Space)) {
				//Vector3 jump = new Vector3 (.0f, jumpspeed, .0f);
				//rb.AddForce (jump, ForceMode.Impulse);
				if (jmp < jumpspeed) {
					jmp += .5f;
				}
			}
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
	}
}
