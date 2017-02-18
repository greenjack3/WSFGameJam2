using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloeScript : MonoBehaviour
{

    public float upForce;
    public float upForceTime;
    float upForceTimeLeft;

    public float minPosY;

    public float maxDownForce;
    public float minDownForce;

    public float maxTorque;
    public float minTorque;
    
    public float maxTime;
    public float minTime;
    public float maxTorqueTime;
    public float minTorqueTime;

    float timeToForce;
    float timeToTorque;

    Rigidbody body;
    public float returnDamping = 5;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        upForceTimeLeft = upForceTime;
        timeToForce = Random.Range(minTime, maxTime);
        timeToTorque = Random.Range(minTorqueTime, maxTorqueTime);
    }


    void Update()
    {
        if (timeToForce <= 0 && transform.position.y > minPosY)
        {
            float downForce = Random.Range(minDownForce, maxDownForce);
            body.AddForce(0, downForce, 0);
            timeToForce = Random.Range(minTime, maxTime);
        }

        if (upForceTimeLeft <= 0 && transform.position.y < 0)
        {
            body.AddForce(0, upForce, 0);
            upForceTimeLeft = upForceTime;
        }

        if (timeToTorque <= 0 )
        {
            Debug.Log("new torque");
            float torque = Random.Range(minTorque, maxTorque);
            body.AddTorque(0, 0, torque, ForceMode.Impulse);
            timeToTorque = Random.Range(minTorqueTime, maxTorqueTime);
        }

        //        Debug.Log(transform.rotation.eulerAngles.z);

        if (!(transform.rotation.eulerAngles.z <= 1 || transform.rotation.eulerAngles.z >= 359))
        {
            Debug.Log("returning to balance");
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity,Time.deltaTime*returnDamping);
        }
        else
        {
            timeToTorque -= Time.deltaTime;
        }

        upForceTimeLeft -= Time.deltaTime;
        timeToForce -= Time.deltaTime;
        
    }
}
