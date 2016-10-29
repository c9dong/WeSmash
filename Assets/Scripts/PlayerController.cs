using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float verticalSpeed;             //Floating point variable to store the player's movement speed.
	public float horizontalSpeed;
	private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
	public string horizontalCtrl = "Horizontal_P1";
	public string verticalCtrl = "Vertical_P1";

	// Use this for initialization
	void Start()
	{
		//Get and store a reference to the Rigidbody2D component so that we can access it.
		rb2d = GetComponent<Rigidbody2D> ();
	}

	//FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
	void FixedUpdate()
	{
		float v = Input.GetAxis (verticalCtrl);
		if (v < 0) {
			transform.rotation = Quaternion.Euler (0, 0, 0);
			rb2d.velocity = new Vector2 (rb2d.velocity.x, -verticalSpeed);

		} else if(v > 0) {
			transform.rotation = Quaternion.Euler (0, 0, 180);
			rb2d.velocity = new Vector2(rb2d.velocity.x, verticalSpeed);
		}


		float h = Input.GetAxis(horizontalCtrl);
		if (h != 0.0)
		{
			rb2d.velocity = new Vector2(h*horizontalSpeed, rb2d.velocity.y);

		}
	}
}