using UnityEngine;
using System.Collections;

public class EnemyFormation : MonoBehaviour {
	public GameObject enemyPrefab;
	public float width = 5f;
	public float height = 4f;
	public float speed = 2.5f;

	private float xMinFormation, xMaxFormation;
	//private enum Direction {LEFT, RIGHT};
	//private Direction direction;
	private float direction;

	// Use this for initialization
	void Start () {
		foreach(Transform child in transform) {
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}

		if(Random.Range (0, 2) == 1)
		{
			direction = 1;
		} else {
			direction = -1;
		}

		// Find the main camera
		Camera camera = Camera.main;
		
		// Find distance between the camera and the object
		float distance = transform.position.z - camera.transform.position.z;

		// Set up the padding
		float padding = width * 0.5f;

		// Find the X Min
		xMinFormation = camera.ViewportToWorldPoint(new Vector3(0, 0, distance)).x + padding;
		// Find the X Max
		xMaxFormation = camera.ViewportToWorldPoint(new Vector3(1, 1, distance)).x - padding;
	}

	void OnDrawGizmos()	{
		// Find min - max values of x y coordinates
		float xmin, xmax, ymin, ymax;
		xmin = transform.position.x - (width * 0.5f);
		xmax = transform.position.x + (width * 0.5f);
		ymin = transform.position.y - (height * 0.5f);
		ymax = transform.position.y + (height * 0.5f);

		// Draw the lines around the formation
		Gizmos.DrawLine(new Vector3(xmin, ymax, 0), new Vector3(xmax, ymax, 0));
		Gizmos.DrawLine(new Vector3(xmax, ymax, 0), new Vector3(xmax, ymin, 0));
		Gizmos.DrawLine(new Vector3(xmax, ymin, 0), new Vector3(xmin, ymin, 0));
		Gizmos.DrawLine(new Vector3(xmin, ymin, 0), new Vector3(xmin, ymax, 0));
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if(direction == Direction.LEFT) {
			transform.position = new Vector3(
				Mathf.Clamp(this.transform.position.x - speed * Time.deltaTime, xMinFormation, xMaxFormation),
				transform.position.y,
				transform.position.z);

			if(this.transform.position.x == xMinFormation) {
				Debug.Log("Min Position");
				direction = Direction.RIGHT;
			}
		} else {
			transform.position = new Vector3(
				Mathf.Clamp(this.transform.position.x + speed * Time.deltaTime, xMinFormation, xMaxFormation),
				transform.position.y,
				transform.position.z);

			if(this.transform.position.x == xMaxFormation) {
				Debug.Log("Max Position");
				direction = Direction.LEFT;
			}
		}
		*/


		// Check for the right edge of the screen
		if(transform.position.x > xMaxFormation) {
			Debug.Log("Max Position");
			direction = -1;
		}

		// Check for the left edge of the screen
		if(transform.position.x < xMinFormation) {
			Debug.Log("Max Position");
			direction = 1;
		}

		// Move the formation
		transform.position += new Vector3(direction * speed * Time.deltaTime, 0, 0);
	}
}
