using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{
    public GameObject CrossHairUI, Inventory;
    public Transform PaperReader,Draw;
    public IsLookingAt LA;
    public Camera PlayerCamera;
    public Player_Camera PCScript;
    public Player_Movement PMScript;
    bool ReadPaper;

    // Update is called once per frame
    void Update()
    {
        if (LA.LookingAt() != null && LA.LookingAt().name == "RiddlePaper" && Input.GetKeyDown("e") && ReadPaper == false) {
            Draw.GetChild(0).transform.SetParent(PaperReader);
            transform.localPosition = new Vector3(0f,0f,0f);
            transform.localRotation = Quaternion.Euler(90f,180f,0f);
            transform.localScale = new Vector3(0.03f, 2f, 0.035f);
            Camera.main.transform.localRotation = Quaternion.Euler(0f,0f,0f);
            ReadPaper = true;
            PCScript.enabled = false;
            PMScript.enabled = false;
            Inventory.SetActive(false);
            CrossHairUI.SetActive(false);
        } else if (ReadPaper == true && Input.GetKeyDown("e")) {
            PaperReader.GetChild(0).transform.SetParent(Draw);
            transform.localPosition = new Vector3(0f, 0f, 0f);
            transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            transform.localScale = new Vector3(0.03f, 2f, 0.035f);
            ReadPaper = false;
            PCScript.enabled = true;
            PMScript.enabled = true;
            Inventory.SetActive(true);
            CrossHairUI.SetActive(true);
        }
    }
}
