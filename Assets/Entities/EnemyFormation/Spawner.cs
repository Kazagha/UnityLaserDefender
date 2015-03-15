using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public EnemyFormation[] formation;

	private float padding = 0.5f;
	private int count;

	// Use this for initialization
	void Start () {
		count = 0;

		SpawnNext();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SpawnNext(){
		EnemyFormation form = formation[count++] as EnemyFormation;
		
		// Find the main camera
		Camera camera = Camera.main;
		
		// Find distance between the camera and the object
		float distance = transform.position.z - camera.transform.position.z;
		
		// Find the Y Max
		float yMax = camera.ViewportToWorldPoint(new Vector3(1, 1, distance)).y - (form.height * 0.5f) - padding;
		
		Instantiate (formation[0], new Vector3(0, yMax, 0), Quaternion.identity);
	}
}
