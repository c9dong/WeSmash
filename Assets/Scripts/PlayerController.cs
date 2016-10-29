using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float verticalSpeed;             //Floating point variable to store the player's movement speed.
	public float horizontalSpeed;
	private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
	public string horizontalCtrl = "Horizontal_P1";
	public string jumpCtrl = "Jump_P1";
	public string toggleCtrl = "Toggle_P1";
	public bool didCollide = false;
	public float stunTime = 1.0f;
	public Sprite normalSprite;
	public Sprite stunnedSprite;
	private bool orientation = true;	//true means upright, false means upside down
	private bool jumped = false;
	private bool jumpHolding = false;
	private bool toggleHolding = false;

	// Use this for initialization
	void Start()
	{
		//Get and store a reference to the Rigidbody2D component so that we can access it.
		rb2d = GetComponent<Rigidbody2D> ();
		orientation = !orientation;
		transform.rotation = Quaternion.Euler (0, 0, orientation ? 180 : 0);
		rb2d.gravityScale = orientation ? -3 : 3;
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

		float j = Input.GetAxisRaw (jumpCtrl);
		if (j != 0 && !jumped && !jumpHolding) {
			jumped = true;
			jumpHolding = true;
			rb2d.velocity = new Vector2 (rb2d.velocity.x, orientation ? -verticalSpeed : verticalSpeed);
		}
		if (j == 0) {
			jumpHolding = false;
		}

		float t = Input.GetAxisRaw (toggleCtrl);
		if (t != 0 && !toggleHolding) {
			toggleHolding = true;
			orientation = !orientation;
			transform.rotation = Quaternion.Euler (0, 0, orientation ? 180 : 0);
			rb2d.gravityScale = orientation ? -3 : 3;
		}
		if (t == 0) {
			toggleHolding = false;
		}


		float h = Input.GetAxis(horizontalCtrl);
		if (h != 0.0)
		{
			rb2d.velocity = new Vector2(h*horizontalSpeed, rb2d.velocity.y);

		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		print ("collided");
		if (collision.gameObject.CompareTag ("Player")) {
			SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer> ();
			sr.sprite = stunnedSprite;
			this.didCollide = true;
			this.stunTime = 2.0f;
		} else if (collision.gameObject.CompareTag ("Floor")) {
			jumped = false;
		}
	}
}