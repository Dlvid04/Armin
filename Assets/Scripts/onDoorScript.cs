using UnityEngine;

public class onDoorScript : MonoBehaviour
{
    public GameObject door;
    private bool isOpen = false;

    public void OnDoorScript()
    {
        if (!isOpen)
        {
            door.GetComponent<Animation>().Play("door");
            isOpen = true;
        }
        else
        {
            door.GetComponent<Animation>().Play("close");
            isOpen = false;
        }
    }
}
