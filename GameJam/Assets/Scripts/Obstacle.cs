using System.Collections;
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
		BearScript.PlayerRevived += OnPlayerRevived;
        StartCoroutine(fetchTarget());
    }

    void OnPlayerRevived ()
    {
		this.Recycle ();
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
        GoLimp(collision);
        
        //Debug.DrawRay(transform.position, forceVector,Color.green,10f);
        
    }

    void GoLimp(Collision collision)
    {
        isMoving = false;
        body.useGravity = true;
        Vector3 forceVector = (transform.position - collision.contacts[0].point).normalized;
        body.AddForce(forceVector * bounceForce, ForceMode.Impulse);
        StartCoroutine (DisableFloeDestruction ());
    }

	void OnDisable()
	{
		BearScript.PlayerRevived -= OnPlayerRevived;
	}

	IEnumerator DisableFloeDestruction ()
	{
		yield return new WaitForFixedUpdate ();
		ableToDestroyFloe = false;
	}
}
