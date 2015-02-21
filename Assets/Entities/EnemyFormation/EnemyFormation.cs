using UnityEngine;
using System.Collections;

public class EnemyFormation : MonoBehaviour {
	public GameObject enemyPrefab;
	public float width = 5f;
	public float height = 4f;
	public float speed = 2.5f;
	public float spawnDelaySeconds = 1f;

	private float xMinFormation, xMaxFormation;
	//private enum Direction {LEFT, RIGHT};
	//private Direction direction;
	private float direction;

	// Use this for initialization
	void Start () {
		//SpawnFullFormation();
		SpawnUntilFull();

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
	

		/*
		 * Original Update Function
		 * 
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

	// Update is called once per frame
	void Update () {

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

		// Check if all enemies are dead
		if(AllMembersDead())		{
			SpawnUntilFull();
		}
	}

	/*
	 * Begin spawning until the formaiton has been filled
	 */
	void SpawnUntilFull(){
		// Fetch the next free position
		Transform freePos = NextFreePosition();
		// Create the enemy in the specified position
		GameObject Enemy = (Instantiate (enemyPrefab, freePos.position, Quaternion.identity)) as GameObject;
		Enemy.transform.parent = freePos;
		
		if(FreePostiton()){
			Invoke("SpawnUntilFull", spawnDelaySeconds);
		}
	}

	/*
	 * Find the next free position to spawn an enemy
	 */
	Transform NextFreePosition(){
		foreach(Transform enemyPos in transform) {
			if(enemyPos.childCount <= 0){
				return enemyPos;
			}
		}
		return null;
	}

	/*
	 * Check if there are free positions
	 */
	private bool FreePostiton() {
		// Check if any 'transform' objects have enemies attached
		foreach(Transform enemyPos in transform) {
			if(enemyPos.childCount > 0){
				return true;
			}
		}
		// Failing that return false
		return false;
	}

	/*
	 * Check if all members have been killed
	 */
	private bool AllMembersDead() {
		// Check if any 'transform' objects have enemies attached
		foreach(Transform enemyPos in transform) {
			if(enemyPos.childCount > 0){
				return false;
			}
		}
		// Failing that return true
		return true;
	}

	/*
	 * Instantly spawn the full formation of enemies
	 */
	void SpawnFullFormation(){
		foreach(Transform enemyPos in transform) {
			GameObject enemy = Instantiate (enemyPrefab, enemyPos.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = enemyPos;
		}
		
		if(Random.Range (0, 2) == 1)
		{
			direction = 1;
		} else {
			direction = -1;
		}
	}
}
