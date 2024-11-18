using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Is_Portable : MonoBehaviour
{
    public float rayLength = 4f;
    public Inventory Inventory;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayLength))
        {
            if (hit.collider.gameObject.CompareTag("Portable"))
            {
                if (Input.GetKeyDown("e"))
                {
                    if (Inventory.Slot1_Belegt == false && Inventory.Slot2_Belegt == false)
                    {
                        hit.collider.gameObject.transform.SetParent(Inventory.Slot1, false);
                        Inventory.Slot1.GetChild(0).transform.localPosition = Vector3.zero;
                        Inventory.Slot1.GetChild(0).transform.localScale = Vector3.one;
                        Inventory.Slot1.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
                        Inventory.Slot1_Belegt = true;
                        Inventory.Slot_Switchen(1);
                        if(hit.transform.GetComponent<Animator>() != null){
                            hit.transform.GetComponent<Animator>().enabled = false;
                        }
                    }
                    else if (Inventory.Slot1_Belegt == true && Inventory.Slot2_Belegt == false)
                    {
                        hit.collider.gameObject.transform.SetParent(Inventory.Slot2, false);
                        Inventory.Slot2.GetChild(0).transform.localPosition = Vector3.zero;
                        Inventory.Slot2.GetChild(0).transform.localScale = Vector3.one;
                        Inventory.Slot2.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
                        Inventory.Slot2_Belegt = true;
                        Inventory.Slot_Switchen(2);
                        if(hit.transform.GetComponent<Animator>() != null){
                            hit.transform.GetComponent<Animator>().enabled = false;
                        }
                    }
                    else if (Inventory.Slot1_Belegt == false && Inventory.Slot2_Belegt == true)
                    {
                        hit.collider.gameObject.transform.SetParent(Inventory.Slot1, false);
                        Inventory.Slot1.GetChild(0).transform.localPosition = Vector3.zero;
                        Inventory.Slot1.GetChild(0).transform.localScale = Vector3.one;
                        Inventory.Slot1.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
                        Inventory.Slot1_Belegt = true;
                        Inventory.Slot_Switchen(1);
                        if(hit.transform.GetComponent<Animator>() != null){
                            hit.transform.GetComponent<Animator>().enabled = false;
                        }
                    }
                }
            }
        }
    }
}
