using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharaterPlayer : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float hitForce = 2;
    [SerializeField] private float gravity = Physics.gravity.y;
    [SerializeField] private float turnRate = 10;
    [SerializeField] private float jumpHeight = 2;

    CharacterController characterController;
    Camera mainCamera;
    Vector3 velocity = Vector3.zero;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        Vector3 direction = Vector3.zero;
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");

        direction = mainCamera.transform.TransformDirection(direction);

        if(characterController.isGrounded)
        {
            velocity.x = direction.x * speed;
            velocity.z = direction.z * speed;
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -3 * gravity);
            }
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        characterController.Move(velocity * Time.deltaTime);
        Vector3 look = direction;
        look.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(look), turnRate * Time.deltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
        {
            return;
        }

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.velocity = pushDir * hitForce;
    }
}
