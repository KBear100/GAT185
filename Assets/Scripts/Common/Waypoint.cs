using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Waypoint[] waypoints;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<WaypointNavagator>(out WaypointNavagator waypointNavigator))
        {
            // if current navigator waypoint is this waypoint, set new random waypoint
            if (waypointNavigator.waypoint == this)
            {
                waypointNavigator.waypoint = waypoints[Random.Range(0, waypoints.Length)];
            }
        }
    }

    public static GameObject GetNearestGameObjectWithTag(Vector3 position, string tag)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
        gameObjects = gameObjects.OrderBy(go => (go.transform.position - position).sqrMagnitude).ToArray();

        return (gameObjects.Length > 0) ? gameObjects[0] : null;
    }
}
