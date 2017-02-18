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
	public Animator anim;
	public float jumpHeight = .7f;
	public float jumpSpeed = 1;
	public Vector3 startPosition = Vector3.zero;
	public Rigidbody rb;
	public FloeScript floe;
	bool grounded;
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
		grounded = true;
	}

	void BearScript_PlayerRevived ()
	{
		StopAllCoroutines ();
		transform.position = startPosition;
		rb.velocity = Vector3.zero;
		rb.useGravity = true;
		playerDead = false;
		grounded = true;
	}

	void BearScript_PlayerFallDown ()
	{
		StopAllCoroutines ();
		playerDead = true;
		//rb.velocity = Vector3.zero;
		rb.useGravity = false;
	}



	// Update is called once per frame
	void FixedUpdate () {
		if (playerDead) {
			return;
		}
		rb.AddForce (new Vector3(Input.acceleration.x * pushForce,0,0),ForceMode.Acceleration);
		anim.SetBool ("IsMoving",Mathf.Abs (Input.acceleration.x) > 0.001f);
		anim.SetFloat ("MovementSpeed",-Input.acceleration.x);
	}

	void Update()
	{
		
		if (transform.position.y <=deathHeight) {
			OnPlayerFallDown ();
		}

		if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject ()) {
			return;
		}
		grounded = Physics.Raycast (transform.position, Vector3.down, .05f, LayerMask.GetMask ("Ground"));
		if(Input.GetMouseButtonDown(0) && grounded && floe.nextModel !=null && !playerDead)
		{
			
			floe.StopGettingNewFloe ();
			grounded = false;
			StartCoroutine (JumpToNextPlatform ());
		}
	}

	IEnumerator JumpToNextPlatform ()
	{
		Debug.Log ("Jumping");
		float jumpTarget = transform.position.y + jumpHeight;
		rb.useGravity = false;


		while (transform.position.y <= jumpTarget) {
			Debug.Log (transform.position.y + " " + jumpTarget);
			transform.Translate (Vector3.up * Time.deltaTime * jumpSpeed);
			yield return null;
			
		}

		yield return StartCoroutine (floe.MoveNextFloeUp ());
		Debug.Log ("dropping down again");
		rb.useGravity = true;

		while (!Physics.Raycast(transform.position,Vector3.down,.05f,LayerMask.GetMask("Ground"))) {
			Debug.Log ("Waiting For Ground");
			yield return null;
		}

		floe.PlatformCenter ();

	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawRay (transform.position, Vector3.down*.05f);
	}

}
