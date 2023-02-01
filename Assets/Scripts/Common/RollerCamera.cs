using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField, Range(2, 20)] private float distance;
    [SerializeField, Range(20, 80)] private float pitch;
    [SerializeField, Range(0.1f, 5)] private float sensitivity;

    private float yaw = 0;

    void LateUpdate()
    {
        if(target == null)return;

        yaw += Input.GetAxis("Mouse X") * sensitivity;

        Quaternion qyaw = Quaternion.AngleAxis(yaw, Vector3.up);
        Quaternion qpitch = Quaternion.AngleAxis(pitch, Vector3.right);
        Quaternion rotation = qyaw * qpitch;

        Vector3 offset = rotation * Vector3.back * distance;

        transform.position = target.position + offset;
        transform.rotation = Quaternion.LookRotation(-offset);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
        yaw = target.rotation.eulerAngles.y;
    }
}
