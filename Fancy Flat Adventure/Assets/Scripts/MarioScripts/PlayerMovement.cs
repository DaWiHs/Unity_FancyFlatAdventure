using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float speed;
	public float jumpSpeed;

	Rigidbody2D playerRB;
	Animator playerAnim;
	bool grounded;
	bool facingRight = true;

	// Use this for initialization
	void Start () {
		playerRB = gameObject.GetComponent<Rigidbody2D> ();
		playerAnim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		float mvHor = Input.GetAxis ("Horizontal");

		playerRB.velocity = new Vector2 (mvHor * speed, playerRB.velocity.y);
		if (Input.GetKeyDown (KeyCode.W) && grounded == true) {
			playerRB.velocity = new Vector2(playerRB.velocity.x, 5 * jumpSpeed);
		}

		if (mvHor < 0.0f && facingRight == true) {
			FlipPlayer ();
		} else if (mvHor > 0.0f && facingRight == false) {
			FlipPlayer ();
		}

		playerAnim.SetBool ("Grounded", grounded);
		playerAnim.SetFloat ("Speed", playerRB.velocity.x);

	}
	void Grounded () {
		grounded = true;
	}
	void Ungrounded () {
		grounded = false;
	}
	void FlipPlayer () {
		facingRight = !facingRight;
		transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
	}
}
