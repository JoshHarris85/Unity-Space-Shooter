using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour 
{
	// instances rigidbody
	private Rigidbody rb;
	//speed of movement and tilt of the ship when moving
	public float speed, tilt;
	// boundary of background for player movement
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

	private float nextFire;

	void Start () 
	{
		// get the instance of rigidbody at the start
		rb = GetComponent<Rigidbody>();
	}

	void Update () 
	{
		if (Input.GetButton ("Fire1") && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		}
	}

	void FixedUpdate ()
	{
		// player movement
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		// vector 3 xyz for the player
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		// setting velocity to the vector3 position * speed
		rb.velocity = movement * speed;
		// don't allow the player to move outside these boundaries.
		rb.position = new Vector3
		(
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax), 
			0.0f,
			Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
		);
		// rotation of the ship when moving. Only matters for z. Then -tilt to do the correct side when moving.
		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	}
}
