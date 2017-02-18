﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class FloeScript : MonoBehaviour
{
	public GameObject floeModelPrefab;

	public string recentlyCollided {
		get;
		set;
	}

	GameObject currentModel;

    public float upForce;
    public float upForceTime;
    float upForceTimeLeft;
    public float floatingMultiplier;

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
		currentModel = Instantiate (floeModelPrefab, transform) as GameObject;
		StartCoroutine (CheckForDestruction ());

		BearScript.PlayerRevived += OnPlayerRevived;
    }

	IEnumerator CheckForDestruction()
	{
		recentlyCollided = "";
		List<Transform> leftMostObj = new List<Transform> ();
		List<Transform> rightMostObj = new List<Transform> ();
		foreach (var item in currentModel.GetComponentsInChildren<Transform>()) {
			if (item == transform) {
				continue;
			}
			if (item.name.Contains ("L0")) {
				leftMostObj.Add (item);
			}
			if (item.name.Contains ("R0")) {
				rightMostObj.Add (item);
			}
		}

		leftMostObj.Sort(new System.Comparison<Transform>((x,y) => string.Compare(x.name, y.name)));
		rightMostObj.Sort (new System.Comparison<Transform>((x,y) => string.Compare(x.name, y.name)));

		while (true) {
			if (recentlyCollided.Contains ("L0") && leftMostObj.Count > 0) {
				if (recentlyCollided == leftMostObj.Last ().name) {
					GameObject obj = leftMostObj.Last ().gameObject;
					obj.transform.SetParent (null);
					obj.AddComponent<Rigidbody> ();
					obj.GetComponent<Collider> ().isTrigger = false;
					leftMostObj.Remove (obj.transform);
				}
			}
			if (recentlyCollided.Contains ("R0") && rightMostObj.Count>0) {
				if (recentlyCollided == rightMostObj.Last ().name) {
					GameObject obj = rightMostObj.Last ().gameObject;
					obj.AddComponent<Rigidbody> ();
					obj.transform.SetParent (null);
					obj.GetComponent<Collider> ().isTrigger = false;
					rightMostObj.Remove (obj.transform);
				}
			}
			yield return null;
		}
	}

    void OnPlayerRevived ()
    {
		StopAllCoroutines ();
		Destroy (currentModel);
		currentModel = Instantiate (floeModelPrefab, transform) as GameObject;
		StartCoroutine (CheckForDestruction ());
		transform.position = Vector3.zero;
		transform.rotation = Quaternion.identity;
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
            float differenceY = Mathf.Abs(0 - transform.position.y);
            float floatingPower = differenceY * floatingMultiplier;
            body.AddForce(0, upForce * floatingPower, 0);
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
//            Debug.Log("returning to balance");
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
