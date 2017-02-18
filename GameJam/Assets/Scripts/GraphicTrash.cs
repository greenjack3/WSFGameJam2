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
		transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed);
		float c = (Mathf.Abs(maxY-minY) / 2);
		transform.position = new Vector3 (transform.position.x,
			((c * Mathf.Sin (wobbleSpeed * Time.time) + (c + minY))),
			transform.position.z);

        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0 && underseaFlag)
        {
            transform.Translate(Vector3.up * Time.deltaTime * -wobbleSpeed);
        }

        if (transform.position.x <= -20 || transform.position.x >= 20)
        {
            this.Recycle();
        }
        if (transform.position.y <= -10)
        {
            this.Recycle();
        }
    }
}
