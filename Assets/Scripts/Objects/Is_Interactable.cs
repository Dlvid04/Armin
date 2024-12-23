using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Is_Interactable : MonoBehaviour
{
    private GameObject Crosshair;
    public IsLookingAt LA;

    // Start is called before the first frame update
    void Start()
    {
        Crosshair = GameObject.Find("Image_Interactable");
        Crosshair.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (LA.LookingAt() != null &&  LA.LookingAt().CompareTag("Interactable") || LA.LookingAt() != null &&  LA.LookingAt().CompareTag("Door") || LA.LookingAt() != null &&  LA.LookingAt().CompareTag("Portable")) {
                Crosshair.SetActive(true);
            } else { 
                Crosshair.SetActive(false); 
        }        
    }
}
