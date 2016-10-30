using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public int playerNumber;

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
	public GameObject deadPlayer;
	private bool orientation = true;	//true means upright, false means upside down
	private bool jumped = false;
	private bool jumpHolding = false;
	private bool toggleHolding = false;

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

		// Continuous edges
		Vector3 cameraSize = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, 0.0f, 0.0f));
		float x = rb2d.position.x;
		x = x + cameraSize.x*3;
		x = x % (cameraSize.x * 2);
		x = x - cameraSize.x;
		rb2d.position = new Vector3 (x, rb2d.position.y, 0);
	}

	void OnCollisionEnter2D(Collision2D collision) {
		print ("collided");
		if (collision.gameObject.CompareTag ("Player")) {
			SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer> ();
			sr.sprite = stunnedSprite;
			this.didCollide = true;
			this.stunTime = 1.5f;
		} else if (collision.gameObject.CompareTag ("Floor")) {
			jumped = false;
		}
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.CompareTag ("Enemy")) { // Laser
			GameObject deadLOL = Instantiate (deadPlayer, transform.position, Quaternion.identity) as GameObject;
			DestroyObject (this.gameObject);
		}
	}

	bool isStunned() {
		return this.stunTime >= 0;
	}

	public void MoveHorizontal(int d) {
		if (isStunned())
			return;
		rb2d.velocity = new Vector2(d*horizontalSpeed, rb2d.velocity.y);
	}

	public void MoveVertical(int d) {
		if (isStunned ())
			return;
		rb2d.velocity = new Vector2 (rb2d.velocity.x, d*verticalSpeed);
	}

	public void Jump() {
		if (isStunned ())
			return;
		if (!jumped) {
			jumped = true;
			rb2d.velocity = new Vector2 (rb2d.velocity.x, (rb2d.gravityScale < 0) ? -verticalSpeed : verticalSpeed);
		}
	}

	public void ToggleGravity() {
		if (isStunned ())
			return;
		rb2d.gravityScale = -rb2d.gravityScale;
		transform.rotation = Quaternion.Euler (0, 0, (rb2d.gravityScale < 0) ? 180 : 0);
	}
}