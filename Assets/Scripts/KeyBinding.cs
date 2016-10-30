using UnityEngine;
using System.Collections;

public class KeyBinding {

	public KeyCode up = KeyCode.UpArrow;
	public KeyCode down = KeyCode.DownArrow;
	public KeyCode right = KeyCode.RightArrow;
	public KeyCode left = KeyCode.LeftArrow;

	public KeyBinding(KeyCode up, KeyCode down, KeyCode left, KeyCode right) {
		this.up = up;
		this.down = down;
		this.left = left;
		this.right = right;
	}
}
