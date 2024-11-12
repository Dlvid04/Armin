using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public CharacterController Controller;

    public float Speed = 3f;
    public float gravity = -9.8f;

    public Transform GroundCheck;
    public float GroundDistance = 0.4f;
    public LayerMask GroundMask;

    Vector3 velocity;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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

        if (Input.GetKey(KeyCode.LeftShift)) {
            Speed = 4.5f;
        }
        else {
            Speed = 3f;
        }
        
    }
}
