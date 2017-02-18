using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager> {

	public List<Transform> spawners;
	public Obstacle[] obstaclePrefabs;
	public float timeBetweenSpawns = 5f;

	protected override void Awake ()
	{
		base.Awake ();
		foreach (var item in GetComponentsInChildren<Transform>()) {
			if (transform != item) {
				spawners.Add (item);
			}
		}
		BearScript.PlayerRevived += BearScript_PlayerRevived;
		StartCoroutine (ContinousSpawning ());
	}



	void BearScript_PlayerRevived ()
	{
		StopAllCoroutines ();
		StartCoroutine (ContinousSpawning ());
	}

	IEnumerator ContinousSpawning ()
	{
		float timer = 0;
		while (true) {
			timer += Time.deltaTime;
			if (timer >= timeBetweenSpawns) {
				timer = 0;
				Spawn ();
			}
			yield return null;
		}
	}
		
	void Spawn()
	{
		if (obstaclePrefabs.Length == 0 || spawners.Count == 0) {
			return;
		}
		Obstacle nextObstacle = obstaclePrefabs [Random.Range (0, obstaclePrefabs.Length)];
		nextObstacle.Spawn (spawners[Random.Range(0,spawners.Count)].position,Quaternion.identity);
	}
}
