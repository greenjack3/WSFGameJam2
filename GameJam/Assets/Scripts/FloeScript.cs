using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class FloeScript : MonoBehaviour
{
    public GameObject floeModelPrefab;

    public string recentlyCollided
    {
        get;
        set;
    }

    GameObject currentModel;
	public GameObject nextModel{ get; private set;}
    public float startChanceToDestroy = 0f;
    public float maxChanceToDestroy = 100f;
    public float maxChanceIncrease;
    public float minChanceIncrease;
    public float maxIncreaseTime;
    public float minIncreaseTime;
    public float cutChance = 0.3f;
    float chanceToDestroy;
    float increaseTime;

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
	public float newFloeAppearanceSpeed = 3f;
    float timeToForce;
    float timeToTorque;

    Rigidbody body;
	public CustomAudioClip[] clips;
	public AudioSource source;
    public float returnDamping = 5;

    void Start()
    {
		
        body = GetComponent<Rigidbody>();
        upForceTimeLeft = upForceTime;
		StartCoroutine("GetNewFloe");
        timeToForce = Random.Range(minTime, maxTime);
        timeToTorque = Random.Range(minTorqueTime, maxTorqueTime);
        currentModel = Instantiate(floeModelPrefab, transform) as GameObject;
        StartCoroutine(CheckForDestruction());

        BearScript.PlayerRevived += OnPlayerRevived;

        chanceToDestroy = startChanceToDestroy;
        increaseTime = Random.Range(minIncreaseTime, maxIncreaseTime);
        StartCoroutine(DestroyChanceIncrease());
    }

    IEnumerator DestroyChanceIncrease()
    {
        while (true)
        {
            increaseTime -= Time.deltaTime;
            if (increaseTime <= 0 && chanceToDestroy <= maxChanceToDestroy)
            {
                chanceToDestroy += Random.Range(minChanceIncrease, maxChanceIncrease);
                increaseTime = Random.Range(minIncreaseTime, maxIncreaseTime);
            }
            if (chanceToDestroy > maxChanceToDestroy)
            {
                chanceToDestroy = maxChanceToDestroy;
            }
            yield return null;
        }
    }

    IEnumerator CheckForDestruction()
    {
        recentlyCollided = "";
        List<Transform> leftMostObj = new List<Transform>();
        List<Transform> rightMostObj = new List<Transform>();
        foreach (var item in currentModel.GetComponentsInChildren<Transform>())
        {
            if (item == transform)
            {
                continue;
            }
            if (item.name.Contains("L0"))
            {
                leftMostObj.Add(item);
            }
            if (item.name.Contains("R0"))
            {
                rightMostObj.Add(item);
            }
        }

        leftMostObj.Sort(new System.Comparison<Transform>((x, y) => string.Compare(x.name, y.name)));
        rightMostObj.Sort(new System.Comparison<Transform>((x, y) => string.Compare(x.name, y.name)));

        while (true)
        {
            float rngeezus = 0;
            if (recentlyCollided.Contains("L0") && leftMostObj.Count > 0)
            {
                if (recentlyCollided == leftMostObj.Last().name)
                {
                    rngeezus = Random.Range(0, 100);

                    if (rngeezus <= chanceToDestroy)
                    {
                        GameObject obj = leftMostObj.Last().gameObject;
                        obj.transform.SetParent(null);
                        obj.AddComponent<Rigidbody>();
                        obj.GetComponent<Collider>().isTrigger = false;
                        leftMostObj.Remove(obj.transform);
						CustomAudioClip cp = clips [Random.Range (0, clips.Length)];
						source.clip = cp.clip;
						source.volume = cp.volume;
						source.Play ();
                        chanceToDestroy = chanceToDestroy * cutChance;
                    }
                }
            }
            if (recentlyCollided.Contains("R0") && rightMostObj.Count > 0)
            {
                if (recentlyCollided == rightMostObj.Last().name)
                {
                    rngeezus = Random.Range(0, 100);

                    if (rngeezus <= chanceToDestroy)
                    {
                        GameObject obj = rightMostObj.Last().gameObject;
                        obj.AddComponent<Rigidbody>();
                        obj.transform.SetParent(null);
                        obj.GetComponent<Collider>().isTrigger = false;
                        rightMostObj.Remove(obj.transform);
						CustomAudioClip cp = clips [Random.Range (0, clips.Length)];
						source.clip = cp.clip;
						source.volume = cp.volume;
						source.Play ();
                        chanceToDestroy = chanceToDestroy * cutChance;
                    }
                }
            }
            yield return null;
        }
    }

	void ClearNextModel()
	{
		Destroy (nextModel);
		nextModel = null;
	}

    void OnPlayerRevived ()
    {
		
        StopAllCoroutines();
        Destroy(currentModel);
		ClearNextModel ();
        currentModel = Instantiate(floeModelPrefab, transform) as GameObject;
        currentModel.transform.localPosition = Vector3.zero;
        StartCoroutine(CheckForDestruction());
		StartCoroutine("GetNewFloe");
		transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

	public void StopGettingNewFloe ()
	{
		StopAllCoroutines ();
	}

	public void PlatformCenter ()
	{
		StartCoroutine (CenterPlatform());
	}

	IEnumerator GetNewFloe()
	{
		nextModel  = Instantiate (floeModelPrefab, transform) as GameObject;
		nextModel.transform.localPosition = new Vector3 (-14f, -0.45f, -3.84f);
		nextModel.transform.localRotation = Quaternion.identity;
		while (nextModel.transform.localPosition.x <=18) {
//			Debug.Log ("New Floe");
			nextModel.transform.Translate (Vector3.right * newFloeAppearanceSpeed * Time.deltaTime);
			yield return null;
		}

			ClearNextModel ();
			StartCoroutine ("GetNewFloe");

	}

	public IEnumerator MoveNextFloeUp ()
	{
		Debug.Log ("Moving Floe Up");

		Destroy (currentModel);
		while (nextModel.transform.localPosition.y <0 && nextModel.transform.localPosition.z <0) {
			Debug.Log ("moving up");	
			nextModel.transform.localPosition = Vector3.MoveTowards(nextModel.transform.localPosition,
				new Vector3(nextModel.transform.localPosition.x,0,0),newFloeAppearanceSpeed*Time.deltaTime);
			yield return null;
		}

	}

	public IEnumerator CenterPlatform()
	{
		currentModel = nextModel;
		nextModel = null;
		while (Mathf.Abs(currentModel.transform.localPosition.x) >= 0.2f) {
			Debug.Log ("New Floe");
			currentModel.transform.localPosition = Vector3.MoveTowards(currentModel.transform.localPosition,
				Vector3.zero,newFloeAppearanceSpeed*Time.deltaTime);
			yield return null;
		}
		StartCoroutine ("GetNewFloe");

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

        if (timeToTorque <= 0)
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
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * returnDamping);
        }
        else
        {
            timeToTorque -= Time.deltaTime;
        }

        upForceTimeLeft -= Time.deltaTime;
        timeToForce -= Time.deltaTime;

    }

	void OnDisable()
	{
		BearScript.PlayerRevived -= OnPlayerRevived;
	}
}
