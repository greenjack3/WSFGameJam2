﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float maxSpeed;
    public float minSpeed;
    float speed;

    float maxTimeToLimp;
    float minTimeToLimp;

    bool isMoving;

	bool ableToDestroyFloe;

    Rigidbody body;
    public float bounceForce;

    GameObject[] targets;
    void OnEnable()
    {
        Debug.Log("I AM ALIVE!");
        targets = GameObject.FindGameObjectsWithTag("Target");
        speed = Random.Range(minSpeed, maxSpeed);
		ableToDestroyFloe = true;
        isMoving = true;
        body = GetComponent<Rigidbody>();

        StartCoroutine(fetchTarget());
    }
    IEnumerator fetchTarget()
    {
        yield return null;
        int selector = Random.Range(0, targets.Length);
        Transform target = targets[selector].transform;
        transform.LookAt(target);
    }

    void Update()
    {
        if (isMoving)
        {
            transform.Translate(transform.forward * Time.deltaTime * speed, Space.World);
        }
        if (transform.position.y <= -9)
        {
            this.Recycle();
        }
    }

	void OnTriggerEnter(Collider other)
	{
		if (ableToDestroyFloe) {
			return;
		}
		if (other.tag == "Ground") {
			other.GetComponentInParent<FloeScript> ().recentlyCollided = other.transform.name;
		}
	}

	void OnCollisionEnter(Collision collision)
    {
        GoLimp();
        Vector3 forceVector = (transform.position - collision.contacts[0].point).normalized;
        //Debug.DrawRay(transform.position, forceVector,Color.green,10f);
        body.AddForce(forceVector * bounceForce,ForceMode.Impulse);
    }

    void GoLimp()
    {
        isMoving = false;
        body.useGravity = true;
		StartCoroutine (DisableFloeDestruction ());
    }

	IEnumerator DisableFloeDestruction ()
	{
		yield return new WaitForFixedUpdate ();
		ableToDestroyFloe = false;
	}
}
