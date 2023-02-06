using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionEvent))]
public class AnimationEffector : Interactable
{
	[SerializeField] Animator animator;
	[SerializeField] string parameter;
	
	private void Start()
	{
		GetComponent<CollisionEvent>().onEnter += OnInteract;
		GetComponent<CollisionEvent>().onExit += OnInteract;
	}

	public override void OnInteract(GameObject target)
	{
		animator.SetTrigger(parameter);

		if (interactFX != null) Instantiate(interactFX, transform.position, Quaternion.identity);
		if (destroyOnInteract) Destroy(gameObject);
	}
}

