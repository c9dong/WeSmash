using UnityEngine;
using System.Collections;

public class DefaultControls {

	public static DefaultControls instance;
	private KeyBinding[] keyBindings;

	private DefaultControls() {
		keyBindings = new KeyBinding[4];

		// Player 0 keybindings
		keyBindings[0] = new KeyBinding(
			KeyCode.UpArrow, 
			KeyCode.DownArrow, 
			KeyCode.LeftArrow, 
			KeyCode.RightArrow);

		// Player 1 keybindings
		keyBindings[1] = new KeyBinding(
			KeyCode.W, 
			KeyCode.S, 
			KeyCode.A, 
			KeyCode.D);

		// Player 2 keybindings
		keyBindings[2] = new KeyBinding(
			KeyCode.U, 
			KeyCode.J, 
			KeyCode.H, 
			KeyCode.K);

		// Player 3 keybindings
		keyBindings[3] = new KeyBinding(
			KeyCode.JoystickButton16, 
			KeyCode.JoystickButton17, 
			KeyCode.JoystickButton7, 
			KeyCode.JoystickButton8);
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
