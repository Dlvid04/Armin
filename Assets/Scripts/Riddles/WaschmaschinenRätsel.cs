using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class WaschmaschinenRätsel : MonoBehaviour
{
    public TextMeshProUGUI AnzeigenText;
    public Transform Tür;
    int temperatur = 40;
    string schleudern = "An",vorwäsche = "An",schnellewäsche = "An";
    bool simonSaysGeschafft = false;

    void Update(){
        AnzeigenText.text = $"Temperatur: {temperatur}°C\nSchleudern: {schleudern}\nVor Wäsche: {vorwäsche}\nSchnellewäsche: {schnellewäsche}";
    }

    public void TemperaturÄndern(){
        if(temperatur <= 70){
            temperatur += 10;
        }else temperatur = 40;
    }

    public void SchleudernÄndern(){
        if(schleudern == "An"){
            schleudern = "Aus";
        }else schleudern = "An";
    }

    public void VorwäscheÄndern(){
        if(vorwäsche == "An"){
            vorwäsche = "Aus";
        }else vorwäsche = "An";
    }

    public void SchnellewäscheÄndern(){
        if(schnellewäsche == "An"){
            schnellewäsche = "Aus";
        }else schnellewäsche = "An";
    }

    public void Start(){
        if(temperatur == 60 && schleudern == "Aus" && vorwäsche == "An" && schnellewäsche == "An"){
            Tür.transform.localPosition = new Vector3(-1.6f,0.7f,-0.559f);
            Tür.transform.localRotation = Quaternion.Euler(0,90,0);
        }else Debug.Log("Nicht Geschafft");
    }
}
