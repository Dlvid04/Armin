using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject door;  // Referenz auf das Tür-Objekt
    private bool isOpen = false; // Zustand der Tür (offen oder geschlossen)
    private bool isAnimating = false; // Animation-Status (ob die Animation noch läuft)

    public void ToggleDoor()
    {
        // Überprüfe, ob gerade eine Animation läuft
        if (isAnimating)
        {
            Debug.Log("Die Tür ist noch in Bewegung. Warte, bis die Animation abgeschlossen ist.");
            return;
        }

        // Animation-Component holen
        Animation anim = door.GetComponent<Animation>();

        if (anim != null)
        {
            if (isOpen)  // Wenn die Tür offen ist, schließe sie
            {
                if (anim["close"] != null)  // Überprüfen, ob die 'close'-Animation existiert
                {
                    Debug.Log("Tür wird geschlossen.");
                    anim.Play("close");
                    isOpen = false;  // Markiere die Tür als geschlossen
                    isAnimating = true;  // Setze den Animationsstatus auf "läuft"
                    Invoke("FinishAnimation", anim["close"].length);  // Nach Abschluss der Animation den Status zurücksetzen
                }
                else
                {
                    Debug.LogWarning("Die 'close'-Animation wurde nicht gefunden!");
                }
            }
            else  // Wenn die Tür geschlossen ist, öffne sie
            {
                if (anim["open"] != null)  // Überprüfen, ob die 'open'-Animation existiert
                {
                    Debug.Log("Tür wird geöffnet.");
                    anim.Play("open");
                    isOpen = true;  // Markiere die Tür als offen
                    isAnimating = true;  // Setze den Animationsstatus auf "läuft"
                    Invoke("FinishAnimation", anim["open"].length);  // Nach Abschluss der Animation den Status zurücksetzen
                }
                else
                {
                    Debug.LogWarning("Die 'open'-Animation wurde nicht gefunden!");
                }
            }
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
