using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home_Dark_Riddle : MonoBehaviour
{
    public GameObject Riddle1;
    public GameObject Riddle2;
    public GameObject Riddle3;

    public IsLookingAt LA;
    [SerializeField] GameObject Enemy;


    // Update is called once per frame
    void Update() {
        if (LA.LookingAt() != null &&  LA.LookingAt().CompareTag("Interactable")) {
            if (Input.GetKeyDown("e")) {
                if (FindFirstObjectByType<Inventory>().keyId == "key1" && LA.LookingAt().name == "Cube_Dark") {
                    GameObject.Find("Rust_Key_Dark (2)").GetComponent<Animator>().SetBool("Erscheinen", true);
                }
                if (FindFirstObjectByType<Inventory>().keyId == "key_Dark_1" && LA.LookingAt().name == "Cube_Dark (1)") {
                    GameObject.Find("Rust_Key_Dark (3)").GetComponent<Animator>().SetBool("Erscheinen", true);
                }
                if (FindFirstObjectByType<Inventory>().keyId == "key_Dark_2" && LA.LookingAt().name == "Cube_Dark (2)") {
                    GameObject.Find("Rust_Key_Dark (4)").GetComponent<Animator>().SetBool("Erscheinen", true);
                }
                if (FindFirstObjectByType<Inventory>().keyId == "key_Dark_3" && LA.LookingAt().name == "Padlock") {
                    GameObject.Find("Padlock").SetActive(false);
                    Enemy.SetActive(true);
                    FindFirstObjectByType<Player_Finder>().Scene = "Dark_Start";
                }
            }
        }
    }
}
