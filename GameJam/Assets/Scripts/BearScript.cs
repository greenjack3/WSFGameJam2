using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearScript : MonoBehaviour {
	public float topSpeed=3f;
	public Rigidbody rb;

	// Update is called once per frame
	void FixedUpdate () {
		rb.velocity = new Vector3(Mathf.Lerp(rb.velocity.x,(Input.acceleration.x * topSpeed),Mathf.Pow(0.5f,Time.deltaTime)),rb.velocity.y,rb.velocity.z);
	}

	void OnGUI()
	{
		GUI.Label(new Rect(0,0,1000,80),"Gyroscope vector : " + Input.acceleration);    
	}
}
