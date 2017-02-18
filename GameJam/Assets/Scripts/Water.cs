using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public float maxYdiff;
    public float minYdiff;
    float maxY;
    float minY;
    public float maxWobbleSpeed;
    public float minWobbleSpeed;
    float wobbleSpeed;
    float newWobbleSpeed;
    public float maxWobbleSpeedChangeTime;
    public float minWobbleSpeedChangeTime;
    float wobbleSpeedChangeTime;
    public float maxTextureOffsetSpeed;
    public float minTextureOffsetSpeed;
    float textureOffsetSpeed;
    float newTextureOffsetSpeed;
    public float lerpSpeedW;
    public float lerpSpeedO;
    public float maxOffsetSpeedTime;
    public float minOffsetSpeedTime;
    float offsetSpeedTime;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();

        maxY = transform.position.y + maxYdiff;
        minY = transform.position.y - minYdiff;
        wobbleSpeed = Random.Range(minWobbleSpeed, maxWobbleSpeed);
        textureOffsetSpeed = Random.Range(minTextureOffsetSpeed, maxTextureOffsetSpeed);

        wobbleSpeedChangeTime = Random.Range(minWobbleSpeedChangeTime, maxWobbleSpeedChangeTime);
        offsetSpeedTime = Random.Range(minOffsetSpeedTime, maxOffsetSpeedTime);
    }


    void Update()
    {
        float c = (Mathf.Abs(maxY - minY) / 2);
        transform.position = new Vector3(transform.position.x,
            ((c * Mathf.Sin(wobbleSpeed * Time.time) + (c + minY))),
            transform.position.z);

        float offset = Time.time * textureOffsetSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(-offset, 0));

        if (wobbleSpeedChangeTime <= 0)
        {
            newWobbleSpeed = Random.Range(minWobbleSpeed, maxWobbleSpeed);
            wobbleSpeedChangeTime = Random.Range(minWobbleSpeedChangeTime, maxWobbleSpeedChangeTime);
        }
        if (offsetSpeedTime <= 0)
        {
            newTextureOffsetSpeed = Random.Range(minTextureOffsetSpeed, maxTextureOffsetSpeed);
            offsetSpeedTime = Random.Range(minOffsetSpeedTime, maxOffsetSpeedTime);
        }
        wobbleSpeedChangeTime -= Time.deltaTime;
        offsetSpeedTime -= Time.deltaTime;

        wobbleSpeed = Mathf.Lerp(wobbleSpeed, newWobbleSpeed, lerpSpeedW * Time.deltaTime);
        textureOffsetSpeed = Mathf.Lerp(textureOffsetSpeed, newTextureOffsetSpeed, lerpSpeedO * Time.deltaTime);

        wobbleSpeed = Mathf.Clamp(wobbleSpeed, minWobbleSpeed, maxWobbleSpeed);
        textureOffsetSpeed = Mathf.Clamp(textureOffsetSpeed, minTextureOffsetSpeed, maxTextureOffsetSpeed);
    }
}
