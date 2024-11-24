using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Player_Finder : MonoBehaviour
{
    public CharacterController Controller;
    public NavMeshAgent EnemyNav;
    public Transform Player;
    public Transform EnemyTransform;
    public Animator EnemyAnimator;

    public float gravity = -9.8f;

    public Transform GroundCheck;
    public float GroundDistance = 0.4f;
    public LayerMask GroundMask;
    public float See_Range = 4f;

    Vector3 velocity;
    bool isGrounded;

    public string Scene;

    bool IsMoving = false;

    void Start(){
        EnemyNav.gameObject.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
            Door_Open_Close();
            Falling();
            EnemyNav.SetDestination(Player.position);
            Check_If_In_Range();
            if (!IsMoving)
            {
                Check_If_Moving(EnemyTransform.position);
            }
    }

    public void CallMethod() {
        GameObject.Find("Door_Home_Entrance").transform.GetChild(0).GetChild(1).GetComponentInParent<DoorController>().SendMessage("ToggleDoor");
    }

    public void Door_Open_Close() {
        Ray ray = new Ray(EnemyNav.transform.position, EnemyNav.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, See_Range))
        {
            if (hit.collider.gameObject.CompareTag("Door"))
            {
                Debug.Log(hit.collider.gameObject.name);
                if (!hit.collider.gameObject.GetComponentInParent<DoorController>().isOpen && hit.collider.gameObject.name == "Rotator") {
                    hit.collider.gameObject.GetComponentInParent<DoorController>().SendMessage("ToggleDoor"); // Hier greift der Gegner auf T�ren zu !!! Zu Oft !!!
                }
                if (hit.collider.gameObject.transform.parent.parent.name == "Door_Home_Entrance")
                {
                    Invoke("CallMethod", 1f);
                }
            }
        }
    }

    public void Falling() {
        isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        Controller.Move(velocity * Time.deltaTime);
    }

    public void Check_If_In_Range() {
        if (Vector3.Distance(EnemyNav.transform.position, Player.position) <= 2.3f) {
            SceneManager.LoadScene(Scene);
            if (SceneManager.GetSceneByName(Scene).isLoaded) {
                SceneManager.UnloadSceneAsync("Armin");
            }
        }
    }

    public void Check_If_Moving(Vector3 LastPosition) {
        float distanceMoved = Vector3.Distance(transform.position, LastPosition);

        if (distanceMoved > 0.01f)
        {
            EnemyAnimator.SetBool("IsWalking", true);
            EnemyAnimator.SetBool("IsStanding", false);
            IsMoving = true;
        }else
            EnemyAnimator.SetBool("IsWalking", false);
            EnemyAnimator.SetBool("IsStanding", true);
            IsMoving = false;
    }
}
