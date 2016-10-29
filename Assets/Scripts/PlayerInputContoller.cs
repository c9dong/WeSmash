using UnityEngine;
using System.Collections;

public class PlayerInputContoller : MonoBehaviour {

	PlayerController playerController;

	private KeyBinding keyBinding;

	// Use this for initialization
	void Start () {
		playerController = GetComponent<PlayerController>();
		int i = playerController.playerNumber;
		keyBinding = DefaultControls.getInstance().getKeyBinding(i);
	}
	
	// Update is called once per frame
	void Update () {
		// Up
		if (Input.GetKey(keyBinding.up)) {
			playerController.MoveVertical(1);
			playerController.SetGravity(-1);
		} else if (Input.GetKeyUp(keyBinding.up)) {
			playerController.MoveVertical(0);
		}

		// Down
		if (Input.GetKey(keyBinding.down)) {
			playerController.MoveVertical(-1);
			playerController.SetGravity(1);
		} else if (Input.GetKeyUp(keyBinding.down)) {
			playerController.MoveVertical(0);
		}

		// Left
		if (Input.GetKey(keyBinding.left)) {
			playerController.MoveHorizontal(-1);
		} else if (Input.GetKeyUp(keyBinding.left)) {
			playerController.MoveHorizontal(0);
		}

		// Right
		if (Input.GetKey(keyBinding.right)) {
			playerController.MoveHorizontal(1);
		} else if (Input.GetKeyUp(keyBinding.right)) {
			playerController.MoveHorizontal(0);
		}
	}
}
