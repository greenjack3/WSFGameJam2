using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearScript : MonoBehaviour {
	public delegate void Gameplay();
	public static event Gameplay PlayerFallDown;
	public static event Gameplay PlayerRevived;
	public float deathHeight = -5;
	bool playerDead;
	public float pushForce=3f;

	public Vector3 startPosition = Vector3.zero;
	public Rigidbody rb;
	void OnPlayerFallDown()
	{
		if (PlayerFallDown != null) {
			PlayerFallDown ();
		}
	}

	public static void RevivePlayer()
	{
		if (PlayerRevived != null) {
			PlayerRevived ();
		}
	}

	void Start()
	{
		PlayerFallDown += BearScript_PlayerFallDown;
		PlayerRevived += BearScript_PlayerRevived;
	}

	void BearScript_PlayerRevived ()
	{
		transform.position = startPosition;
		rb.velocity = Vector3.zero;
		rb.useGravity = true;
		playerDead = false;
	}

	void BearScript_PlayerFallDown ()
	{
		playerDead = true;
		rb.velocity = Vector3.zero;
		rb.useGravity = false;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (playerDead) {
			return;
		}
		rb.AddForce (new Vector3(Input.acceleration.x * pushForce,0,0),ForceMode.Acceleration);
	}

	void Update()
	{
		if (transform.position.y <=deathHeight) {
			OnPlayerFallDown ();
		}
	}


}
