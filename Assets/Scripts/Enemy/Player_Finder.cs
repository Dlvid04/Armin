using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Player_Finder : MonoBehaviour
{
    public CharacterController Controller;
    public NavMeshAgent EnemyNav;
    public Transform Player, EnemyTransform;
    public Animator EnemyAnimator;
    public PlayableDirector PlayableDirector;
    public float See_Range = 4f;
    public string Scene = "Bright_Start";

    void Start(){
        EnemyTransform.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        Entrance();
        if (PlayableDirector.state != PlayState.Playing) {
            EnemyNav.SetDestination(Player.position);
            Door_Open_Close();
            if (Check_If_In_Range()) {
                LoadNextScene();
            }
        }
    }

    public void EnemySpawn() {
        EnemyTransform.gameObject.SetActive(true);
    }

    public void Türschließen() {
        GameObject.Find("Door_Home_Entrance").transform.GetChild(1).GetComponentInParent<DoorController>().SendMessage("ToggleDoor");
    }

    public void Door_Open_Close() {
        Ray ray = new Ray(EnemyNav.transform.position, EnemyNav.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, See_Range))
        {
            if (hit.collider.gameObject.name == "Rotator")
            {
                if (!hit.collider.gameObject.GetComponentInParent<DoorController>().isOpen && hit.collider.gameObject.name == "Rotator") {
                    hit.collider.gameObject.GetComponentInParent<DoorController>().SendMessage("ToggleDoor"); // Hier greift der Gegner auf T�ren zu !!! Zu Oft !!!
                }
                if (hit.collider.gameObject.transform.parent.name == "Door_Home_Entrance")
                {
                    Invoke("Türschließen", 1f);
                }
            }
        }
    }

    public bool Check_If_In_Range() {
        if (Vector3.Distance(EnemyNav.transform.position, Player.position) <= 2.3f) {
            EnemyAnimator.SetBool("IsWalking", false);
            EnemyAnimator.SetBool("HoldingPlayer", true);
            return true;
        }else { 
            return false; 
        }
    }

    public void Entrance() {
        if (PlayableDirector.state != PlayState.Playing) {
            EnemyAnimator.SetBool("IsWalking", true);
        }
    }

    public void LoadNextScene() {
        SceneManager.LoadScene(Scene);
        if (SceneManager.GetSceneByName(Scene).isLoaded) {
            SceneManager.UnloadSceneAsync("Armin");
        }
    }
}
