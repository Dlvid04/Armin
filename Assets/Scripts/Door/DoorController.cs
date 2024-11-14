using Unity.VisualScripting;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject door;  // Referenz auf das Tür-Objekt
    private bool isOpen = false; // Zustand der Tür (offen oder geschlossen)
    private bool isAnimating = false; // Animation-Status (ob die Animation noch läuft)

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
            Debug.Log("Die Tür ist noch in Bewegung. Warte, bis die Animation abgeschlossen ist.");
            return;
        }

        // Animation-Component holen
        Animator anim = door.GetComponent<Animator>();

        
        if (anim != null)
        {
                    isOpen = !isOpen;  // Markiere die Tür als geschlossen
                    isAnimating = true;  // Setze den Animationsstatus auf "läuft"
                    anim.SetBool("isOpen_Obj_1", isOpen);
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
        Debug.Log("Animation beendet. Türstatus: " + (isOpen ? "offen" : "geschlossen"));
    }
}
