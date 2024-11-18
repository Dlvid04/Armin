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
    public float rayLength = 4f;

    Vector3 velocity;
    bool isGrounded;

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

        if (Enemy.remainingDistance == 1f)
        {
            //Enemy.isStopped = true;
        }

        Ray ray = new Ray(Enemy.transform.position, Enemy.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayLength))
        {
            if (hit.collider.gameObject.CompareTag("Door"))
            {
                hit.collider.gameObject.GetComponentInParent<DoorController>().SendMessage("ToggleDoor");   // Hier greift der Gegner auf Türen zu !!! Zu Oft !!!
                if (hit.collider.gameObject.transform.parent.parent.name == "Door_Home_Entrance")
                {
                    Invoke("CallMethod", 1f);
                }
            }
        }
    }

    public void CallMethod() {
        GameObject.Find("Door_Home_Entrance").transform.GetChild(0).GetChild(1).GetComponentInParent<DoorController>().SendMessage("ToggleDoor");
    }
}
