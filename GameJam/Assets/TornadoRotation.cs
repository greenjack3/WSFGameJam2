using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoRotation : MonoBehaviour {

    public float yrot;
    void Update()
    {
        transform.Rotate(new Vector3(0, yrot, 0) * Time.deltaTime);
    }
}
