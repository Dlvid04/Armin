using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home_Dark_Riddle : MonoBehaviour
{
    public GameObject Riddle1;
    public GameObject Riddle2;
    public GameObject Riddle3;

    public float rayLength = 4f;
    [SerializeField] GameObject Enemy;


    // Update is called once per frame
    void Update() {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayLength)) {
            if (hit.collider.gameObject.CompareTag("Interactable")) {
                if (Input.GetKeyDown("e")) {
                    if (FindFirstObjectByType<Inventory>().keyId == "key1" && hit.collider.gameObject.name == "Cube_Dark") {
                        GameObject.Find("Rust_Key_Dark (2)").GetComponent<Animator>().SetBool("Erscheinen", true);
                    }
                    if (FindFirstObjectByType<Inventory>().keyId == "key_Dark_1" && hit.collider.gameObject.name == "Cube_Dark (1)") {
                        GameObject.Find("Rust_Key_Dark (3)").GetComponent<Animator>().SetBool("Erscheinen", true);
                    }
                    if (FindFirstObjectByType<Inventory>().keyId == "key_Dark_2" && hit.collider.gameObject.name == "Cube_Dark (2)") {
                        GameObject.Find("Rust_Key_Dark (4)").GetComponent<Animator>().SetBool("Erscheinen", true);
                    }
                    if (FindFirstObjectByType<Inventory>().keyId == "key_Dark_3" && hit.collider.gameObject.name == "Padlock") {
                        GameObject.Find("Padlock").SetActive(false);
                        Enemy.SetActive(true);
                        FindFirstObjectByType<Player_Finder>().Scene = "Dark_Start";
                    }
                }
            }
        }
    }
}
