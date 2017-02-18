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
    Rigidbody body;
    public float bounceForce;

    GameObject[] targets;
    void OnEnable()
    {
        Debug.Log("I AM ALIVE!");
        targets = GameObject.FindGameObjectsWithTag("Target");
        speed = Random.Range(minSpeed, maxSpeed);

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

    void OnCollisionEnter(Collision other)
    {
        GoLimp();
        Vector3 forceVector = (transform.position - other.contacts[0].point).normalized;
        //Debug.DrawRay(transform.position, forceVector,Color.green,10f);
        body.AddForce(forceVector * bounceForce,ForceMode.Impulse);
    }

    void GoLimp()
    {
        isMoving = false;
        body.useGravity = true;
    }
}
