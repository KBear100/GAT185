using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
	[SerializeField] WeaponData weaponData;
	[SerializeField] Animator animator;
	[SerializeField] Transform ammoTransform;

	private int ammoCount = 0;
	private bool weaponReady = false;

	private void Start()
	{
		if (ammoTransform == null) ammoTransform = transform;
	}

	public override ItemData GetData() {  return weaponData; }

	public override void Equip()
	{
		base.Equip();
		weaponReady = true;
	}

	public override void Unequip()
	{
		base.Unequip();
	}

	public override void Use()
	{
		if (!weaponReady) return;

		Debug.Log("Fire");
		// trigger weapon animation if trigger name set and animator exists
		// ammo will be created through animation event
		if (weaponData.animTriggerName != "" && animator != null) 
		{ 
			animator.SetTrigger(weaponData.animTriggerName);
			weaponReady = false;
		}
		else
		{
			// create ammo prefab
			Instantiate(weaponData.ammoPrefab, ammoTransform.position, ammoTransform.rotation);
			if (weaponData.usageType == UsageType.SINGLE || weaponData.usageType == UsageType.BURST) weaponReady = false;
			if (weaponData.fireRate > 0)
			{
				weaponReady = false;
				StartCoroutine(ResetFireTimer());
			}
		}
	}

	public override void StopUse()
	{
		if (weaponData.usageType == UsageType.SINGLE || weaponData.usageType == UsageType.BURST) weaponReady = true;
	}

	public override bool isReady()
	{
		// check if ammo exists or weapon does not have rounds
		return weaponReady && (ammoCount > 0 || weaponData.rounds == 0);
	}

	public override void OnAnimEventItemUse()
	{
		// create ammo prefab
		Instantiate(weaponData.ammoPrefab, ammoTransform.position, ammoTransform.rotation);
	}

	IEnumerator ResetFireTimer()
	{
		yield return new WaitForSeconds(weaponData.fireRate);
		weaponReady = true;
	}
}
