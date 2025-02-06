using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ClockRiddle : MonoBehaviour
{
    public WaschmaschinenRätsel WaschmaschinenRätsel;
    public GameObject MinutHand,HourHand,Clock,CrossHairUI,InventoryUI,ClockUI,Waschmaschine,laptop;
    public Transform MinutHandHolder,HourHandHolder,CameraHolderClock,CameraHolderPlayer;
    public Inventory PlayerInventory;
    public IsLookingAt LA;
    public Player_Camera PCScript;
    public Player_Movement PMScript;
    public Camera PlayerCamera;
    public bool IsOnClock;

    void Start(){
        ClockUI.SetActive(false);
        Waschmaschine.tag = tag.Normalize();
    }
    void Update()
    { 
        if(LA.LookingAt() != null && LA.LookingAt().name == "Clock" && PlayerInventory.Slot1_Angezeigt && Input.GetKey(InputManager.Instance.Interact)) {
            if(PlayerInventory.ObjectHolder1.childCount > 0 && PlayerInventory.ObjectHolder1.GetChild(0).name == "Minuten Zeiger"){
                PlayerInventory.GegenstandVerwenden(PlayerInventory.ObjectHolder1.GetChild(0).name);
                PlayerInventory.GegenstandImInventarTransform("Minuten Zeiger").SetParent(MinutHandHolder,false);
                MinutHandHolder.GetChild(0).transform.localPosition = Vector3.zero;
                MinutHandHolder.GetChild(0).transform.localRotation = Quaternion.identity;
                MinutHandHolder.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
            }else if(PlayerInventory.ObjectHolder1.childCount > 0 && PlayerInventory.ObjectHolder1.GetChild(0).name == "Stunden Zeiger"){
                PlayerInventory.GegenstandVerwenden(PlayerInventory.ObjectHolder1.GetChild(0).name);
                PlayerInventory.GegenstandImInventarTransform("Stunden Zeiger").SetParent(HourHandHolder,false);
                HourHandHolder.GetChild(0).transform.localPosition = Vector3.zero;
                HourHandHolder.GetChild(0).transform.localRotation = Quaternion.identity;
                HourHandHolder.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
            }
        }else if(LA.LookingAt() != null && LA.LookingAt().name == "Clock" && PlayerInventory.Slot2_Angezeigt && Input.GetKey(InputManager.Instance.Interact)) {
            if(PlayerInventory.ObjectHolder2.childCount > 0 && PlayerInventory.ObjectHolder2.GetChild(0).name == "Minuten Zeiger") {
                PlayerInventory.GegenstandVerwenden(PlayerInventory.ObjectHolder2.GetChild(0).name);
                PlayerInventory.GegenstandImInventarTransform("Minuten Zeiger").SetParent(MinutHandHolder,false);
                MinutHandHolder.GetChild(0).transform.localPosition = Vector3.zero;
                MinutHandHolder.GetChild(0).transform.localRotation = Quaternion.identity;
                MinutHandHolder.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
            }else if(PlayerInventory.ObjectHolder2.childCount > 0 && PlayerInventory.ObjectHolder2.GetChild(0).name == "Stunden Zeiger") {
                PlayerInventory.GegenstandVerwenden(PlayerInventory.ObjectHolder2.GetChild(0).name);
                PlayerInventory.GegenstandImInventarTransform("Stunden Zeiger").SetParent(HourHandHolder,false);
                HourHandHolder.GetChild(0).transform.localPosition = Vector3.zero;
                HourHandHolder.GetChild(0).transform.localRotation = Quaternion.identity;
                HourHandHolder.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
            }
        }

        if(HourHandHolder.childCount > 0 && MinutHandHolder.childCount > 0){
            if(LA.LookingAt() != null && LA.LookingAt().name == "Clock" && IsOnClock == false){
                if(Input.GetKey(InputManager.Instance.Interact)) {
                    UhrAn();
                }
            }else if(IsOnClock == true && Input.GetKey(InputManager.Instance.Interact)) {
                UhrAus();
            }
        }
        CheckIfFinished();
    }

    public void UhrAn(){
        PlayerCamera.transform.SetParent(CameraHolderClock,true);
        CameraHolderClock.GetChild(0).transform.localPosition = Vector3.zero;
        CameraHolderClock.GetChild(0).transform.localScale = Vector3.one;
        CameraHolderClock.GetChild(0).transform.localRotation = Quaternion.identity;
        PCScript.enabled = false;
        PMScript.enabled = false;
        CrossHairUI.SetActive(false);
        ClockUI.SetActive(true);
        InventoryUI.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        IsOnClock = true;
    }

        public void UhrAus(){
        PlayerCamera.transform.SetParent(CameraHolderPlayer,true);
        CameraHolderPlayer.GetChild(0).transform.localPosition = Vector3.zero;
        CameraHolderPlayer.GetChild(0).transform.localScale = Vector3.one;
        CameraHolderPlayer.GetChild(0).transform.localRotation = Quaternion.identity;
        PCScript.enabled = true;
        PMScript.enabled = true;
        CrossHairUI.SetActive(true);
        ClockUI.SetActive(false);
        InventoryUI.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        IsOnClock = false;
    }

    public void CheckIfFinished() {
        if (Quaternion.Angle(MinutHand.transform.localRotation, Quaternion.Euler(0, 0, 150)) < 0.1f && Quaternion.Angle(HourHand.transform.localRotation, Quaternion.Euler(0, 0, -90)) < 0.1f) {
            WaschmaschinenRätsel.enabled = true;
            Waschmaschine.tag = "Interactable";
            Clock.tag = tag.Normalize();
            laptop.GetComponent<Laptop>().ToDOListeText.text = "";
            laptop.GetComponent<Laptop>().EMailText.gameObject.SetActive(true);
            UhrAus();
            enabled = false;
        }
    }

    public void ZeigerVorstellen(GameObject gameObject){
        if (gameObject.name == "Stunden Zeiger") {
            gameObject.transform.localRotation *= Quaternion.Euler(0, 0, 30);
        }else
            gameObject.transform.localRotation *= Quaternion.Euler(0, 0, 5);
    }

    public void ZeigerZurükstellen(GameObject gameObject) {
        if (gameObject.name == "Stunden Zeiger") {
            gameObject.transform.localRotation *= Quaternion.Euler(0, 0, -30);
        } else
            gameObject.transform.localRotation *= Quaternion.Euler(0, 0, -5);
    }
}
