using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ClockRiddle : MonoBehaviour
{
    public GameObject MinutHand,HourHand,CrossHairUI,InventoryUI,ClockUI;
    public Transform MinutHandHolder,HourHandHolder,CameraHolderClock,CameraHolderPlayer;
    public Inventory PlayerInventory;
    public IsLookingAt LA;
    public Player_Camera PCScript;
    public Player_Movement PMScript;
    public Camera PlayerCamera;
    bool IsOnClock;

    void Start(){
        ClockUI.SetActive(false);
    }
    void Update()
    { 
        if(LA.LookingAt() != null && LA.LookingAt().name == "Clock" && PlayerInventory.Slot1_Angezeigt && Input.GetKeyDown("e")){
            if(PlayerInventory.Slot1.childCount > 0 && PlayerInventory.Slot1.GetChild(0).name == "MinuteHand"){
                PlayerInventory.GegenstandVerwenden(PlayerInventory.Slot1.GetChild(0).name);
                PlayerInventory.GegenstandImInventarSuchen("MinuteHand").SetParent(MinutHandHolder,false);
                MinutHandHolder.GetChild(0).transform.localPosition = Vector3.zero;
                MinutHandHolder.GetChild(0).transform.localRotation = Quaternion.identity;
                MinutHandHolder.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
            }else if(PlayerInventory.Slot1.childCount > 0 && PlayerInventory.Slot1.GetChild(0).name == "HourHand"){
                PlayerInventory.GegenstandVerwenden(PlayerInventory.Slot1.GetChild(0).name);
                PlayerInventory.GegenstandImInventarSuchen("HourHand").SetParent(HourHandHolder,false);
                HourHandHolder.GetChild(0).transform.localPosition = Vector3.zero;
                HourHandHolder.GetChild(0).transform.localRotation = Quaternion.identity;
                HourHandHolder.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
            }
        }else if(LA.LookingAt() != null && LA.LookingAt().name == "Clock" && PlayerInventory.Slot2_Angezeigt && Input.GetKeyDown("e")){
            if(PlayerInventory.Slot2.childCount > 0 && PlayerInventory.Slot2.GetChild(0).name == "MinuteHand"){
                PlayerInventory.GegenstandVerwenden(PlayerInventory.Slot2.GetChild(0).name);
                PlayerInventory.GegenstandImInventarSuchen("MinuteHand").SetParent(MinutHandHolder,false);
                MinutHandHolder.GetChild(0).transform.localPosition = Vector3.zero;
                MinutHandHolder.GetChild(0).transform.localRotation = Quaternion.identity;
                MinutHandHolder.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
            }else if(PlayerInventory.Slot2.childCount > 0 && PlayerInventory.Slot2.GetChild(0).name == "HourHand"){
                PlayerInventory.GegenstandVerwenden(PlayerInventory.Slot2.GetChild(0).name);
                PlayerInventory.GegenstandImInventarSuchen("HourHand").SetParent(HourHandHolder,false);
                HourHandHolder.GetChild(0).transform.localPosition = Vector3.zero;
                HourHandHolder.GetChild(0).transform.localRotation = Quaternion.identity;
                HourHandHolder.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
            }
        }

        if(HourHandHolder.childCount > 0 && MinutHandHolder.childCount > 0){
            if(LA.LookingAt() != null && LA.LookingAt().name == "Clock" && IsOnClock == false){
                if(Input.GetKeyDown("e")){
                    UhrAn();
                    IsOnClock = true;
                }
            }else if(IsOnClock == true && Input.GetKeyDown("e")){
                UhrAus();
                IsOnClock = false;
            }
        }

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
    }

    public void zeigerVorstellen(){

    }

    public void zeigerZurükstellen(){

    }
}
