﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public int playerNumber;

	public float verticalSpeed;             //Floating point variable to store the player's movement speed.
	public float horizontalSpeed;
	private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
	public string horizontalCtrl = "Horizontal_P1";
	public string verticalCtrl = "Vertical_P1";
	public bool didCollide = false;
	public float stunTime = 1.0f;
	public Sprite normalSprite;
	public Sprite stunnedSprite;

	// Use this for initialization
	void Start()
	{
		//Get and store a reference to the Rigidbody2D component so that we can access it.
		rb2d = GetComponent<Rigidbody2D> ();
		rb2d.gravityScale = 3;
	}

	//FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
	void FixedUpdate()
	{
		this.stunTime -= Time.deltaTime;
		if (this.stunTime < 0) {
			this.didCollide = false;
			SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer> ();
			sr.sprite = normalSprite;
		}

		if (this.didCollide) {
			return;
		}

		// float v = Input.GetAxis (verticalCtrl);
		// if (v < 0) {
		// 	transform.rotation = Quaternion.Euler (0, 0, 180);
		// 	rb2d.velocity = new Vector2 (rb2d.velocity.x, -verticalSpeed);
		// 	rb2d.gravityScale = -3;
		// } else if(v > 0) {
		// 	transform.rotation = Quaternion.Euler (0, 0, 0);
		// 	rb2d.velocity = new Vector2(rb2d.velocity.x, verticalSpeed);
		// 	rb2d.gravityScale = 3;
		// }


		// float h = Input.GetAxis(horizontalCtrl);
		// if (h != 0.0)
		// {
		// 	rb2d.velocity = new Vector2(h*horizontalSpeed, rb2d.velocity.y);

		// }
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("Player")) {
			SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer> ();
			sr.sprite = stunnedSprite;
			this.didCollide = true;
			this.stunTime = 1.0f;
		}
	}

	public void MoveHorizontal(int d) {
		rb2d.velocity = new Vector2(d*horizontalSpeed, rb2d.velocity.y);
	}

	public void MoveVertical(int d) {
		rb2d.velocity = new Vector2 (rb2d.velocity.x, d*verticalSpeed);
	}

	public void SetGravity(int d) {
		rb2d.gravityScale = d*3;
	}
}