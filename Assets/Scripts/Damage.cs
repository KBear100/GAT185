using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionEvent))]
public class Damage : Interactable
{
    [SerializeField] float damage = 0;
    [SerializeField] bool oneTime = true;

    void Start()
    {
        GetComponent<CollisionEvent>().onEnter += OnInteract;
        if (!oneTime) GetComponent<CollisionEvent>().onStay += OnInteract;
    }

    public override void OnInteract(GameObject target)
    {
        if (target.TryGetComponent<Health>(out Health health))
        {
            health.OnApplyDamage(damage * ((oneTime) ? 1 : Time.deltaTime));
        }
    }
}
