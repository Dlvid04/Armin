using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WaschmaschinenRätsel : MonoBehaviour
{
    public GameObject CrossHairUI,InventoryUI,WaschmaschinenUI,GoldKey,Waschmaschine;
    public TextMeshProUGUI AnzeigenText,RundenCounterText;
    public Transform Tür,Zeiger,CameraHolderPlayer,CameraHolderWaschmaschine;
    public Button BlueButton,RedButton,GreenButton,YellowButton,PurpleButton;
    public Image SimonSaysImage;
    public Slider RundenSlider;
    public IsLookingAt LA;
    public Camera PlayerCamera;
    public Player_Camera PCScript;
    public Player_Movement PMScript;
    public int temperatur = 40, x = 1, runde = 1;
    string schleudern = "An",vorwäsche = "An",schnellewäsche = "An";
    public bool simonSaysGeschafft,isOnWaschmaschine;
    List<int> userInput = new List<int>();
    List<int> simonsaysRundenInput = new List<int>();

    void Start(){
        BlueButton.GetComponent<Button>().enabled = false;
        RedButton.GetComponent<Button>().enabled = false;
        GreenButton.GetComponent<Button>().enabled = false;
        YellowButton.GetComponent<Button>().enabled = false;
        RedButton.GetComponent<Button>().enabled = false;
        AnzeigenTextAusgeben();
    }
    void Update(){
        if(LA.LookingAt() != null &&  LA.LookingAt().name == "Waschmaschine" && isOnWaschmaschine == false){
            if (Input.GetKeyDown("e")){
                WaschmaschineAn();
            }
        }else if(isOnWaschmaschine == true && Input.GetKeyDown("e")){
            WaschmaschineAus();
        }
    }

    public void WaschmaschineAus(){
        PlayerCamera.transform.SetParent(CameraHolderPlayer,true);
        CameraHolderPlayer.GetChild(0).transform.localPosition = Vector3.zero;
        CameraHolderPlayer.GetChild(0).transform.localScale = Vector3.one;
        CameraHolderPlayer.GetChild(0).transform.localRotation = Quaternion.identity;
        PCScript.enabled = true;
        PMScript.enabled = true;
        CrossHairUI.SetActive(true);
        InventoryUI.SetActive(true);
        WaschmaschinenUI.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isOnWaschmaschine = false;
    }
    public void WaschmaschineAn(){
        PlayerCamera.transform.SetParent(CameraHolderWaschmaschine,true);
        CameraHolderWaschmaschine.GetChild(0).transform.localPosition = Vector3.zero;
        CameraHolderWaschmaschine.GetChild(0).transform.localScale = Vector3.one;
        CameraHolderWaschmaschine.GetChild(0).transform.localRotation = Quaternion.identity;
        PCScript.enabled = false;
        PMScript.enabled = false;
        CrossHairUI.SetActive(false);
        InventoryUI.SetActive(false);
        WaschmaschinenUI.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        isOnWaschmaschine = true;
    }

    public void AnzeigenTextAusgeben(){
        AnzeigenText.text = $"Temperatur: {temperatur}°C\nSchleudern: {schleudern}\nVor Wäsche: {vorwäsche}\nSchnellewäsche: {schnellewäsche}";
    }

    public void TemperaturÄndern(){
        if(temperatur <= 70){
            temperatur += 10;
            AnzeigenTextAusgeben();
        }else temperatur = 40; AnzeigenTextAusgeben();
    }

    public void SchleudernÄndern(){
        if(schleudern == "An"){
            schleudern = "Aus";
            AnzeigenTextAusgeben();
        }else schleudern = "An"; AnzeigenTextAusgeben();
    }

    public void VorwäscheÄndern(){
        if(vorwäsche == "An"){
            vorwäsche = "Aus";
            AnzeigenTextAusgeben();
        }else vorwäsche = "An"; AnzeigenTextAusgeben();
    }

    public void SchnellewäscheÄndern(){
        if(schnellewäsche == "An"){
            schnellewäsche = "Aus";
            AnzeigenTextAusgeben();
        }else schnellewäsche = "An"; AnzeigenTextAusgeben();
    }

    public void StartKorrektur(){
        if(temperatur == 60 && schleudern == "Aus" && vorwäsche == "Aus" && schnellewäsche == "An" && simonSaysGeschafft == true && Zeiger.localRotation == Quaternion.Euler(0, 0, -290)) {
            Tür.transform.localPosition = new Vector3(-1.6f,0.7f,-0.559f);
            Tür.transform.localRotation = Quaternion.Euler(0,90,0);
            GoldKey.transform.localPosition = new Vector3(-1.6f,0.77f,-0.7f);
            WaschmaschineAus();
            Waschmaschine.tag = tag.Normalize();
            enabled = false;
        }
    }    
    public void ZeigerÄndernHoch(){
        if(x == 14){
            x = 1;
            ZeigerÄndern();
        }else x++;
        ZeigerÄndern();
    }

    public void ZeigerÄndernRunter(){
        if(x == 1){
            x = 14;
            ZeigerÄndern();
        }else x--;
        ZeigerÄndern();
    }

    public void ZeigerÄndern(){
        if(x == 1){
            Zeiger.localRotation = Quaternion.Euler(0, 0, -32);
        }else if(x == 2){
            Zeiger.localRotation = Quaternion.Euler(0, 0, -53);
        }else if(x == 3){
            Zeiger.localRotation = Quaternion.Euler(0, 0, -71);
        }else if(x == 4){
            Zeiger.localRotation = Quaternion.Euler(0, 0, -90);
        }else if(x == 5){
            Zeiger.localRotation = Quaternion.Euler(0, 0, -105);
        }else if(x == 6){
            Zeiger.localRotation = Quaternion.Euler(0, 0, -122);
        }else if(x == 7){
            Zeiger.localRotation = Quaternion.Euler(0, 0, -140);
        }else if(x == 8){
            Zeiger.localRotation = Quaternion.Euler(0, 0, -215);
        }else if(x == 9){
            Zeiger.localRotation = Quaternion.Euler(0, 0, -235);
        }else if(x == 10){
            Zeiger.localRotation = Quaternion.Euler(0, 0, -254);
        }else if(x == 11){
            Zeiger.localRotation = Quaternion.Euler(0, 0, -270);
        }else if(x == 12){
            Zeiger.localRotation = Quaternion.Euler(0, 0, -290);
        }else if(x == 13){
            Zeiger.localRotation = Quaternion.Euler(0, 0, -310);
        }else if(x == 14){
            Zeiger.localRotation = Quaternion.Euler(0, 0, -332);
        }
    }
    public void StartSimonSays() {
        simonsaysRundenInput.Clear();
        userInput.Clear();
        SimonSaysScreen();

        if (runde == 1) {
            simonsaysRundenInput.Add(0);  // Blau
            simonsaysRundenInput.Add(1);  // Rot
            simonsaysRundenInput.Add(4);  // Lila
        } else if (runde == 2) {
            simonsaysRundenInput.Add(1);  // Rot
            simonsaysRundenInput.Add(2);  // Grün
            simonsaysRundenInput.Add(4);  // Lila
            simonsaysRundenInput.Add(3);  // Gelb
        } else if (runde == 3) {
            simonsaysRundenInput.Add(4);  // Lila
            simonsaysRundenInput.Add(0);  // Blau
            simonsaysRundenInput.Add(3);  // Gelb
            simonsaysRundenInput.Add(1);  // Rot
            simonsaysRundenInput.Add(2);  // Grün
        }
    }

    public void SimonSaysEingabe(){
        BlueButton.GetComponent<Button>().enabled = true;
        RedButton.GetComponent<Button>().enabled = true;
        GreenButton.GetComponent<Button>().enabled = true;
        YellowButton.GetComponent<Button>().enabled = true;
        PurpleButton.GetComponent<Button>().enabled = true;
    }

    public void SimonSaysScreen(){
        if(runde == 1){     
            BlueButton.GetComponent<Button>().enabled = false;
            RedButton.GetComponent<Button>().enabled = false;
            GreenButton.GetComponent<Button>().enabled = false;
            YellowButton.GetComponent<Button>().enabled = false;
            PurpleButton.GetComponent<Button>().enabled = false;
            Invoke(nameof(ChangeColorToBlue),0.5f);
            Invoke(nameof(ChangeColorToWhite), 1.5f);
            Invoke(nameof(ChangeColorToRed), 2.5f);
            Invoke(nameof(ChangeColorToWhite), 3.5f);
            Invoke(nameof(ChangeColorToPurple), 4.5f);
            Invoke(nameof(ChangeColorToBlack), 5.5f);
            Invoke(nameof(SimonSaysEingabe),5.4f);
        }else if(runde == 2){
            BlueButton.GetComponent<Button>().enabled = false;
            RedButton.GetComponent<Button>().enabled = false;
            GreenButton.GetComponent<Button>().enabled = false;
            YellowButton.GetComponent<Button>().enabled = false;
            PurpleButton.GetComponent<Button>().enabled = false;
            Invoke(nameof(ChangeColorToRed),0.5f);
            Invoke(nameof(ChangeColorToWhite), 1.5f);
            Invoke(nameof(ChangeColorToGreen), 2.5f);
            Invoke(nameof(ChangeColorToWhite), 3.5f);
            Invoke(nameof(ChangeColorToPurple), 4.5f);
            Invoke(nameof(ChangeColorToWhite), 5.5f);
            Invoke(nameof(ChangeColorToYellow), 6.5f);
            Invoke(nameof(ChangeColorToBlack), 7.5f);
            Invoke(nameof(SimonSaysEingabe),7.4f);
        }else if(runde == 3){
            BlueButton.GetComponent<Button>().enabled = false;
            RedButton.GetComponent<Button>().enabled = false;
            GreenButton.GetComponent<Button>().enabled = false;
            YellowButton.GetComponent<Button>().enabled = false;
            PurpleButton.GetComponent<Button>().enabled = false;
            Invoke(nameof(ChangeColorToPurple),0.5f);
            Invoke(nameof(ChangeColorToWhite), 1.5f);
            Invoke(nameof(ChangeColorToBlue), 2.5f);
            Invoke(nameof(ChangeColorToWhite), 3.5f);
            Invoke(nameof(ChangeColorToYellow), 4.5f);
            Invoke(nameof(ChangeColorToWhite), 5.5f);
            Invoke(nameof(ChangeColorToRed), 6.5f);
            Invoke(nameof(ChangeColorToWhite), 7.5f);
            Invoke(nameof(ChangeColorToGreen), 8.5f);
            Invoke(nameof(ChangeColorToBlack), 9.5f);
            Invoke(nameof(SimonSaysEingabe),9.4f);
        }
    }

    void ChangeColorToBlue() {
        SimonSaysImage.color = new Color(0f, 255f, 255f);
    }
    void ChangeColorToRed() {
        SimonSaysImage.color = new Color(255f, 0f, 0f);
    }
    void ChangeColorToGreen() {
        SimonSaysImage.color = new Color(0f, 255f, 0f);
    }
    void ChangeColorToYellow() {
        SimonSaysImage.color = new Color(255f, 255f, 0f);
    }
    void ChangeColorToPurple() {
        SimonSaysImage.color = new Color(255f, 0f, 255f);
    }
    void ChangeColorToWhite() {
        SimonSaysImage.color = new Color(255f, 255f, 255f);
    }
    void ChangeColorToBlack() {
        SimonSaysImage.color = new Color(0f, 0f, 0f);
    }

    public void BlueButtonPressed(){
        userInput.Add(0);
        CheckSimonSaysInput();
    }
    public void RedButtonPressed(){
        userInput.Add(1);
        CheckSimonSaysInput();
    }
    public void GreenButtonPressed(){
        userInput.Add(2);
        CheckSimonSaysInput();
    }
    public void YellowButtonPressed(){
        userInput.Add(3);
        CheckSimonSaysInput();
    }
    public void PurpleButtonPressed(){
        userInput.Add(4);
        CheckSimonSaysInput();
    }

    public void CheckSimonSaysInput(){
        for (int i = 0; i < userInput.Count; i++){
            if (userInput[i] != simonsaysRundenInput[i]){
                userInput.Clear();
                //Hier Sound Abspielen wenn spieler falsche eingabe gemacht hat
                return;
            }
        }

        if (userInput.Count == simonsaysRundenInput.Count){
            runde++;
            RundenCounterText.text = $"Runde {runde}/3";
            RundenSlider.value = runde - 1;
            userInput.Clear();
            simonsaysRundenInput.Clear();
            if (runde > 3){
                simonSaysGeschafft = true;
                RundenCounterText.text = $"Waschmittel Eingefügt";
                RundenCounterText.rectTransform.localPosition = new Vector3(0,15,0);
                BlueButton.GetComponent<Button>().enabled = false;
                RedButton.GetComponent<Button>().enabled = false;
                GreenButton.GetComponent<Button>().enabled = false;
                YellowButton.GetComponent<Button>().enabled = false;
                PurpleButton.GetComponent<Button>().enabled = false;
                return;
            } else {
                StartSimonSays();
            }
        }
    }
}