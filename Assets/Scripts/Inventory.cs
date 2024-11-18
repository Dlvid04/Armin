using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Transform Items_On_Ground;
    public Transform Slot1, Slot2;
    public bool Slot1_Belegt, Slot2_Belegt = false;
    bool Slot1_Angezeigt = true;
    bool Slot2_Angezeigt = false;
    public string keyId ;
    


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

        if(Slot1_Angezeigt && Slot1_Belegt){
            Key K = Slot1.GetChild(0).GetComponent<Key>();
            if(K != null){
                keyId = K.id;
            }
        }else if(Slot2_Angezeigt && Slot2_Belegt){
            Key K = Slot2.GetChild(0).GetComponent<Key>();
            if(K != null){
                keyId = K.id;
            }
        }
    }

    public void Slot_Switchen(int Slot) {
        if (Slot == 1)
        {
            Slot1.GetChild(0).gameObject.SetActive(true);
            if (Slot2_Belegt) {
                Slot2.GetChild(0).gameObject.SetActive(false);
            }
            Slot1_Angezeigt = true;
            Slot2_Angezeigt = false;
        } else if (Slot == 2) {
            if (Slot1_Belegt) {
                Slot1.GetChild(0).gameObject.SetActive(false);
            }
            Slot2.GetChild(0).gameObject.SetActive(true);
            Slot1_Angezeigt = false;
            Slot2_Angezeigt = true;
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
}
