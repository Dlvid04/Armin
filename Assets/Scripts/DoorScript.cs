using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DoorScript : MonoBehaviour
{
    public float rayLength = 4f;
    public TMP_Text doorText;

    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, rayLength))
        {
            if (hit.collider.gameObject.tag == "Door")
            {
                doorText.gameObject.SetActive(true);

                if (Input.GetKeyDown("e"))
                {
                    hit.collider.gameObject.SendMessage("onDoorScript");
                }
            }
            else
            {
                doorText.gameObject.SetActive(false);
            }
        }
        else
        {
            doorText.gameObject.SetActive(false);
        }
    }
}
