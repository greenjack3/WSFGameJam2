using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storm : MonoBehaviour
{

    public float maxTextureOffsetSpeed;
    public float minTextureOffsetSpeed;
    float textureOffsetSpeed;
    float newTextureOffsetSpeed;
    public float lerpSpeedO;
    public float maxOffsetSpeedTime;
    public float minOffsetSpeedTime;
    float offsetSpeedTime;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        textureOffsetSpeed = Random.Range(minTextureOffsetSpeed, maxTextureOffsetSpeed);
        offsetSpeedTime = Random.Range(minOffsetSpeedTime, maxOffsetSpeedTime);
    }

    void Update()
    {
        float offset = Time.time * textureOffsetSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(-offset, 0));

        if (offsetSpeedTime <= 0)
        {
            newTextureOffsetSpeed = Random.Range(minTextureOffsetSpeed, maxTextureOffsetSpeed);
            offsetSpeedTime = Random.Range(minOffsetSpeedTime, maxOffsetSpeedTime);
        }
        offsetSpeedTime -= Time.deltaTime;

        textureOffsetSpeed = Mathf.Lerp(textureOffsetSpeed, newTextureOffsetSpeed, lerpSpeedO * Time.deltaTime);
        textureOffsetSpeed = Mathf.Clamp(textureOffsetSpeed, minTextureOffsetSpeed, maxTextureOffsetSpeed);
    }
}
