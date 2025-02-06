using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public bool Slot1_Belegt, Slot2_Belegt = false;
    public bool Slot1_Angezeigt = true;
    public bool Slot2_Angezeigt = false;
    public string keyId ;
    public Transform ObjectHolder1,ObjectHolder2,ThrowOut, Items_On_Ground;
    public Renderer PlaneSlot1,PlaneSlot2;
    public TextMeshProUGUI Slot1Text, Slot2Text;
    public Material Holding, Stored;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(InputManager.Instance.Inventar1)) {
            Slot_Switchen(1);
        } else if (Input.GetKey(InputManager.Instance.Inventar2)) {
            Slot_Switchen(2);
        }

        if (Input.GetKey(InputManager.Instance.ThrowOut)) {
            Throw();
        }
    }

    public void Slot_Switchen(int Slot) {
        if (Slot == 1)
        {
            Slot1_Angezeigt = true;
            Slot2_Angezeigt = false;
            PlaneSlot1.material = Holding;
            PlaneSlot2.material = Stored;
        } else if (Slot == 2) {
            Slot1_Angezeigt = false;
            Slot2_Angezeigt = true;
            PlaneSlot1.material = Stored;
            PlaneSlot2.material = Holding;
        }
    }

    public void Throw() {
        Vector3 throwDirection = transform.forward + transform.up * 0.5f;
        if (Slot1_Angezeigt && Slot1_Belegt)
        {
            ObjectHolder1.GetChild(0).transform.SetParent(ThrowOut,true);
            ThrowOut.GetChild(0).transform.localPosition = Vector3.zero;
            ThrowOut.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
            ThrowOut.GetChild(0).gameObject.GetComponent<Rigidbody>().AddForce(throwDirection.normalized * 2f, ForceMode.Impulse);
            ThrowOut.GetChild(0).transform.localScale = ThrowOut.GetChild(0).transform.localScale * 2;
            ThrowOut.GetChild(0).gameObject.transform.SetParent(Items_On_Ground, true);
            Slot1_Belegt = false;
            Slot1Text.text = string.Empty;
        }
        else if (Slot2_Angezeigt && Slot2_Belegt) 
        {
            ObjectHolder2.GetChild(0).transform.SetParent(ThrowOut, true);
            ThrowOut.GetChild(0).transform.localPosition = Vector3.zero;
            ThrowOut.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
            ThrowOut.GetChild(0).gameObject.GetComponent<Rigidbody>().AddForce(throwDirection.normalized * 2f, ForceMode.Impulse);
            ThrowOut.GetChild(0).transform.localScale = ThrowOut.GetChild(0).transform.localScale * 2;
            ThrowOut.GetChild(0).gameObject.transform.SetParent(Items_On_Ground, true);
            Slot2_Belegt = false;
            Slot2Text.text = string.Empty;
        }
    }

    public Transform GegenstandImInventarTransform(string GegenstandsNamen){ //Sucht einen Gegenstand und Gibt dessen Transform wieder
        if (ObjectHolder1.childCount > 0 && ObjectHolder1.GetChild(0).name == GegenstandsNamen) {
            return ObjectHolder1.GetChild(0);
        } else if (ObjectHolder2.childCount > 0 && ObjectHolder2.GetChild(0).name == GegenstandsNamen) {
            return ObjectHolder2.GetChild(0);
        } else
            return null;
    }

    public bool GegenstandImInventarUndAusgerüstet(string GegenstandsNamen) { //Sucht einen Gegenstand, schaut ob dieser ausgerüstet ist und Gibt einen Bool zurück
        if (ObjectHolder1.childCount > 0 && ObjectHolder1.GetChild(0).name == GegenstandsNamen && Slot1_Angezeigt) {
            return true;
        } else if (ObjectHolder2.childCount > 0 && ObjectHolder2.GetChild(0).name == GegenstandsNamen && Slot2_Angezeigt) {
            return true;
        } else
            return false;
    }

    public void GegenstandVerwenden(string GegenstandsNamen){
        if(ObjectHolder1.GetChild(0) != null && ObjectHolder1.GetChild(0).name == GegenstandsNamen){
            Slot1_Belegt = false;
            Slot1Text.text = "";
        }else if(ObjectHolder2.GetChild(0) != null && ObjectHolder2.GetChild(0).name == GegenstandsNamen){
            Slot2_Belegt = false;
            Slot2Text.text = "";
        }
    }
}
