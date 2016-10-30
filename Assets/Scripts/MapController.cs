using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class MapController : MonoBehaviour {
	public int rows = 10;
	public int columns = 10;

	public GameObject floorTile;
	public GameObject player1;
	public GameObject player2;
	public GameObject player3;
	public GameObject projectile;

	private float projectileTime = 0;
	private static readonly float PROJECTILE_INTERVAL = 1; // Generate projectile every 2 seconds

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

//		for (int y = -rows / 2; y < rows / 2+1; y++) {
//			GameObject instance1 = Instantiate (floorTile, new Vector3 (-columns/2, y, 0f), Quaternion.identity) as GameObject;
//			GameObject instance2 = Instantiate (floorTile, new Vector3 (columns/2, y, 0f), Quaternion.identity) as GameObject;
//
//			instance1.transform.SetParent (boardHolder);
//			instance2.transform.SetParent (boardHolder);
//		}
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

	public void createProjectile() {
		GameObject projectileObj = Instantiate (projectile, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		ProjectileController projectileCtrl = projectileObj.GetComponent<ProjectileController> ();
		projectileCtrl.isLeft = (Random.value >= 0.5);

		float projectileX;
		float projectileY = Random.Range (0, Screen.height);
		if (projectileCtrl.isLeft) {
			projectileX = 0.0F; 
			projectileObj.transform.position = Camera.current.ScreenToWorldPoint (new Vector3 (projectileX, projectileY, Camera.current.nearClipPlane));
			projectileObj.transform.position = new Vector3 (projectileObj.transform.position.x - projectileObj.GetComponent<Renderer>().bounds.size.x / 3, projectileObj.transform.position.y, projectileObj.transform.position.z); 
		} else {
			projectileX = Screen.width;
			projectileObj.transform.position = Camera.current.ScreenToWorldPoint (new Vector3 (projectileX, projectileY, Camera.current.nearClipPlane));
			projectileObj.transform.position = new Vector3 (projectileObj.transform.position.x + projectileObj.GetComponent<Renderer>().bounds.size.x / 3, projectileObj.transform.position.y, projectileObj.transform.position.z);
		}

		print("Create projectile (" + projectileX + "," + projectileY + ")");
	}

	void initPlayer() {
		GameObject[] players = { player1, player2, player3 };
		for (int i = 0; i < 3; i++) {
			GameObject instance = Instantiate (players[i], new Vector3 (-5 + i*5, 0, 0), Quaternion.identity) as GameObject;
			PlayerController pc = instance.GetComponent <PlayerController>();
			pc.playerNumber = i;
		}
	}

	public void setupScene() {
		setupFloor ();
		setupPlatform ();
		initPlayer ();
	}

	void FixedUpdate() {
		if (Time.time >= projectileTime && Camera.current != null) {
			createProjectile();
			projectileTime += PROJECTILE_INTERVAL;
		}
	}
}
