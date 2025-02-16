using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LabRiddle : MonoBehaviour
{
    public Transform CameraHolder,PlayerCameraHolder;
    public Camera PlayerCamera;
    public Player_Movement Player_MovementScript;
    public Player_Camera Player_CameraScript;
    public IsLookingAt LA;
    public bool IsOnTable;
    public GameObject CrossHairUI,InventoryUI,Image,Mischung;
    public TextMeshProUGUI NameText;
    public BoxCollider TableCollidor;
    public Button Reset;
    
    GameObject SelectedObject;
    Vector3 SelectedObjectPosition;
    Vector3 offset;
    float zPosition;
    bool RätselGelöst;

    // Update is called once per frame
    void Update()
    {
        Ray ray = PlayerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

            if (LA.LookingAt() != null && LA.LookingAt().name == "LabTable" && IsOnTable == false && Input.GetKeyDown(InputManager.Instance.Interact)) {
            OnTable();
        } else if (IsOnTable == true && Input.GetKeyDown(InputManager.Instance.Interact)) { 
            OffTable();
        }

        if (IsOnTable) {
            ShowName();
            if (Physics.Raycast(ray, out hit)) {
                if (Input.GetMouseButtonDown(0) && SelectedObject == null) {
                    SelectObject();
                } else if (Input.GetMouseButtonDown(0) && SelectedObject != null && !hit.collider.gameObject.name.StartsWith("Test")) {
                    SchmelzenUndZerkleinern();
                } else if (Input.GetMouseButtonDown(0) && SelectedObject != null) {
                    SelectedObject.transform.position = SelectedObjectPosition;
                    SelectedObject = null;
                }
            }
        }
        
        if(SelectedObject != null){
            DragSelectedObject();
        }
    }

    public void OnTable() {
        PlayerCamera.transform.SetParent(CameraHolder,true);
        CameraHolder.transform.GetChild(0).transform.localPosition = Vector3.zero;
        CameraHolder.transform.GetChild(0).transform.localScale = Vector3.one;
        CameraHolder.transform.GetChild(0).transform.localRotation = Quaternion.identity;
        Player_CameraScript.enabled = false;
        Player_MovementScript.enabled = false;
        CrossHairUI.SetActive(false);
        InventoryUI.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        IsOnTable = true;
        TableCollidor.enabled = false;
        Reset.gameObject.SetActive(true);
        //GlobusUI.SetActive(true);
    }

    public void OffTable() {
        PlayerCamera.transform.SetParent(PlayerCameraHolder, true);
        PlayerCameraHolder.transform.GetChild(0).transform.localPosition = Vector3.zero;
        PlayerCameraHolder.transform.GetChild(0).transform.localScale = Vector3.one;
        PlayerCameraHolder.transform.GetChild(0).transform.localRotation = Quaternion.identity;
        Player_CameraScript.enabled = true;
        Player_MovementScript.enabled = true;
        CrossHairUI.SetActive(true);
        InventoryUI.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        IsOnTable = false;
        TableCollidor.enabled = true;
        Image.SetActive(false);
        NameText.gameObject.SetActive(false);
        Reset.gameObject.SetActive(false);
        //GlobusUI.SetActive(false);
    }

    public void ShowName() {
        Ray ray = PlayerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.CompareTag("Interactable"))
            {
                Image.SetActive(true);
                NameText.gameObject.SetActive(true);
                if (!hit.collider.gameObject.name.StartsWith("Test")) {
                    NameText.text = $"{hit.collider.gameObject.name}";
                } else {
                    NameText.text = $"{hit.collider.gameObject.GetComponent<TestTubes>().ElementName} ({hit.collider.gameObject.GetComponent<TestTubes>().Kürzel})\n({hit.collider.gameObject.GetComponent<TestTubes>().Zustand})";
                }
                NameText.transform.position = Input.mousePosition + new Vector3(10,15,0);
                Image.transform.position = Input.mousePosition + new Vector3(10,0,0);
            } else {
                Image.SetActive(false);
                NameText.gameObject.SetActive(false);
                NameText.text = "";
            }
        } else {
            Image.SetActive(false);
            NameText.gameObject.SetActive(false);
            NameText.text = "";
        }
    }

    public void SelectObject() {
        Ray ray = PlayerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.name.StartsWith("Test Tube"))
            {
                SelectedObject = hit.collider.gameObject;
                SelectedObjectPosition = SelectedObject.transform.position;
                zPosition = PlayerCamera.WorldToScreenPoint(SelectedObject.transform.position).z;
                offset = SelectedObject.transform.position - GetMouseWorldPosition();
            }
        }
    }

    public Vector3 GetMouseWorldPosition() {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = zPosition;
        return PlayerCamera.ScreenToWorldPoint(mousePosition);
    }

    public void DragSelectedObject() {
        Vector3 mouseWorldPosition = GetMouseWorldPosition();
        SelectedObject.transform.position = mouseWorldPosition + offset;
    }

    public void SchmelzenUndZerkleinern(){
        Ray ray = PlayerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)){
            if (SelectedObject != null && hit.collider.gameObject.name == "Mörser & Stößel") {
                SelectedObject.GetComponent<TestTubes>().Zustand = "Zerkleinert";
            } else if (SelectedObject != null && hit.collider.gameObject.name == "Brenner") {
                SelectedObject.GetComponent<TestTubes>().Zustand = "Flüssig";
            } else if (SelectedObject != null && hit.collider.gameObject.name == "Mischung") {
                Mischung.GetComponent<Mischung>().TestTubes.Add(SelectedObject.GetComponent<TestTubes>());
            }
        }
    }

    public void RätselPrüfen() {
    }

    public void ResetMischung() {
        Mischung.GetComponent<Mischung>().TestTubes.Clear();
    }
}