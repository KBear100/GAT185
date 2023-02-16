using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
	[SerializeField] protected AmmoData ammoData;

	public void OnDamage(GameObject target)
	{
		// apply damage if game object has health
		if (target.TryGetComponent<Health>(out Health health))
		{
			health.OnApplyDamage(ammoData.damage * ((ammoData.damageOverTime) ? Time.deltaTime : 1));
		}

		// create impact prefab
		if (ammoData.impactPrefab != null)
		{
			Instantiate(ammoData.impactPrefab, target.transform.position, target.transform.rotation);
		}

		// destroy game object
		if (ammoData.destroyOnImpact)
		{
			Destroy(gameObject);
		}
	}
}
