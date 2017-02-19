using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CustomAudioClip
{
    public float volume;
    public AudioClip clip;

}
public class Obstacle : MonoBehaviour
{
    public CustomAudioClip[] firstImpactClips;
    public CustomAudioClip[] laterImpactClips;
    public float maxSpeed;
    public float minSpeed;
    float speed;
    public AudioSource source;
    float maxTimeToLimp;
    float minTimeToLimp;
    public GameObject particleSystem;
    public bool explodeWithParticles;

    bool isMoving;

    bool ableToDestroyFloe;

    Rigidbody body;

    public bool explode;

    float explosionTorque = 5f;

    public float explosionForce = 1000;
    public GameObject[] explosionObjs;
    public float bounceForce;

    GameObject[] targets;

    void OnEnable()
    {
        Debug.Log("I AM ALIVE!");
        targets = GameObject.FindGameObjectsWithTag("Target");
        speed = Random.Range(minSpeed, maxSpeed);
        ableToDestroyFloe = true;
        isMoving = true;
        body = GetComponent<Rigidbody>();
        BearScript.PlayerRevived += OnPlayerRevived;
        StartCoroutine(fetchTarget());
    }

    void OnPlayerRevived()
    {
        if (explode)
        {
            StartCoroutine(DestroyAfterTime());
        }
        else
        {
            this.Recycle();
        }
    }

    IEnumerator fetchTarget()
    {
        yield return null;
        int selector = Random.Range(0, targets.Length);
        Transform target = targets[selector].transform;
        transform.LookAt(target);
    }

    void Update()
    {
        if (isMoving)
        {
            transform.Translate(transform.forward * Time.deltaTime * speed, Space.World);
        }
        if (transform.position.y <= -9)
        {

            if (!explode)
            {
                this.Recycle();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!ableToDestroyFloe)
        {
            return;
        }
        if (other.tag == "Ground")
        {
            other.GetComponentInParent<FloeScript>().recentlyCollided = other.transform.name;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isMoving)
        {
            if (!source.isPlaying)
            {
                CustomAudioClip cp = firstImpactClips[Random.Range(0, firstImpactClips.Length)];
                source.clip = cp.clip;
                source.volume = cp.volume;
                source.Play();
            }
        }
        else
        {
            if (!source.isPlaying)
            {
                CustomAudioClip cp = laterImpactClips[Random.Range(0, firstImpactClips.Length)];
                source.clip = cp.clip;
                source.volume = cp.volume;
                source.Play();
            }
        }
        if (explode)
        {
            foreach (var item in explosionObjs)
            {
                item.transform.SetParent(null);
                Rigidbody rb = item.gameObject.AddComponent<Rigidbody>();
                rb = item.gameObject.GetComponent<Rigidbody>();
                rb.AddForce(new Vector3(Random.Range(-.7f, .7f), Random.Range(.8f, 1), Random.Range(-.7f, .7f))
                        .normalized * explosionForce, ForceMode.Impulse);
                rb.AddTorque(new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1)).normalized * explosionTorque, ForceMode.Impulse);
                item.GetComponent<Collider>().enabled = false;
                StartCoroutine(DestroyAfterTime());
            }
        }
        else if (explodeWithParticles)
        {
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            StartCoroutine(DestroyAfterTime());
            particleSystem.GetComponent<ParticleSystem>().Play();
        }
        GoLimp(collision);

        //Debug.DrawRay(transform.position, forceVector,Color.green,10f);

    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(5f);
        foreach (var item in explosionObjs)
        {
            Destroy(item.gameObject);
        }
        Destroy(gameObject);
    }

    void GoLimp(Collision collision)
    {
        isMoving = false;
        body.useGravity = true;
        Vector3 forceVector = (transform.position - collision.contacts[0].point).normalized;
        body.AddForce(forceVector * bounceForce, ForceMode.Impulse);
        //StartCoroutine (DisableFloeDestruction ());
    }

    void OnDisable()
    {
        BearScript.PlayerRevived -= OnPlayerRevived;
    }

    //IEnumerator DisableFloeDestruction ()
    //{
    //	yield return new WaitForFixedUpdate ();
    //	ableToDestroyFloe = false;
    //}
}
