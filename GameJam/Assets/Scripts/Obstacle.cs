using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float maxSpeed;
    public float minSpeed;
    float speed;

    GameObject[] targets;
    void OnEnable()
    {
        Debug.Log("I AM ALIVE!");
        targets = GameObject.FindGameObjectsWithTag("Target");
        speed = Random.Range(minSpeed, maxSpeed);

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
        transform.Translate(transform.forward * Time.deltaTime * speed, Space.World);
    }
}
