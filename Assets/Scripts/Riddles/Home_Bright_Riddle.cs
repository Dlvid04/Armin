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
                    GameObject.Find("Rust_Key").transform.localPosition = new Vector3(-1,0,0);
                }
            }
        }
    }
}
