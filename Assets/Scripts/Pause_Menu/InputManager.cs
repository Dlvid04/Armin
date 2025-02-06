using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    public GameObject SteurungMenue;
    // Standardbelegungen
    public KeyCode moveForward;
    public KeyCode moveLeft;
    public KeyCode moveBackward;
    public KeyCode moveRight;
    public KeyCode interact;
    public KeyCode sprinten;
    public KeyCode inventar1;
    public KeyCode inventar2;
    public KeyCode throwOut;
    public TMP_InputField VorwärtsField,LeftField,RightField,BackwardField,InteractField,SprintenField,Inventar1Field,Inventar2Field,ThrowOutField;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            SteurungMenue.SetActive(false);
        } else {
            Debug.LogError("More than one InputManager in the scene!");
            Destroy(gameObject);
            return;
        }
    }

    public KeyCode ThrowOut { 
        get { return throwOut; } set { throwOut = value; }
    }

    public KeyCode MoveForward {
        get { return moveForward; } set {  moveForward = value; }
    }

    public KeyCode MoveLeft { 
        get { return moveLeft; } set {  moveLeft = value; }
    }

    public KeyCode MoveBackward { 
        get { return moveBackward; } set { moveBackward = value; }
    }

    public KeyCode MoveRight { 
        get { return moveRight; } set { moveRight = value; }
    }

    public KeyCode Interact {
        get { return interact; } set { interact = value; }
    }

    public KeyCode Sprinten { 
        get { return sprinten; } set {  sprinten = value; }
    }

    public KeyCode Inventar1 { 
        get { return inventar1; } set {  inventar1 = value; }
    }

    public KeyCode Inventar2 { 
        get { return inventar2; } set {  inventar2 = value; }
    }

    public void ChangeVorwärts() {
        if (Enum.TryParse(VorwärtsField.text, true, out KeyCode newKey)) {
            MoveForward = newKey;
        }
    }

    public void ChangeLeft() {
        if (Enum.TryParse(LeftField.text, true, out KeyCode newKey)) {
            MoveLeft = newKey;
        }
    }
    public void ChangeBackward() {
        if (Enum.TryParse(BackwardField.text, true, out KeyCode newKey)) {
            MoveBackward = newKey;
        }
    }
    public void ChangeRight() {
        if (Enum.TryParse(RightField.text, true, out KeyCode newKey)) {
            MoveRight = newKey;
        }
    }
    public void ChangeInteract() {
        if (Enum.TryParse(InteractField.text, true, out KeyCode newKey)) {
            Interact = newKey;
        }
    }
    public void ChangeSprinten() {
        if (SprintenField.text == "Shift" || SprintenField.text == "shift") {
                Sprinten = KeyCode.LeftShift;
        }else if (Enum.TryParse(SprintenField.text, true, out KeyCode newKey)) {
            Sprinten = newKey;
        }
    }
    public void ChangeInvetar1() {
        string keyCodeName = "Alpha" + Inventar1Field.text;
        if (Enum.TryParse(keyCodeName, true, out KeyCode newKey)) {
            Inventar1 = newKey;
        }
    }
    public void ChangeInventar2() {
        string keyCodeName = "Alpha" + Inventar1Field.text;
        if (Enum.TryParse(keyCodeName, true, out KeyCode newKey)) {
            Inventar2 = newKey;
        }
    }
    public void ChangeThrowOut() {
        if (Enum.TryParse(ThrowOutField.text, true, out KeyCode newKey)) {
            ThrowOut = newKey;
        }
    }
}
