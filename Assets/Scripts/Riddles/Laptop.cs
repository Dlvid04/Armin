using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Laptop : MonoBehaviour
{
    public Camera PlayerCamera;
    public Transform CameraHolderLaptop;
    public Transform CameraHolderPlayer;
    public IsLookingAt LA;
    public Player_Camera PCScript;
    public Player_Movement PMScript;
    public GameObject ToDOListeUI,EMailUI,EMailButton,ToDoListeButton,CrossHairUI,LaptopUI;
    bool EmailButtonPressed,ToDoButtonPressed;
    public bool IsOnLaptop;

    void Update(){
        if(LA.LookingAt() != null &&  LA.LookingAt().CompareTag("Laptop") && IsOnLaptop == false){
            if (Input.GetKeyDown("e")){
                LaptopAn();
                IsOnLaptop = true;
            }
        }else if(IsOnLaptop == true && Input.GetKeyDown("e")){
            IsOnLaptop = false;
            LaptopAus();
        }

    }
    public void LaptopAn(){
        PlayerCamera.transform.SetParent(CameraHolderLaptop,true);
        CameraHolderLaptop.GetChild(0).transform.localPosition = Vector3.zero;
        CameraHolderLaptop.GetChild(0).transform.localScale = Vector3.one;
        CameraHolderLaptop.GetChild(0).transform.localRotation = Quaternion.identity;
        PCScript.enabled = false;
        PMScript.enabled = false;
        CrossHairUI.SetActive(false);
        LaptopUI.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LaptopAus(){
        PlayerCamera.transform.SetParent(CameraHolderPlayer,true);
        CameraHolderPlayer.GetChild(0).transform.localPosition = Vector3.zero;
        CameraHolderPlayer.GetChild(0).transform.localScale = Vector3.one;
        CameraHolderPlayer.GetChild(0).transform.localRotation = Quaternion.identity;
        PCScript.enabled = true;
        PMScript.enabled = true;
        CrossHairUI.SetActive(true);
        LaptopUI.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ToDoListeButtonEnter(){
        if(ToDoButtonPressed){
            ToDOListeUI.SetActive(false);
            EMailButton.SetActive(true);
            ToDoButtonPressed = false;
        }else if(!ToDoButtonPressed){
            ToDOListeUI.SetActive(true);
            EMailButton.SetActive(false);
            ToDoButtonPressed = true;
        }
    }
        public void EMailButtonEnter(){
        if(EmailButtonPressed){
            ToDoListeButton.SetActive(true);
            EMailUI.SetActive(false);
            EmailButtonPressed = false;
        }else if(!EmailButtonPressed){
            ToDoListeButton.SetActive(false);
            EMailUI.SetActive(true);
            EmailButtonPressed = true;
        }
    }
    
}
