using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public GameObject Menue;

    public void StartGame() {
        SceneManager.LoadScene("Escape Insanity");
        Menue.SetActive(false);
    }

    public void EndGame() { 
        Application.Quit();
    }

}
