using NavKeypad;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class HomeRiddle : MonoBehaviour
{
    public GameObject BibelBright, BibelDark, Laptop, Clock, NachtTischSchloss;
    public ClockRiddle ClockRiddle;
    public Laptop LaptopScript;
    public WaschmaschinenRätsel WaschmaschinenScript;
    public Keypad KeypadScript;
    public Inventory Inventory;
    public Animator NachtTischController;
    public IsLookingAt LA;
    [SerializeField] GameObject Enemy;

    // Update is called once per frame
    void Update()
    {
        if (LA.LookingAt() != null && LA.LookingAt().name == "BibleBright") {
            if (Input.GetKey(InputManager.Instance.Interact))
            {
                Bright();
                Riddle();
            }
        }else if(LA.LookingAt() != null && LA.LookingAt().name == "BibleDark"){
            if (Input.GetKey(InputManager.Instance.Interact))
            {
                Dark();
                Riddle();
            }
        }

        if (LA.LookingAt() != null && LA.LookingAt().name == "PadlockKomode" && Inventory.GegenstandImInventarUndAusgerüstet("Goldener Schlüssel")) {
            if (Input.GetKey(InputManager.Instance.Interact)) {
                NachtTischSchloss.SetActive(false);
                NachtTischController.SetBool("isOpen_Obj_1",true);
            }
        }

        IsOnSomething();
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

    public bool IsOnSomething() {
        if (ClockRiddle.IsOnClock == true || LaptopScript.IsOnLaptop == true || WaschmaschinenScript.isOnWaschmaschine == true || KeypadScript.onKeyPad == true) {
            return true;
        } else return false;
    }

    public GameObject IsOnWhat() { //Schaut am welchen obj der Spieler gerade ist
        if (ClockRiddle.IsOnClock == true) {
            return ClockRiddle.GameObject();
        } else if (LaptopScript.IsOnLaptop == true) {
            return LaptopScript.GameObject();
        } else if (WaschmaschinenScript.isOnWaschmaschine == true) {
            return WaschmaschinenScript.GameObject();
        } else if (KeypadScript.onKeyPad == true) {
            return KeypadScript.GameObject();
        } else return null;
    }
}
