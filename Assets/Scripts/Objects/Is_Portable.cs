using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Is_Portable : MonoBehaviour
{
    public IsLookingAt LA;
    public Inventory Inventory;

    // Update is called once per frame
    void Update()
    {
        if (LA.LookingAt() != null &&  LA.LookingAt().CompareTag("Portable"))
        {
            if (Input.GetKeyDown("e"))
            {
                if (Inventory.Slot1_Belegt == false && Inventory.Slot2_Belegt == false)
                {
                    LA.LookingAt().transform.SetParent(Inventory.Slot1, false);
                    Inventory.Slot1.GetChild(0).transform.localPosition = Vector3.zero;
                    Inventory.Slot1.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
                    Inventory.Slot1_Belegt = true;
                    Inventory.Slot_Switchen(1);
                    if(LA.LookingAt().transform.GetComponent<Animator>() != null){
                        LA.LookingAt().transform.GetComponent<Animator>().enabled = false;
                    }
                }
                else if (Inventory.Slot1_Belegt == true && Inventory.Slot2_Belegt == false)
                {
                    LA.LookingAt().transform.SetParent(Inventory.Slot2, false);
                    Inventory.Slot2.GetChild(0).transform.localPosition = Vector3.zero;
                    Inventory.Slot2.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
                    Inventory.Slot2_Belegt = true;
                    Inventory.Slot_Switchen(2);
                    if(LA.LookingAt().transform.GetComponent<Animator>() != null){
                        LA.LookingAt().transform.GetComponent<Animator>().enabled = false;
                    }
                }
                else if (Inventory.Slot1_Belegt == false && Inventory.Slot2_Belegt == true)
                {
                    LA.LookingAt().transform.SetParent(Inventory.Slot1, false);
                    Inventory.Slot1.GetChild(0).transform.localPosition = Vector3.zero;
                    Inventory.Slot1.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
                    Inventory.Slot1_Belegt = true;
                    Inventory.Slot_Switchen(1);
                    if(LA.LookingAt().transform.GetComponent<Animator>() != null){
                        LA.LookingAt().transform.GetComponent<Animator>().enabled = false;
                    }
                }
            }
        }
    }
}
