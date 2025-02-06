using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class Laptop : MonoBehaviour
{
    public Camera PlayerCamera;
    public Transform CameraHolderLaptop,CameraHolderPlayer;
    public IsLookingAt LA;
    public Player_Camera PCScript;
    public Player_Movement PMScript;
    public GameObject ToDOListeUI,EMailUI,EMailButton,ToDoListeButton,CrossHairUI,LaptopUI,InventoryUI;
    public TextMeshProUGUI ToDOListeText,EMailText;
    bool EmailButtonPressed,ToDoButtonPressed;
    public bool IsOnLaptop;

    void Start(){
        LaptopUI.SetActive(false);
    }
    void Update(){
        if(LA.LookingAt() != null &&  LA.LookingAt().name == "Laptop" && IsOnLaptop == false && LA.LookingAt().tag == "Interactable"){
            if (Input.GetKey(InputManager.Instance.Interact)) {
                LaptopAn();
            }
        }else if(IsOnLaptop == true && Input.GetKey(InputManager.Instance.Interact)) {
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
        InventoryUI.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        IsOnLaptop = true;
    }

    public void LaptopAus(){
        PlayerCamera.transform.SetParent(CameraHolderPlayer,true);
        CameraHolderPlayer.GetChild(0).transform.localPosition = Vector3.zero;
        CameraHolderPlayer.GetChild(0).transform.localScale = Vector3.one;
        CameraHolderPlayer.GetChild(0).transform.localRotation = Quaternion.identity;
        PCScript.enabled = true;
        PMScript.enabled = true;
        CrossHairUI.SetActive(true);
        InventoryUI.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        IsOnLaptop = false;
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
