using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour
{

	private Rigidbody2D rb2d;
	public bool isLeft;

	// Use this for initialization
	void Start ()
	{
		rb2d = GetComponent<Rigidbody2D> ();

		if (isLeft) {
			rb2d.velocity = new Vector2 (10, 0);
		} else {
			rb2d.velocity = new Vector2 (-10, 0);
		}
	}

	public void OnBecameInvisible()
	{
		DestroyObject (gameObject);
	}

	// Update is called once per frame
	void Update ()
	{

	}
}