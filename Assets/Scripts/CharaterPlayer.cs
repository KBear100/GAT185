using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class CharaterPlayer : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private InputRouter inputRouter;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Weapon weapon;

    CharacterController characterController;
    Vector2 inputAxis;

    Camera mainCamera;
    Vector3 velocity = Vector3.zero;
    float inAirTime = 0;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;

        inputRouter.jumpEvent += OnJump;
        inputRouter.moveEvent += OnMove;
        inputRouter.fireEvent += OnFire;
        inputRouter.fireStopEvent += OnFireStop;
    }

    void Update()
    {
        Vector3 direction = Vector3.zero;

        direction.x = inputAxis.x;
        direction.y = inputAxis.y;

        direction = mainCamera.transform.TransformDirection(direction);

        if(characterController.isGrounded)
        {
            velocity.x = direction.x * playerData.speed;
            velocity.z = direction.z * playerData.speed;
            inAirTime = 0;
        }
        else
        {
            inAirTime += Time.deltaTime;
            velocity.y += playerData.gravity * Time.deltaTime;
        }

        characterController.Move(velocity * Time.deltaTime);
        Vector3 look = direction;
        look.y = 0;
        if(look.magnitude > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(look), playerData.turnRate * Time.deltaTime);
        }

        //set animator parameters
        animator.SetFloat("Speed", characterController.velocity.magnitude);
        animator.SetFloat("VelocityY", characterController.velocity.y);
        animator.SetFloat("InAirTime", inAirTime);
        animator.SetBool("isGrounded", characterController.isGrounded);
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
        body.velocity = pushDir * playerData.hitForce;
    }

    public void OnJump()
    {
        if(characterController.isGrounded)
        {
            animator.SetTrigger("Jump");
            velocity.y = Mathf.Sqrt(playerData.jumpHeight * -3 * playerData.gravity);
        }
    }

    public void OnFire()
    {
        weapon.Use();
    }

    public void OnFireStop()
    {
        weapon.StopUse();
    }


    public void OnMove(Vector2 axis)
    {
        inputAxis = axis;
    }

    public void OnAnimEventItemUse()
    {
        weapon.OnAnimEventItemUse();
    }

    public void OnLeftFootSpawn(GameObject go)
    {
        Transform bone = animator.GetBoneTransform(HumanBodyBones.LeftFoot);
        Instantiate(go, bone.position, bone.rotation);
    }
    public void OnRightFootSpawn(GameObject go)
    {
        Transform bone = animator.GetBoneTransform(HumanBodyBones.RightFoot);
        Instantiate(go, bone.position, bone.rotation);
    }
}
