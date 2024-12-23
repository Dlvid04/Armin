using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Home_Bright_Riddle : MonoBehaviour
{
    public GameObject Riddle1;
    public GameObject Riddle2;
    public GameObject Riddle3;
    public IsLookingAt LA;
    [SerializeField] GameObject Enemy;


    // Update is called once per frame
    void Update()
    {
        if (LA.LookingAt() != null &&  LA.LookingAt().CompareTag("Interactable")) {
            if (Input.GetKeyDown("e"))
            {
                if(FindFirstObjectByType<Inventory>().keyId == "key1" && LA.LookingAt().name == "Cube_Bright")
                {
                    GameObject.Find("Rust_Key_Bright (2)").GetComponent<Animator>().SetBool("Erscheinen", true);
                }

                if (FindFirstObjectByType<Inventory>().keyId == "key_Bright_1" && LA.LookingAt().name == "Cube_Bright (1)")
                {
                    GameObject.Find("Rust_Key_Bright (3)").GetComponent<Animator>().SetBool("Erscheinen", true);
                }
                    
                if (FindFirstObjectByType<Inventory>().keyId == "key_Bright_2" && LA.LookingAt().name == "Cube_Bright (2)")
                {
                    GameObject.Find("Rust_Key_Bright (4)").GetComponent<Animator>().SetBool("Erscheinen", true);
                }
                if (FindFirstObjectByType<Inventory>().keyId == "key_Bright_3" && LA.LookingAt().name == "Padlock") {
                    GameObject.Find("Padlock").SetActive(false);
                    Enemy.SetActive(true);
                    FindFirstObjectByType<Player_Finder>().Scene = "Bright_Start";
                }
            }
        }
    }
}
