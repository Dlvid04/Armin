using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public CharacterController Controller;
    public Animator Animator;

    public float Speed = 3f;
    public float gravity = -9.8f;

    public Transform GroundCheck;
    public float GroundDistance = 0.4f;
    public LayerMask GroundMask;

    Vector3 velocity;
    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);

        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        Controller.Move(velocity * Time.deltaTime);


        float X = Input.GetAxis("Horizontal");
        float Z = Input.GetAxis("Vertical");

        Vector3 Move = transform.right * X + transform.forward * Z;

        Controller.Move(Move * Speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Speed = 4.5f;
            Animator.SetBool("IsRunning", true);
            Animator.SetBool("IsStanding", false);
            Animator.SetBool("IsWalking", false);
        }
        else if (!Input.GetKey(KeyCode.LeftShift) && Input.GetKey(InputManager.Instance.moveForward) || Input.GetKey(InputManager.Instance.moveLeft) || Input.GetKey(InputManager.Instance.moveBackward) || Input.GetKey(InputManager.Instance.moveRight))
        {
            Speed = 3f;
            Animator.SetBool("IsRunning",false);
            Animator.SetBool("IsStanding",false);
            Animator.SetBool("IsWalking", true);
        }
        else {
            Animator.SetBool("IsRunning", false);
            Animator.SetBool("IsStanding", true);
            Animator.SetBool("IsWalking", false);
        }
        
    }
}
