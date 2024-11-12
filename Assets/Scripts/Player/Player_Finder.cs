using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player_Finder : MonoBehaviour
{
    public CharacterController Controller;
    public NavMeshAgent Enemy;
    public Transform Player;

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

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        Controller.Move(velocity * Time.deltaTime);

        Enemy.SetDestination(Player.position);

        if (Enemy.remainingDistance == 1f) {
            //Enemy.isStopped = true;
        }
    }
}
