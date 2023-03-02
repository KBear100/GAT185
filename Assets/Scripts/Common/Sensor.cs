using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    [SerializeField] private Transform origin;
    [SerializeField] private string targetTag;
    [SerializeField] private float distance;
    [SerializeField] private float senseRate;

    //game object that has been sensed
    public GameObject sensed { get; private set; } = null;

    private void Start()
    {
        StartCoroutine(SenseCoroutine());
    }

    IEnumerator SenseCoroutine()
    {
        while(true)
        {
            Sense();
            yield return new WaitForSeconds(senseRate);
        }
    }

    void Sense()
    {
        sensed = null;

        Ray ray = new Ray(origin.position, origin.forward);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, distance))
        {
            if(raycastHit.collider.CompareTag(targetTag))
            {
                sensed = raycastHit.collider.gameObject;
            }
        }
    }
}
