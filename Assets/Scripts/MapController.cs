using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapController : MonoBehaviour {
	public int rows = 10;
	public int columns = 10;

	public GameObject floorTile;
	public GameObject player;

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
	}

	public void initPlayer() {
		GameObject instance1 = Instantiate (player, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		GameObject instance2 = Instantiate (player, new Vector3 (2, 0, 0), Quaternion.identity) as GameObject;
		PlayerController p2 = instance2.GetComponent <PlayerController>();
		p2.horizontalCtrl = "Horizontal_P2";
		p2.verticalCtrl = "Vertical_P2";
	}

	public void setupScene() {
		setupFloor ();
		initPlayer ();
	}
}
