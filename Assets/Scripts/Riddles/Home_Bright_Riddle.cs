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

    public float rayLength = 4f;
    [SerializeField] GameObject Enemy;


    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayLength)) 
        {
            if (hit.collider.gameObject.CompareTag("Interactable"))
            {
                if (Input.GetKeyDown("e"))
                {
                    if(FindFirstObjectByType<Inventory>().keyId == "key1" && hit.collider.gameObject.name == "Cube")
                    {
                        GameObject.Find("Rust_Key (2)").GetComponent<Animator>().SetBool("Erscheinen", true);
                    }
                    if (FindFirstObjectByType<Inventory>().keyId == "key2" && hit.collider.gameObject.name == "Cube (1)")
                    {
                        GameObject.Find("Rust_Key (3)").GetComponent<Animator>().SetBool("Erscheinen", true);
                    }
                    if (FindFirstObjectByType<Inventory>().keyId == "key3" && hit.collider.gameObject.name == "Cube (2)")
                    {
                        Enemy.SetActive(true);
                        FindFirstObjectByType<Player_Finder>().Scene = "Bright_Start";
                    }
                }
            }
        }
    }
}
