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
        transform.Translate(transform.up * Time.deltaTime * wobbleSpeed * Mathf.Sin(Time.timeSinceLevelLoad));
    }
}
