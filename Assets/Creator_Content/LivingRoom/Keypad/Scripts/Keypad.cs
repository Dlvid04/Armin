using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace NavKeypad
{
    public class Keypad : MonoBehaviour
    {
        public GameObject CrossHairUI, InventoryUI;
        public Player_Camera PCScript;
        public Player_Movement PMScript;
        public Transform CameraHolderPlayer, CameraHolderKeyPad;
        public IsLookingAt LA;
        public bool onKeyPad;

        [Header("Events")]
        [SerializeField] private UnityEvent onAccessGranted;
        [SerializeField] private UnityEvent onAccessDenied;
        [Header("Combination Code (9 Numbers Max)")]
        [SerializeField] private int keypadCombo;

        public UnityEvent OnAccessGranted => onAccessGranted;
        public UnityEvent OnAccessDenied => onAccessDenied;

        [Header("Settings")]
        [SerializeField] private string accessGrantedText = "Granted";
        [SerializeField] private string accessDeniedText = "Denied";

        [Header("Visuals")]
        [SerializeField] private float displayResultTime = 1f;
        [Range(0, 5)]
        [SerializeField] private float screenIntensity = 2.5f;
        [Header("Colors")]
        [SerializeField] private Color screenNormalColor = new Color(0.98f, 0.50f, 0.032f, 1f); //orangy
        [SerializeField] private Color screenDeniedColor = new Color(1f, 0f, 0f, 1f); //red
        [SerializeField] private Color screenGrantedColor = new Color(0f, 0.62f, 0.07f); //greenish
        [Header("SoundFx")]
        [SerializeField] private AudioClip buttonClickedSfx;
        [SerializeField] private AudioClip accessDeniedSfx;
        [SerializeField] private AudioClip accessGrantedSfx;
        [Header("Component References")]
        [SerializeField] private Renderer panelMesh;
        [SerializeField] private TMP_Text keypadDisplayText;
        [SerializeField] private AudioSource audioSource;


        private string currentInput;
        private bool displayingResult = false;
        private bool accessWasGranted = false;

        private void Awake()
        {
            ClearInput();
            panelMesh.material.SetVector("_EmissionColor", screenNormalColor * screenIntensity);
        }

        void Update() {
            if (LA.LookingAt() != null && LA.LookingAt().name == "KeyPad" && onKeyPad == false) {
                if (Input.GetKeyDown("e")) {
                    OnKeyPad();
                }
            } else if (onKeyPad == true && Input.GetKeyDown("e")) {
                OffKeyPad();
            }
        }

        public void OnKeyPad() {
            GetComponent<BoxCollider>().enabled = false;
            CameraHolderPlayer.GetChild(0).transform.SetParent(CameraHolderKeyPad, true);
            CameraHolderKeyPad.GetChild(0).transform.localPosition = Vector3.zero;
            CameraHolderKeyPad.GetChild(0).transform.localScale = Vector3.one;
            CameraHolderKeyPad.GetChild(0).transform.localRotation = Quaternion.identity;
            PCScript.enabled = false;
            PMScript.enabled = false;
            CrossHairUI.SetActive(false);
            InventoryUI.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            onKeyPad = true;
        }

        public void OffKeyPad() {
            GetComponent<BoxCollider>().enabled = true;
            CameraHolderKeyPad.GetChild(0).transform.SetParent(CameraHolderPlayer, true);
            CameraHolderPlayer.GetChild(0).transform.localPosition = Vector3.zero;
            CameraHolderPlayer.GetChild(0).transform.localScale = Vector3.one;
            CameraHolderPlayer.GetChild(0).transform.localRotation = Quaternion.identity;
            PCScript.enabled = true;
            PMScript.enabled = true;
            CrossHairUI.SetActive(true);
            InventoryUI.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            onKeyPad = false;
        }
        //Gets value from pressedbutton
        public void AddInput(string input)
        {
            audioSource.PlayOneShot(buttonClickedSfx);
            if (displayingResult || accessWasGranted) return;
            switch (input)
            {
                case "enter":
                    CheckCombo();
                    break;
                default:
                    if (currentInput != null && currentInput.Length == 9) // 9 max passcode size 
                    {
                        return;
                    }
                    currentInput += input;
                    keypadDisplayText.text = currentInput;
                    break;
            }

        }
        public void CheckCombo()
        {
            if (int.TryParse(currentInput, out var currentKombo))
            {
                bool granted = currentKombo == keypadCombo;
                if (!displayingResult)
                {
                    StartCoroutine(DisplayResultRoutine(granted));
                }
            }
            else
            {
                Debug.LogWarning("Couldn't process input for some reason..");
            }

        }

        //mainly for animations 
        private IEnumerator DisplayResultRoutine(bool granted)
        {
            displayingResult = true;

            if (granted) AccessGranted();
            else AccessDenied();

            yield return new WaitForSeconds(displayResultTime);
            displayingResult = false;
            if (granted) yield break;
            ClearInput();
            panelMesh.material.SetVector("_EmissionColor", screenNormalColor * screenIntensity);

        }

        private void AccessDenied()
        {
            keypadDisplayText.text = accessDeniedText;
            onAccessDenied?.Invoke();
            panelMesh.material.SetVector("_EmissionColor", screenDeniedColor * screenIntensity);
            audioSource.PlayOneShot(accessDeniedSfx);
        }

        private void ClearInput()
        {
            currentInput = "";
            keypadDisplayText.text = currentInput;
        }

        private void AccessGranted()
        {
            accessWasGranted = true;
            keypadDisplayText.text = accessGrantedText;
            onAccessGranted?.Invoke();
            panelMesh.material.SetVector("_EmissionColor", screenGrantedColor * screenIntensity);
            audioSource.PlayOneShot(accessGrantedSfx);
            OffKeyPad();
            this.GameObject().SetActive(false);
        }

    }
}