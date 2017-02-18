using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicTrash : MonoBehaviour {

    public float maxYdiff;
    public float minYdiff;
    public float maxWobbleSpeed;
    public float minWobbleSpeed;

    public float maxForwardSpeed;
    public float minForwardSpeed;

    float wobbleSpeed;
    float forwardSpeed;

    void Start ()
    {
        wobbleSpeed = Random.Range(minWobbleSpeed, maxWobbleSpeed);
        forwardSpeed = Random.Range(minForwardSpeed, maxForwardSpeed);
	}
	
	void Update ()
    {
        transform.Translate(transform.forward * Time.deltaTime * forwardSpeed, Space.World);
    }
}
