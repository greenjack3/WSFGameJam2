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

    public bool underseaFlag;
    public float maxLifeTime;
    public float minLifeTime;
    float lifeTime;

    void OnEnable()
    {
       
        maxY = transform.position.y + maxYdiff;
        minY = transform.position.y - minYdiff;
        wobbleSpeed = Random.Range(minWobbleSpeed, maxWobbleSpeed);
        forwardSpeed = Random.Range(minForwardSpeed, maxForwardSpeed);
        lifeTime = Random.Range(minLifeTime, maxLifeTime);
    }

    void Update()
    {
        Debug.Log("y: " + transform.position.y);
        transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed);
		float c = (Mathf.Abs(maxY-minY) / 2);
        transform.Translate(Vector3.up * ((c * Mathf.Sin(wobbleSpeed * Time.time) + (c + minY)) * Time.deltaTime));

        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0 && underseaFlag)
        {
            transform.Translate(Vector3.up * Time.deltaTime * -wobbleSpeed);
        }

        if (transform.position.x <= -55 || transform.position.x >= 55)
        {
            this.Recycle();
        }
        if (transform.position.y <= -10)
        {
            this.Recycle();
        }
    }
}
