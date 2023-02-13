using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game/PlayData")]
public class PlayerData : ScriptableObject
{
    public float speed = 5;
    public float hitForce = 2;
    public float gravity = Physics.gravity.y;
    public float turnRate = 10;
    public float jumpHeight = 2;

    
}
