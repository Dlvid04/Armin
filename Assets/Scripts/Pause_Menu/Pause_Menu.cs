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
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI,OptionMenuUI,SteuerungMenuUI,CrossHair;
    public bool isPaused = false;
    public AudioMixer audioMixer;
    public Volume Volume;
    ColorAdjustments colorAdjustments;
    float gamma;
    const string VolumePrefKey = "Volume",GammaPrefKey = "Gamma";
    public Laptop LPScript;
    public Slider VolumeSlider,BrightnessSlider;


    void Start(){
        float savedGamma = PlayerPrefs.GetFloat(GammaPrefKey, 0f); // Standardwert: 0
        SetBrightness(savedGamma);
        BrightnessSlider.value = savedGamma;
        float savedVolume = PlayerPrefs.GetFloat(VolumePrefKey, 0f); // Standardwert: 0
        SetVolume(savedVolume);
        VolumeSlider.value = savedVolume;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !isPaused) {
            ActivateMenu();
        } else if(Input.GetKeyDown(KeyCode.Escape) && isPaused) {
            DeactivateMenu();
        }
    }
    void ActivateMenu()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
        pauseMenuUI.SetActive(true);
        isPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        CrossHair.SetActive(false);
    }
    public void DeactivateMenu()
    {
        AudioListener.pause = false;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        OptionMenuUI.SetActive(false);
        SteuerungMenuUI.SetActive(false);
        isPaused = false;
        if(LPScript.IsOnLaptop == false || LPScript == null){
            CrossHair.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
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
        audioMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat(VolumePrefKey, volume);
        PlayerPrefs.Save();
        if (VolumeSlider != null)
        {
            VolumeSlider.value = volume;
        }
    }

    public void SetFullScreen(bool isFullScreen){
        Screen.fullScreen = isFullScreen;
    }

    public void SetBrightness (float gamma){
        this.gamma = gamma;
        PlayerPrefs.SetFloat("Gamma", gamma);
        if (Volume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
        {
            colorAdjustments.postExposure.value = gamma;
        }
    }
    //---Einstellungen---


}
