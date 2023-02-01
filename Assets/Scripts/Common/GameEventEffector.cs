using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionEvent))]
public class GameEventEffector : Interactable
{
    [SerializeField] EventRouter gameEvent;
    [SerializeField] bool oneTime = true;

    private void Start()
    {
        GetComponent<CollisionEvent>().onEnter = OnInteract;
        if (!oneTime) GetComponent<CollisionEvent>().onStay = OnInteract;
    }

    public override void OnInteract(GameObject target)
    {
        gameEvent?.Notify();
        if (interactFX != null) Instantiate(interactFX, transform.position, Quaternion.identity);
        if (destroyOnInteract) Destroy(gameObject);
    }
}