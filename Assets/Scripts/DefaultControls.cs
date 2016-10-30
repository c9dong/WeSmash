using UnityEngine;
using System.Collections;

public class DefaultControls {

	public static DefaultControls instance;
	private KeyBinding[] keyBindings;

	private DefaultControls() {
		keyBindings = new KeyBinding[4];

		keyBindings[0] = new KeyBinding(
			KeyCode.UpArrow, 
			KeyCode.DownArrow, 
			KeyCode.LeftArrow, 
			KeyCode.RightArrow);

		keyBindings[1] = new KeyBinding(
			KeyCode.W, 
			KeyCode.S, 
			KeyCode.A, 
			KeyCode.D);

		keyBindings[2] = new KeyBinding(
			KeyCode.U, 
			KeyCode.J, 
			KeyCode.H, 
			KeyCode.K);

		// keyBindings[3] = new KeyBinding(
		// 	KeyCode.UpArrow, 
		// 	KeyCode.DownArrow, 
		// 	KeyCode.LeftArrow, 
		// 	KeyCode.RightArrow);
	}

	public static DefaultControls getInstance() {
		if (instance == null) {
			instance = new DefaultControls();
		}
		return instance;
	}

	public KeyBinding getKeyBinding(int playerNum) {
		return keyBindings[playerNum];
	}
}
