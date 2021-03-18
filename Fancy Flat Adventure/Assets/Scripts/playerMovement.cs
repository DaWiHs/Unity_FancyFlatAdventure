/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {

	public GameObject player;
	private Rigidbody2D playerRb2D;
	public float speed;
	float jump;
	public float jumpSpeed;
	private bool grounded;
	public LayerMask whatIsGround;
	public Collider2D myCollider;
	private Animator myAnimator;

	// Use this for initialization
	void Start () {
		playerRb2D = GetComponent<Rigidbody2D> ();
		myCollider = GetComponent<Collider2D> ();
		myAnimator = GetComponent<Animator> ();
	}
	void Update () {

		grounded = Physics2D.IsTouchingLayers (myCollider, whatIsGround);

		//playerRb2D.velocity = new Vector2 (1 * speed, playerRb2D.velocity.y);
		if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.W) && grounded) {
			playerRb2D.velocity = new Vector2 (playerRb2D.velocity.x, 1 * jumpSpeed);
		}

		myAnimator.SetFloat ("Speed", playerRb2D.velocity.x);
		myAnimator.SetBool ("Grounded", grounded);
	}
}
*/