using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DoorScript : MonoBehaviour
{
    public float rayLength = 4f;
    public GameObject Interactable;

    void Start()
    {
        Interactable = GameObject.Find("Image_Interactable");
    }


    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, rayLength))
        {
            if (hit.collider.gameObject.tag == "Door")
            {
                if (Input.GetKeyDown("e"))
                {
                    hit.collider.gameObject.SendMessage("ToggleDoor");
                }
            }
        }
    }
}
