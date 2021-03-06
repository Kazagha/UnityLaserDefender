﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public GameObject laser;
	public float health;
	public GameObject PlayerThruster;

	// Beam Variables
	public float beamSpeed = 500f;
	public float beamOffSet = 0.3f;
	public float beamRepeatRate = 0.2f;
	public float speed = 15f;
	public float padding = 0.5f;
	private float beamSide = 1f;
	private Animator anim;

	private float xMax;
	private float xMin;

	// Use this for initialization
	void Start () {
		// Find the main camera
		Camera camera = Camera.main;

		// Find distance between the camera and the object
		float distance = transform.position.z - camera.transform.position.z;

		// Where bottom left is 0,0 top right corner is 1,1

		// Find the X Min
		xMin = camera.ViewportToWorldPoint(new Vector3(0, 0, distance)).x + padding;
		// Find the X Max
		xMax = camera.ViewportToWorldPoint(new Vector3(1, 1, distance)).x - padding;

		//Vector3 thrustPos = this.transform.position + new Vector3(0, -0.5f , 0f);
		Vector3 thrustPos = this.transform.position + new Vector3(0, -0.3f , 0f);
		GameObject thrust = Instantiate(PlayerThruster, thrustPos, Quaternion.Euler(80, 180, 180)) as GameObject;
		//GameObject thrust = Instantiate(PlayerThruster, thrustPos, Quaternion.identity) as GameObject;
		thrust.transform.parent = this.transform;

		anim = this.gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKey(KeyCode.LeftArrow))
		{
			transform.position = new Vector3(
												Mathf.Clamp(this.transform.position.x - speed * Time.deltaTime, xMin, xMax),
			                                    transform.position.y,
			                                    transform.position.z);
		} else if(Input.GetKey(KeyCode.RightArrow)) {
			transform.position = new Vector3(
												Mathf.Clamp(this.transform.position.x + speed * Time.deltaTime, xMin, xMax),
			                                    transform.position.y,
			                                    transform.position.z);
		}

		if(Input.GetKeyDown(KeyCode.Space)) {
			InvokeRepeating("FireBeam", 0.0001f, beamRepeatRate);
		}

		if(Input.GetKeyUp(KeyCode.Space)) {
			CancelInvoke("FireBeam");
		}
	}	

	void FireBeam()	{
		// Swap the side that the beam fires on
		if(beamSide == 1){
			beamSide = -1;
			//anim.Play("LaserLeft");
		} else {
			beamSide = 1;
			//anim.Play("LaserRight");
		}
		
		// Create the beam from the prefab
		GameObject beam = Instantiate(laser, transform.position + new Vector3((beamSide * beamOffSet), 0, 0), Quaternion.identity) as GameObject;
		// Apply velocity to the rigid body of the laser
		beam.rigidbody2D.velocity = new Vector3(0, beamSpeed * Time.deltaTime, 0);
		// Play a souud
		AudioSource.PlayClipAtPoint(beam.audio.clip, transform.position, 0.1f);
	}

	void OnTriggerEnter2D(Collider2D col) {
		//if(col.gameObject.GetComponent<Projectile>()&& (col.tag != "Player")){
			// We know this object will be a projectile, so cast it 
			Projectile beam = col.gameObject.GetComponent<Projectile>();
			
			// Reduce the health of this by the beam's damage
			health -= beam.GetDamage();
			
			// Call the 'hit' method on the beam, and remove it
			beam.Hit();
			
			// If there is no health left destroy the object
			if(health <= 0)	{
				Die ();
			}
	}

	void Die(){
		LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>() as LevelManager;
		Destroy(this.gameObject);
		man.LoadLevel("Lose");
	}
}
