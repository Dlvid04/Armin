using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public CharacterController Controller;
    public Animator Animator;

    public float Speed = 3f, gravity = -9.8f;


    public Transform GroundCheck;
    public float GroundDistance = 0.4f;
    public LayerMask GroundMask;

    Vector3 velocity;
    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        float X = 0;
        float Z = 0;

        isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);

        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        Controller.Move(velocity * Time.deltaTime);

        if (Input.GetKey(InputManager.Instance.MoveForward)) {
            Z = 1f;
        }
        if (Input.GetKey(InputManager.Instance.MoveBackward)) {
            Z = -1f;
        }
        if (Input.GetKey(InputManager.Instance.MoveLeft)) {
            X = -1f;
        }
        if (Input.GetKey(InputManager.Instance.MoveRight)) {
            X = 1f;
        }

        Vector3 Move = transform.right * X + transform.forward * Z;

        Controller.Move(Move * Speed * Time.deltaTime);

        if (Input.GetKey(InputManager.Instance.Sprinten))
        {
            Speed = 4.5f;
            Animator.SetBool("IsRunning", true);
            Animator.SetBool("IsStanding", false);
            Animator.SetBool("IsWalking", false);
        }
        else if (!Input.GetKey(InputManager.Instance.Sprinten) && Input.GetKey(InputManager.Instance.MoveForward) || Input.GetKey(InputManager.Instance.MoveLeft) || Input.GetKey(InputManager.Instance.MoveBackward) || Input.GetKey(InputManager.Instance.MoveRight))
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
