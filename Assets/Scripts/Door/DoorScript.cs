using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DoorScript : MonoBehaviour
{
    public IsLookingAt LA;
    public GameObject Interactable;

    void Start()
    {
        Interactable = GameObject.Find("Image_Interactable");
    }


    void Update()
    {
        if (LA.LookingAt() != null &&  LA.LookingAt().CompareTag("Door")) {
            if (Input.GetKey(InputManager.Instance.Interact)) {                    
                LA.LookingAt().GetComponentInParent<DoorController>().SendMessage("ToggleDoor");
            }
        }
    }
}
