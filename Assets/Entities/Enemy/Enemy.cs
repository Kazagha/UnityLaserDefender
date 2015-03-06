using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public GameObject laser;
	public float health;
	public float beamSpeed;
	public float projectileDelay = 2f;
	public float projectileVariance = 1f;
	public float shotsPerSecond = .5f;
	public int scoreValue = 75;

	private float projectileTimer;
	private ScoreKeeper keeper;

	void Start(){
		projectileTimer = Random.Range(-projectileVariance, projectileVariance);

		// Locate the score keeper
		keeper = GameObject.Find("Score").GetComponent<ScoreKeeper>() as ScoreKeeper;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.GetComponent<Projectile>()){ // && (col.tag != "Enemy")){
			// We know this object will be a projectile, so cast it 
			Projectile beam = col.gameObject.GetComponent<Projectile>();

			// Reduce the health of this by the beam's damage
			health -= beam.GetDamage();

			// Call the 'hit' method on the beam, and remove it
			beam.Hit();

			// If there is no health left destroy the object
			if(health <= 0)	{
				Destroy(this.gameObject);
				keeper.AddScore(scoreValue);
			}
		}
	}

	void Update(){
		float probability = Time.deltaTime * shotsPerSecond;

		if(Random.value < probability){
			Fire();
		}

		/*
		projectileTimer += Time.deltaTime;
		if(projectileTimer > projectileDelay)
		{
			Fire();
		}
		*/
	}

	void Fire()	{
		// Create the beam from the prefab
		GameObject beam = Instantiate(laser, transform.position + new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		// Apply velocity to the rigid body of the laser
		beam.rigidbody2D.velocity = new Vector3(0, -beamSpeed * Time.deltaTime, 0);
		
		projectileTimer = Random.Range(-projectileVariance, projectileVariance);
	}
}

