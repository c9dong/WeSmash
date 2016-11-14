using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public int playerNumber;

	public float verticalSpeed;             //Floating point variable to store the player's movement speed.
	public float horizontalSpeed;
	public Sprite normalSprite;
	public Sprite stunnedSprite;
	public GameObject deadPlayer;

	private static float stunDuration = 1.5f;
	private static float defaultGravityScale = 3;
	
	private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
	Animator animator;

	private float stunTime = -1;
	private bool jumped = false;
	private bool isDead = false;
	private bool faceRight = false;
	private GUIStyle style = new GUIStyle();

	void OnGUI() {
		style.normal.textColor = Color.white;
		GUI.Label(new Rect(Camera.main.WorldToScreenPoint(gameObject.transform.position).x - 25, Screen.height - Camera.main.WorldToScreenPoint(gameObject.transform.position).y - 50, 20, 20), "Player " + playerNumber.ToString(), style);
	}

	private bool invincible = false;

	// Use this for initialization
	void Start()
	{
		//Get and store a reference to the Rigidbody2D component so that we can access it.
		rb2d = GetComponent<Rigidbody2D> ();
		rb2d.gravityScale = defaultGravityScale;

		animator = this.GetComponent<Animator> ();
		animator.SetBool ("walk", false);
		animator.SetBool ("dead", false);
		animator.SetBool ("jump", false);
	}

	//FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
	void FixedUpdate()
	{
		this.stunTime -= Time.deltaTime;
		if (this.stunTime < 0) {
			SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer> ();
			sr.sprite = normalSprite;
			animator.enabled = true;
		}

		// Continuous edges
		Vector3 cameraSize = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, 0.0f, 0.0f));
		float x = rb2d.position.x;
		x = x + cameraSize.x*3;
		x = x % (cameraSize.x * 2);
		x = x - cameraSize.x;
		rb2d.position = new Vector3 (x, rb2d.position.y, 0);

		if (rb2d.velocity.x == 0) {
			animator.SetBool ("walk", false);
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		print ("collided");
		if (collision.gameObject.CompareTag ("Player")) {
			animator.SetBool ("walk", false);
			animator.SetBool ("jump", false);

			// SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer> ();
			// sr.sprite = stunnedSprite;
			this.stunTime = PlayerController.stunDuration;
		} else if (collision.gameObject.CompareTag ("Floor")) {
			jumped = false;
			animator.SetBool ("jump", false);
		}
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.CompareTag ("Enemy") && !invincible) { // Laser
			animator.SetBool ("dead", true);
			isDead = true;
			rb2d.simulated = false;
			// GameObject deadLOL = Instantiate (deadPlayer, transform.position, Quaternion.identity) as GameObject;
			// DestroyObject (this.gameObject);
		}
		if (collision.gameObject.CompareTag ("Barrier")) {
			invincible = true;
		}
	}

	void OnTriggerExit2D(Collider2D collision) {
		if (collision.gameObject.CompareTag ("Barrier")) {
			invincible = false;
		}
	}

	bool isStunned() {
		return this.stunTime > 0;
	}

	public void MoveHorizontal(int d) {
		if (isStunned())
			return;
		rb2d.velocity = new Vector2(d*horizontalSpeed, rb2d.velocity.y);

		animator.SetBool ("walk", true);
		if (rb2d.gravityScale > 0) {
			if (d < 0) {
				if (!faceRight) {
					Flip();
				}
			} else if (d > 0) {
				if (faceRight) {
					Flip();
				}
			}
		} else {
			if (d < 0) {
				if (faceRight) {
					Flip();
				}
			} else if (d > 0) {
				if (!faceRight) {
					Flip();
				}
			}
		}
		
		
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
			animator.SetBool ("jump", true);
			rb2d.velocity = new Vector2 (rb2d.velocity.x, (rb2d.gravityScale < 0) ? -verticalSpeed : verticalSpeed);
		}
	}

	public void ToggleGravity() {
		if (isStunned ())
			return;
		rb2d.gravityScale = -rb2d.gravityScale;
		transform.rotation = Quaternion.Euler (0, 0, (rb2d.gravityScale < 0) ? 180 : 0);
	}

	public bool isPlayerDead() {
		return isDead;
	}

	private void Flip() {
		faceRight = !faceRight;
		Vector2 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}