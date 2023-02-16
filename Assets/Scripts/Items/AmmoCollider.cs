using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionEvent))]
public class AmmoCollider : Ammo
{
	private void Start()
	{
		// set ammo rigidbody force
		if (ammoData.force != 0) GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * ammoData.force, ammoData.forceMode);

		// register collision event
		GetComponent<CollisionEvent>().onEnter += OnDamage;
		Destroy(gameObject, ammoData.lifetime);
	}
}
