using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicTrash : MonoBehaviour
{

    public float maxYdiff;
    float maxY;
    public float minYdiff;
    float minY;
    public float maxWobbleSpeed;
    public float minWobbleSpeed;
    
    public float maxForwardSpeed;
    public float minForwardSpeed;

    float wobbleSpeed;
    float forwardSpeed;

    void OnEnable()
    {
        maxY = transform.position.y + maxYdiff;
        minY = transform.position.y - minYdiff;
        wobbleSpeed = Random.Range(minWobbleSpeed, maxWobbleSpeed);
        forwardSpeed = Random.Range(minForwardSpeed, maxForwardSpeed);
    }

    void Update()
    {
        transform.Translate(transform.forward * Time.deltaTime * forwardSpeed, Space.World);
        float c = (Mathf.Abs(maxY) + Mathf.Abs(minY)) / 2;
        transform.position = Vector3.Lerp(transform.position,

            new Vector3(transform.position.x,
            (Time.deltaTime * (c * Mathf.Sin(wobbleSpeed * Time.time) + (c + minY))),
            transform.position.z),
            Mathf.Pow(0.5f, Time.deltaTime));


    }
}
