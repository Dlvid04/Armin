using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Is_Interactable : MonoBehaviour
{
    private GameObject Crosshair;
    public float rayLength = 4f;

    // Start is called before the first frame update
    void Start()
    {
        Crosshair = GameObject.Find("Image_Interactable");
        Crosshair.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayLength)) {
            if (hit.collider.gameObject.CompareTag("Interactable") || hit.collider.gameObject.CompareTag("Door")) {
                Crosshair.SetActive(true);
            }
            else { 
                Crosshair.SetActive(false); 
            }
        }else {
            Crosshair.SetActive(false);
        }
    }
}
