using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using UnityEngine.Playables;

public class GlobusRiddle : MonoBehaviour {
    public Transform Globus, CameraHolder, CameraHolderPlayer;
    public IsLookingAt LA;
    public GameObject CrossHairUI, InventoryUI, PinNeedle, GlobusUI;
    public bool IsOnGlobus, isDragging,cutsceneBeendet;
    public Player_Camera PCScript;
    public Player_Movement PMScript;
    public Camera PlayerCamera;
    public Texture2D idMap;
    Vector3 lastMousePosition;
    float clickThreshold = 0.2f, mouseDownTime;
    public int anzahlAnPins = 0, Counter;
    public TextMeshProUGUI GlobusTxt;
    public List<Color> spielerEingabe = new List<Color>();
    public List<Color> rätselLösung = new List<Color>();
    public PlayableDirector PlayableDirector;
    public Animator Chandelier;

    void Start() {
        rätselLösung.Add(Color.red);
        rätselLösung.Add(Color.blue);
        rätselLösung.Add(Color.green);
    }

    void Update() {
        if (PlayableDirector.state == PlayState.Playing) {
            cutsceneBeendet = true;
        }
        if (PlayableDirector.state != PlayState.Playing && cutsceneBeendet) {
            PMScript.enabled = enabled;
            PCScript.enabled = enabled;
        }

        if (LA.LookingAt() != null && LA.LookingAt().name == "Globe" && Input.GetKeyDown(InputManager.Instance.Interact) && IsOnGlobus == false && !cutsceneBeendet) {
            OnGlobus();
        } else if (IsOnGlobus == true && Input.GetKeyDown(InputManager.Instance.Interact)) {
            OffGlobus();
        }

        if (Input.GetMouseButtonDown(0) && IsOnGlobus) {
            lastMousePosition = Input.mousePosition;
            isDragging = false;
            mouseDownTime = Time.deltaTime;
        }

        if (Input.GetMouseButton(0) && IsOnGlobus) {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            if (delta.magnitude > 5f) {
                isDragging = true;
                RotateObject();
            }
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0) && IsOnGlobus) {
            if (!isDragging && Time.deltaTime - mouseDownTime < clickThreshold) {
                PlaceObject();
            }
        }
    }


    public void RätselGelöst() {
        OffGlobus();
        tag = "Untagged";
        PlayableDirector.Play();
        PMScript.enabled = false;
        PCScript.enabled = false;
    }


    public void RätselAntwortTesten() {
        for (int i = 0; i < spielerEingabe.Count; i++) {
            if (rätselLösung.Contains(spielerEingabe[i]) && anzahlAnPins == 3) {
                Counter++;
            } else {
                Counter = 0;
                break;
            }
        }

        if (Counter == 3) {
            RätselGelöst();
        }
    }

    public void OnGlobus() {
        PlayerCamera.transform.SetParent(CameraHolder, true);
        CameraHolder.transform.GetChild(0).transform.localPosition = Vector3.zero;
        CameraHolder.transform.GetChild(0).transform.localScale = Vector3.one;
        CameraHolder.transform.GetChild(0).transform.localRotation = Quaternion.identity;
        PCScript.enabled = false;
        PMScript.enabled = false;
        CrossHairUI.SetActive(false);
        InventoryUI.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        IsOnGlobus = true;
        GlobusUI.SetActive(true);
    }

    public void OffGlobus() {
        PlayerCamera.transform.SetParent(CameraHolderPlayer, true);
        CameraHolderPlayer.GetChild(0).transform.localPosition = Vector3.zero;
        CameraHolderPlayer.GetChild(0).transform.localScale = Vector3.one;
        CameraHolderPlayer.GetChild(0).transform.localRotation = Quaternion.identity;
        PCScript.enabled = true;
        PMScript.enabled = true;
        CrossHairUI.SetActive(true);
        InventoryUI.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        IsOnGlobus = false;
        GlobusUI.SetActive(false);

    }

    void RotateObject() {
        float horizontal = Input.GetAxis("Mouse X") * 400f * Time.deltaTime;
        float vertical = Input.GetAxis("Mouse Y") * 400f * Time.deltaTime;
        transform.eulerAngles += Vector3.up * -horizontal;
        transform.eulerAngles += Vector3.forward * vertical;
    }

    void PlaceObject() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit) && anzahlAnPins < 4 && hit.collider.gameObject.name == "Globe") {
            Instantiate(PinNeedle, hit.point, Quaternion.identity).transform.SetParent(Globus);
            anzahlAnPins++;
            GlobusTxt.text = $"Zurücksetzen\nGesetzte Pins: {anzahlAnPins}/4";
            GetColor();
            RätselAntwortTesten();
        }
    }

    public void GetColor() {
        Ray ray = PlayerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit)) {
            if (hit.transform.gameObject == gameObject) {
                Renderer renderer = hit.collider.GetComponent<Renderer>();
                MeshCollider meshCollider = hit.collider as MeshCollider;
                if (renderer != null && meshCollider != null) {
                    Vector2 uv;
                    if (GetUVCoordinates(renderer, hit, out uv)) {
                        Color color = idMap.GetPixelBilinear(uv.x, uv.y);
                        Debug.Log(color);
                        GetCountryFromColor(color);
                    }
                }
            }
        }
    }

    private bool GetUVCoordinates(Renderer renderer, RaycastHit hit, out Vector2 uv) {
        uv = Vector2.zero;
        if (renderer is MeshRenderer meshRenderer) {
            MeshCollider collider = meshRenderer.GetComponent<MeshCollider>();
            if (collider != null) {
                Mesh mesh = collider.sharedMesh;
                int[] triangles = mesh.triangles;
                Vector3[] vertices = mesh.vertices;
                Vector2[] uvs = mesh.uv;

                int triangleIndex = hit.triangleIndex * 3;

                Vector3 p0 = vertices[triangles[triangleIndex + 0]];
                Vector3 p1 = vertices[triangles[triangleIndex + 1]];
                Vector3 p2 = vertices[triangles[triangleIndex + 2]];

                Vector2 uv0 = uvs[triangles[triangleIndex + 0]];
                Vector2 uv1 = uvs[triangles[triangleIndex + 1]];
                Vector2 uv2 = uvs[triangles[triangleIndex + 2]];

                Vector3 barycentricCoord = hit.barycentricCoordinate;
                uv = uv0 * barycentricCoord.x + uv1 * barycentricCoord.y + uv2 * barycentricCoord.z;
                return true;
            }
        }
        return false;
    }

    private void GetCountryFromColor(Color color) {
        if (color == Color.red) spielerEingabe.Add(Color.red);   //Deutschland
        else if (color == Color.blue) spielerEingabe.Add(Color.blue);    //Brasilien
        else if (color == Color.magenta) spielerEingabe.Add(Color.magenta);   //Italien
        else if (color == Color.green) spielerEingabe.Add(Color.green);  //Indien
        else if (color == Color.cyan) spielerEingabe.Add(Color.cyan);   //Australien
        else if (color == new Color(1, 1, 0, 1)) spielerEingabe.Add(new Color(1,1,0,1));  //Russland
        else spielerEingabe.Add(Color.white); //Daneben Geklickt
    }


    public void PinsZurücksetzten (){
        Counter = 0;
        anzahlAnPins = 0;
        foreach (Transform child in transform) { 
            Destroy(child.gameObject);
        }
        GlobusTxt.text = $"Zurücksetzen\nGesetzte Pins: {anzahlAnPins}/4";
        spielerEingabe.Clear();
    }
}
