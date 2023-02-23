using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRadiusSpawner : Spawner
{
	[SerializeField][Range(1, 1000)] private float radius = 100;
	[SerializeField] private Transform spawnLocation = null;
	[SerializeField] private GameObject[] prefabs;

	public override void Spawn()
	{
		// set spawn position around spawn location transform (player) at circle radius (distance)
		Vector3 position = spawnLocation.position + Quaternion.AngleAxis(Random.value * 360.0f, Vector3.up) * (Vector3.forward * radius);
		// create spawn object from radom spawn prefab, spawner is parent object
		Instantiate(prefabs[Random.Range(0, prefabs.Length)], position, Quaternion.identity, transform);
	}

	public override void Clear()
	{
		// get all children game objects of spawner
		var spawned = GetComponentsInChildren<Transform>();
		// iterate through all spawned transforms
		foreach (var spawn in spawned)
		{
			// destroy child game object
			Destroy(spawn.gameObject);
		}
	}
}
