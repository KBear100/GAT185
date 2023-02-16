using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoRaycast : Ammo
{
	private void Start()
	{
		// raycast, check for game object hit
		Ray ray = new Ray(transform.position, transform.forward);
		if (Physics.Raycast(ray, out RaycastHit raycastHit, ammoData.distance, ammoData.hitMask))
		{
			OnDamage(raycastHit.collider.gameObject);
		}

		Destroy(gameObject, ammoData.lifetime);
	}
}
