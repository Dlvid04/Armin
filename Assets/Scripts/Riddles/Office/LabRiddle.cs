using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LabRiddle : MonoBehaviour
{
    public Transform CameraHolder,PlayerCameraHolder;
    public Camera PlayerCamera;
    public Player_Movement Player_MovementScript;
    public Player_Camera Player_CameraScript;
    public IsLookingAt LA;
    public bool IsOnTable;
    public GameObject CrossHairUI,InventoryUI;
    public TextMeshProUGUI NameText;
    public BoxCollider TableCollidor;
    
    GameObject SelectedObject;
    Vector3 SelectedObjectPosition;
    Vector3 offset;
    float zPosition;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (LA.LookingAt() != null && LA.LookingAt().name == "LabTable" && IsOnTable == false && Input.GetKeyDown(InputManager.Instance.Interact)) {
            OnTable();
        } else if (IsOnTable == true && Input.GetKeyDown(InputManager.Instance.Interact)) { 
            OffTable();
        }

        if (IsOnTable) {
            ShowName();
            Debug.Log(SelectedObject);
            if (Input.GetMouseButtonDown(0) && SelectedObject == null) {
                SelectObject();
                Debug.Log("Test");
            } else if (Input.GetMouseButtonDown(0) && SelectedObject != null) { 
                SelectedObject.transform.position = SelectedObjectPosition;
                SelectedObject = null;
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
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        IsOnTable = true;
        TableCollidor.enabled = false;
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
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        IsOnTable = false;
        TableCollidor.enabled = true;
        //GlobusUI.SetActive(false);
    }

    public void ShowName() {
        Ray ray = PlayerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.CompareTag("Interactable"))
            {
                NameText.text = hit.collider.gameObject.name;
                NameText.transform.position = Input.mousePosition + new Vector3(10, 10, 0);
            } else {
                NameText.text = "";
            }
        } else {
            NameText.text = "";
        }
    }

    void SelectObject() {
        Ray ray = PlayerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.name.StartsWith("Test Tube"))
            {
                Debug.Log("evtuizbuzibvtverguizb");
                SelectedObject = hit.collider.gameObject;
                SelectedObjectPosition = SelectedObject.transform.position;
                zPosition = PlayerCamera.WorldToScreenPoint(SelectedObject.transform.position).z;
                offset = SelectedObject.transform.position - GetMouseWorldPosition();
            }
        }
    }

    Vector3 GetMouseWorldPosition() {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = zPosition;
        return PlayerCamera.ScreenToWorldPoint(mousePosition);
    }

    void DragSelectedObject() {
        Vector3 mouseWorldPosition = GetMouseWorldPosition();
        SelectedObject.transform.position = mouseWorldPosition + offset;
    }
}