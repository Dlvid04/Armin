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
            if (Input.GetKeyDown(InputManager.Instance.Interact))
            {
                if (Inventory.Slot1_Belegt == false && Inventory.Slot2_Belegt == false)
                {
                    LA.LookingAt().transform.SetParent(Inventory.ObjectHolder1, false);
                    Inventory.ObjectHolder1.GetChild(0).transform.localScale = Inventory.ObjectHolder1.GetChild(0).transform.localScale / 2;
                    Inventory.ObjectHolder1.GetChild(0).transform.localPosition = Vector3.zero;
                    Inventory.ObjectHolder1.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, 0);
                    Inventory.ObjectHolder1.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
                    Inventory.Slot1Text.text = Inventory.ObjectHolder1.GetChild(0).name;
                    Inventory.Slot1_Belegt = true;
                    Inventory.Slot_Switchen(1);
                    if(LA.LookingAt().transform.GetComponent<Animator>() != null){
                        LA.LookingAt().transform.GetComponent<Animator>().enabled = false;
                    }
                }
                else if (Inventory.Slot1_Belegt == true && Inventory.Slot2_Belegt == false)
                {
                    LA.LookingAt().transform.SetParent(Inventory.ObjectHolder2, false);
                    Inventory.ObjectHolder2.GetChild(0).transform.localScale = Inventory.ObjectHolder2.GetChild(0).transform.localScale / 2;
                    Inventory.ObjectHolder2.GetChild(0).transform.localPosition = Vector3.zero;
                    Inventory.ObjectHolder2.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, 0);
                    Inventory.ObjectHolder2.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
                    Inventory.Slot2Text.text = Inventory.ObjectHolder2.GetChild(0).name;
                    Inventory.Slot2_Belegt = true;
                    Inventory.Slot_Switchen(2);
                    if(LA.LookingAt().transform.GetComponent<Animator>() != null){
                        LA.LookingAt().transform.GetComponent<Animator>().enabled = false;
                    }
                }
                else if (Inventory.Slot1_Belegt == false && Inventory.Slot2_Belegt == true)
                {
                    LA.LookingAt().transform.SetParent(Inventory.ObjectHolder1, false);
                    Inventory.ObjectHolder1.GetChild(0).transform.localScale = Inventory.ObjectHolder1.GetChild(0).transform.localScale / 2;
                    Inventory.ObjectHolder1.GetChild(0).transform.localPosition = Vector3.zero;
                    Inventory.ObjectHolder1.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, 0);
                    Inventory.ObjectHolder1.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
                    Inventory.Slot1Text.text = Inventory.ObjectHolder1.GetChild(0).name;
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
