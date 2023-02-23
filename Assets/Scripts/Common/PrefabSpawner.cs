using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : Spawner
{
	[SerializeField] EventRouter startEvent;
	[SerializeField] private Transform spawnersParent;
	[SerializeField] private Transform spawnedParent;

	private List<GameObject> spawners = new List<GameObject>();

	private void OnEnable()
	{
		// add spawn events
		if (startEvent != null) startEvent.onEvent += StartSpawn;
	}

	private void OnDisable()
	{
		// remove spawn events
		if (startEvent != null) startEvent.onEvent -= StartSpawn;
	}


	private new void Start()
	{
		// call parent start
		base.Start();

		// get all children game object of spawners parent and add to spawners list
		foreach (Transform child in spawnersParent)
		{
			spawners.Add(child.gameObject);			
		}
		// set all spawners inactive (hide)
		foreach (GameObject spawner in spawners)
		{
			spawner.SetActive(false);
		}
	}

	public override void Spawn()
	{
		// find open prefab (no objects at prefab location)
		GameObject prefab = GetRandomOpenSpawnPrefab();

		// create spawn game object, set spawner as parent
		GameObject spawn = Instantiate(prefab, spawnedParent);
		spawn.SetActive(true);
	}

	public override void Clear()
	{
		// go through all children of spawned parents and destroy children game objects
		foreach (Transform child in spawnedParent)
		{
			Destroy(child.gameObject);
		}
	}

	public void StartSpawn()
	{
		// clear all spawned
		Clear();
		// spawn all spawner objects
		foreach (var prefab in spawners)
		{
			// spawn game object under spawned parent
			GameObject spawn = Instantiate(prefab.gameObject, spawnedParent);
			spawn.SetActive(true);
		}
	}


	private bool IsGameObjectOpen(GameObject go)
	{
		// check if there are any colliders at game object location
		return Physics.CheckSphere(go.transform.position, 0.2f);
	}

	private GameObject GetRandomOpenSpawnPrefab()
	{
		GameObject openPrefab = null;
		int attempts = 0;
		// look for open prefab (no objects colliding at location)
		// avoid infinite loop (no open prefabs) using attempts
		while (openPrefab == null && attempts++ < spawners.Count * 2)
		{
			// get random prefab
			GameObject prefab = spawners[Random.Range(0, spawners.Count)];
			// if prefab is open set open prefab
			if (IsGameObjectOpen(prefab)) 
			{
				openPrefab = prefab;
			}

		}

		return openPrefab;
	}
}
