using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collidable
{
    [SerializeField] GameObject pickupFX;

    void Start()
    {
        onEnter += OnCoinPickup;
    }

    void Update()
    {
        
    }

    void OnCoinPickup(GameObject go)
    {
        if(TryGetComponent<RollerPlayer>(out RollerPlayer player))
        {
            player.AddPoints(100);
        }

        Instantiate(pickupFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
