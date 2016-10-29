using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class MapController : MonoBehaviour {
	public int rows = 10;
	public int columns = 10;

	public GameObject floorTile;
	public GameObject player;

	private Transform platformHolder;
	private Transform boardHolder;
	private List<Vector3> gridPositions = new List<Vector3>();

	void initList() {
		gridPositions.Clear ();
		for (int i=0; i<rows; i++) {
			for (int j=0; j<columns; j++) {
				gridPositions.Add(new Vector3(i,j,0f));
			}
		}
	}

	void setupFloor() {
		boardHolder = GameObject.Find ("Map").transform;
		for (int x = -columns/2; x < columns/2; x++) {
			GameObject instance1 = Instantiate (floorTile, new Vector3 (x, -rows/2, 0f), Quaternion.identity) as GameObject;
			GameObject instance2 = Instantiate (floorTile, new Vector3 (x, rows/2, 0f), Quaternion.identity) as GameObject;

			instance1.transform.SetParent (boardHolder);
			instance2.transform.SetParent (boardHolder);
		}

		for (int y = -rows / 2; y < rows / 2+1; y++) {
			GameObject instance1 = Instantiate (floorTile, new Vector3 (-columns/2, y, 0f), Quaternion.identity) as GameObject;
			GameObject instance2 = Instantiate (floorTile, new Vector3 (columns/2, y, 0f), Quaternion.identity) as GameObject;

			instance1.transform.SetParent (boardHolder);
			instance2.transform.SetParent (boardHolder);
		}
	}

	void setupPlatform() {
		platformHolder = GameObject.Find ("Platforms").transform;

		int i = -columns / 2 + 1;
		int lastY = 0;
		while (true) {
			int offset = Random.Range (0, 3);
			i = i + offset;
			int y = Random.Range(-rows/2 + 3, rows/2 - 3);
			// Make sure the y from the last y isn't too close
			if (Mathf.Abs (y - lastY) < 4) {
				if (y >= lastY) {
					y = Mathf.Min (y + 2, rows / 2 - 3);
				} else {
					y = Mathf.Max (y - 2, -rows / 2 + 3);
				}
			}
			int size = Random.Range (3, 6);
			// Break if there is not space to put the platform
			if (i + size > columns / 2-1)
				break;
			createPlatform(platformHolder, i, y, size);
			i = i + size + 2;
		}
	}

	void createPlatform (Transform parent, int x, int y, int length) {
		for (int i=0; i<length; i++) {
			GameObject instance = Instantiate (floorTile, new Vector3 (x+i, y, 0f), Quaternion.identity) as GameObject;
			instance.transform.SetParent (parent);
		}
	}

	void initPlayer() {
		GameObject instance1 = Instantiate (player, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		GameObject instance2 = Instantiate (player, new Vector3 (2, 0, 0), Quaternion.identity) as GameObject;
		PlayerController p2 = instance2.GetComponent <PlayerController>();
		p2.horizontalCtrl = "Horizontal_P2";
		p2.jumpCtrl = "Jump_P2";
		p2.toggleCtrl = "Toggle_P2";
	}

	public void setupScene() {
		setupFloor ();
		setupPlatform ();
		initPlayer ();
	}
}
