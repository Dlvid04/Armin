using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{

    public void StartGame() 
    {
        SceneManager.LoadScene("Armin");
    }

    public void EndGame() 
    { 
        Application.Quit();
    }

}
