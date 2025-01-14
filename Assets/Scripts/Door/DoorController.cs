using Unity.VisualScripting;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject door;  // Referenz auf das Tür-Objekt
    public bool isOpen = false; // Zustand der Tür (offen oder geschlossen)
    public bool isAnimating = false; // Animation-Status (ob die Animation noch läuft)
    public int DoorIndex;

    void Start()
    {
        if(door == null)
        door = gameObject;
    }
    public void ToggleDoor()
    {
        print("interact with door");
        // Überprüfe, ob gerade eine Animation läuft
        if (isAnimating)
        {
            return;
        }

        // Animation-Component holen
        Animator anim = door.GetComponent<Animator>();


        if (anim != null && DoorIndex == 1)
        {
            isOpen = !isOpen;  // Markiere die Tür als geschlossen
            isAnimating = true;  // Setze den Animationsstatus auf "läuft"
            anim.SetBool("isOpen_Obj_1", isOpen);
        } 
        else if (anim != null && DoorIndex == 2) 
        {
            isOpen = !isOpen;  // Markiere die Tür als geschlossen
            isAnimating = true;  // Setze den Animationsstatus auf "läuft"
            anim.SetBool("isOpen_Obj_2", isOpen);
        }
        else if (anim != null && DoorIndex == 3)
        {
            isOpen = !isOpen;  // Markiere die Tür als geschlossen
            isAnimating = true;  // Setze den Animationsstatus auf "läuft"
            anim.SetBool("isOpen_Obj_3", isOpen);
        }
        else if (anim != null && DoorIndex == 4)
        {
            isOpen = !isOpen;  // Markiere die Tür als geschlossen
            isAnimating = true;  // Setze den Animationsstatus auf "läuft"
            anim.SetBool("isOpen_Obj_4", isOpen);
        }
        else if (anim != null && DoorIndex == 5)
        {
            isOpen = !isOpen;  // Markiere die Tür als geschlossen
            isAnimating = true;  // Setze den Animationsstatus auf "läuft"
            anim.SetBool("isOpen_Obj_5", isOpen);
        }
        else if (anim != null && DoorIndex == 6)
        {
            isOpen = !isOpen;  // Markiere die Tür als geschlossen
            isAnimating = true;  // Setze den Animationsstatus auf "läuft"
            anim.SetBool("isOpen_Obj_6", isOpen);
        }
        else if (anim != null && DoorIndex == 7)
        {
            isOpen = !isOpen;  // Markiere die Tür als geschlossen
            isAnimating = true;  // Setze den Animationsstatus auf "läuft"
            anim.SetBool("isOpen_Obj_7", isOpen);
        }
        else
        {
            Debug.LogWarning("Keine Animation an der Tür gefunden!");
        }
    }

    // Methode, die nach Abschluss der Animation aufgerufen wird
    private void FinishAnimation()
    {
        isAnimating = false;  // Animation ist abgeschlossen, Tür kann wieder gesteuert werden
    }


}
