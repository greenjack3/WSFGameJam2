using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {
	[SerializeField]
	Text text;
	// Use this for initialization
	void Start () {
		BearScript.PlayerFallDown += () => StopAllCoroutines ();
		BearScript.PlayerRevived += () => StartCoroutine(CountScore());
		StartCoroutine(CountScore());
	}

	IEnumerator CountScore ()
	{
		float timer = 0;
		while (true) {
			yield return null;
			timer += Time.deltaTime;
			text.text = timer.ToString ("000");
		}
	}
}
