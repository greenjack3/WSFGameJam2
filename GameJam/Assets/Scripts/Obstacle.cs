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
        targets = GameObject.FindGameObjectsWithTag("Target");
        speed = Random.Range(minSpeed, maxSpeed);

        int selector = Random.Range(0, targets.Length);
        Transform target = targets[selector].transform;
        transform.LookAt(target);
    }

    void Update()
    {
        transform.Translate(transform.forward * Time.deltaTime * speed, Space.World);
    }
}
