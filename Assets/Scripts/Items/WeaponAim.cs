using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAim : MonoBehaviour
{
	[SerializeField] LayerMask hitMask;
	private Camera mainCamera;
	
	void Start()
	{
		mainCamera = Camera.main;
	}

	void Update()
	{
		Ray ray = mainCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
		if (Physics.Raycast(ray, out RaycastHit raycastHit, 100.0f, hitMask))
		{
			transform.position = raycastHit.point;
		}
	}
}
