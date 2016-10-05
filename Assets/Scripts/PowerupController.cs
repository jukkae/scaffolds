using UnityEngine;
using System.Collections;

public class PowerupController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.right * Time.deltaTime * 45);
		transform.Rotate (Vector3.up * Time.deltaTime * 30);
		transform.Rotate (Vector3.forward * Time.deltaTime * 15);
	}
}
