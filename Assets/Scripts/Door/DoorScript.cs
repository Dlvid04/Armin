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

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, rayLength))
        {
            Debug.DrawLine(Camera.main.transform.position, hit.point);
            if (hit.collider.gameObject.tag == "Door")
            {
                if (Input.GetKeyDown("e"))
                {
                    hit.collider.gameObject.GetComponentInParent<DoorController>().SendMessage("ToggleDoor");
                    //hit.collider.gameObject.GetComponent<DoorController>().SendMessage("ToggleDoor");
                }
            }
        }
    }
}
