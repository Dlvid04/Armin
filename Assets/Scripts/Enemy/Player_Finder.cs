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
    public NavMeshAgent Enemy;
    public Transform Player;

    public float gravity = -9.8f;

    public Transform GroundCheck;
    public float GroundDistance = 0.4f;
    public LayerMask GroundMask;
    public float See_Range = 4f;

    Vector3 velocity;
    bool isGrounded;

    public string Scene;

    void Start(){
        Enemy.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        Door_Open_Close();
        Falling();
        Enemy.SetDestination(Player.position);
        Check_If_In_Range();
    }

    public void CallMethod() {
        GameObject.Find("Door_Home_Entrance").transform.GetChild(0).GetChild(1).GetComponentInParent<DoorController>().SendMessage("ToggleDoor");
    }

    public void Door_Open_Close() {
        Ray ray = new Ray(Enemy.transform.position, Enemy.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, See_Range))
        {
            if (hit.collider.gameObject.CompareTag("Door"))
            {
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
        if (Vector3.Distance(Enemy.transform.position, Player.position) <= 2.3f) {
            Debug.Log(Vector3.Distance(Enemy.transform.position, Player.position));
            SceneManager.LoadScene(Scene);
            if (SceneManager.GetSceneByName(Scene).isLoaded) {
                SceneManager.UnloadSceneAsync("Armin");
            }
        }
    }
}
