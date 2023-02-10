using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDestroy : MonoBehaviour
{
    [SerializeField] private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, time);
    }
}
