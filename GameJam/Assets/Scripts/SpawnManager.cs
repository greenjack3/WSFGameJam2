using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager> {

	public Transform[] spawners;
	public Obstacle[] obstaclePrefabs;
	void Awake()
	{
		spawners = GetComponentsInChildren<Transform> ();
	}

	void Spawn()
	{
		
	}
}
