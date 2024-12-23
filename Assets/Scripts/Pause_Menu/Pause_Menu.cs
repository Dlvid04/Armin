using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject OptionMenuUI;
    public GameObject SteuerungMenuUI;
    public GameObject CrossHair;
    public bool isPaused = false;
    public AudioMixer audioMixer;
    public Volume Volume;
    private ColorAdjustments colorAdjustments;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !isPaused) {
            ActivateMenu();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            CrossHair.SetActive(false);
        } else if(Input.GetKeyDown(KeyCode.Escape) && isPaused) {
            DeactivateMenu();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            CrossHair.SetActive(true);
        }
    }
    void ActivateMenu()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
        pauseMenuUI.SetActive(true);
        isPaused = true;
    }
    void DeactivateMenu()
    {
        AudioListener.pause = false;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        OptionMenuUI.SetActive(false);
        SteuerungMenuUI.SetActive(false);
        isPaused = false;
    }

    public void Zurück(){
        pauseMenuUI.SetActive(true);
        OptionMenuUI.SetActive(false);
        SteuerungMenuUI.SetActive(false);
    }
    //---Menü---
    public void Beenden(){
        SceneManager.LoadScene("Menue");
        if (SceneManager.GetSceneByName("Menue").isLoaded) {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        }
    }
     public void Option(){
        pauseMenuUI.SetActive(false);
        OptionMenuUI.SetActive(true);
    }
     public void Steuerung(){
        pauseMenuUI.SetActive(false);
        SteuerungMenuUI.SetActive(true);
    }
    //---Menü---
    //---Einstellungen---
    public void SetVolume(float volume){
        audioMixer.SetFloat("Volume",volume);
    }

    public void SetFullScreen(bool isFullScreen){
        Screen.fullScreen = isFullScreen;
    }

    public void SetBrightness (float gamma){
        if(Volume.profile.TryGet<ColorAdjustments>(out colorAdjustments)){
            colorAdjustments.postExposure.value = gamma;
        }
    }
    //---Einstellungen---
}
