using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Home_Bright_Riddle : MonoBehaviour
{
    public GameObject BibelBright, BibelDark, Laptop, Clock, NachtTischSchloss;
    public ClockRiddle ClockRiddle;
    public Inventory Inventory;
    public Animator NachtTischController;
    public IsLookingAt LA;
    [SerializeField] GameObject Enemy;

    // Update is called once per frame
    void Update()
    {
        if (LA.LookingAt() != null && LA.LookingAt().name == "bibleBright") {
            if (Input.GetKeyDown("e"))
            {
                Bright();
                Riddle();
            }
        }else if(LA.LookingAt() != null && LA.LookingAt().name == "bibleDark"){
            if (Input.GetKeyDown("e"))
            {
                Dark();
                Riddle();
            }
        }

        if (LA.LookingAt() != null && LA.LookingAt().name == "PadlockKomode" && Inventory.GegenstandImInventarUndAusgerüstet("Goldener Schlüssel")) {
            if (Input.GetKeyDown("e")) {
                NachtTischSchloss.SetActive(false);
                NachtTischController.SetBool("isOpen_Obj_1",true);
            }
        }
    }


    public void Bright(){
        BibelBright.SetActive(false);
        BibelDark.SetActive(false);
        Enemy.GameObject().GetComponent<Player_Finder>().Scene = "Bright_Start";
    }

    public void Dark(){
        BibelBright.SetActive(false);
        BibelDark.SetActive(false);
        Enemy.GameObject().GetComponent<Player_Finder>().Scene = "Dark_Start";
    }

    public void Riddle(){
        Laptop.GetComponent<Laptop>().LaptopUI.SetActive(true);
        Laptop.tag = "Interactable";
        Clock.tag = "Interactable";
        ClockRiddle.enabled = true;
    }
}
