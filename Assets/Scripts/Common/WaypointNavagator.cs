using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavagator : MonoBehaviour
{
    public Waypoint waypoint { get; set; }

    private void Start()
    {
        GameObject go = Waypoint.GetNearestGameObjectWithTag(transform.position, "Waypoint");
        waypoint = go.GetComponent<Waypoint>();
    }
}
