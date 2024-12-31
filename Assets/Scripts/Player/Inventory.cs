using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Transform Items_On_Ground,Slot1, Slot2;
    public bool Slot1_Belegt, Slot2_Belegt = false;
    public bool Slot1_Angezeigt = true;
    public bool Slot2_Angezeigt = false;
    public string keyId ;
    public Image SlotUI1,SlotUI2;
    


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            Slot_Switchen(1);
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            Slot_Switchen(2);
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            Throw();
        }
    }

    public void Slot_Switchen(int Slot) {
        if (Slot == 1)
        {
            if (Slot1.childCount > 0)
            {
                Slot1.GetChild(0).gameObject.SetActive(true);
            }
            if (Slot2_Belegt) {
                Slot2.GetChild(0).gameObject.SetActive(false);
            }
            Slot1_Angezeigt = true;
            Slot2_Angezeigt = false;
            SlotUI1.color = new Color32(150,150,150,175);
            SlotUI2.color = new Color32(100,100,100,175);
        } else if (Slot == 2) {
            if (Slot1_Belegt) {
                Slot1.GetChild(0).gameObject.SetActive(false);
            }
            if (Slot2.childCount > 0) 
            {
                Slot2.GetChild(0).gameObject.SetActive(true);
            }
            Slot1_Angezeigt = false;
            Slot2_Angezeigt = true;
            SlotUI2.color = new Color32(150,150,150,175);
            SlotUI1.color = new Color32(100,100,100,175);
        }
    }

    public void Throw() {
        if (Slot1_Angezeigt && Slot1_Belegt)
        {
            Slot1.GetChild(0).gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 1f, ForceMode.Impulse);
            Slot1.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
            Slot1.GetChild(0).gameObject.transform.SetParent(Items_On_Ground, true);
            Slot1_Belegt = false;
        }
        else if (Slot2_Angezeigt && Slot2_Belegt) 
        {
            Slot2.GetChild(0).gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 1f, ForceMode.Impulse);
            Slot2.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
            Slot2.GetChild(0).gameObject.transform.SetParent(Items_On_Ground,true);
            Slot2_Belegt = false;
        }
    }

    public Transform GegenstandImInventarSuchen(string GegenstandsNamen){ //Sucht einen Gegenstand und Gibt dessen Inventarplatz Transform wieder
        if(Slot1.childCount > 0 && Slot1.GetChild(0).name == GegenstandsNamen){
            return Slot1.GetChild(0);
        }else if(Slot2.childCount > 0 && Slot2.GetChild(0).name == GegenstandsNamen){
            return Slot2.GetChild(0);
        }else
            return null;
    }

    public void GegenstandVerwenden(string GegenstandsNamen){
        if(Slot1.GetChild(0) != null && Slot1.GetChild(0).name == GegenstandsNamen){
            Slot1_Belegt = false;
        }else if(Slot1.GetChild(0) != null && Slot2.GetChild(0).name == GegenstandsNamen){
            Slot2_Belegt = false;
        }
    }
}
