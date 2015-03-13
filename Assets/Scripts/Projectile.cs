using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public float damage;
	public AudioClip fireSound;

	public void Hit(){
		Destroy (this.gameObject);
	}

	public float GetDamage()	{
		return damage;
	}
}