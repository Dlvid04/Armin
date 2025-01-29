using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class GlobusRiddle : MonoBehaviour {
    public Transform Globus, CameraHolder, CameraHolderPlayer;
    public IsLookingAt LA;
    public GameObject CrossHairUI, InventoryUI, PinNeedle, GlobusUI;
    public bool IsOnGlobus, isDragging;
    public Player_Camera PCScript;
    public Player_Movement PMScript;
    public Camera PlayerCamera;
    public Texture2D idMap;
    Vector3 lastMousePosition;
    float clickThreshold = 0.2f, mouseDownTime;
    public int anzahlAnPins = 0, Counter;
    public TextMeshProUGUI GlobusTxt;
    public List<Color> spielerEingabe = new List<Color>();
    public List<Color> r�tselL�sung = new List<Color>();

    void Start() {
        r�tselL�sung.Add(Color.red);
        r�tselL�sung.Add(Color.blue);
        r�tselL�sung.Add(Color.green);
    }

    void Update() {
        if (LA.LookingAt() != null && LA.LookingAt().name == "Globe" && Input.GetKeyDown("e") && IsOnGlobus == false) {
            OnGlobus();
        } else if (IsOnGlobus == true && Input.GetKeyDown("e")) {
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

    public void R�tselGel�st() {
        OffGlobus();
        tag = "Untagged";
        enabled = false;
    }

    public void R�tselAntwortTesten() {
        for (int i = 0; i < spielerEingabe.Count; i++) {
            if (r�tselL�sung.Contains(spielerEingabe[i]) && anzahlAnPins == 3) {
                Counter++;
            } else {
                Counter = 0;
                break;
            }
        }

        if (Counter == 3) {
            R�tselGel�st();
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
        float horizontal = Input.GetAxis("Mouse X") * 200f * Time.deltaTime;
        float vertical = Input.GetAxis("Mouse Y") * 200f * Time.deltaTime;


        transform.Rotate(Vector3.up, -horizontal, Space.World);
        transform.Rotate(Vector3.right, vertical, Space.Self);
    }

    void PlaceObject() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit) && anzahlAnPins < 4 && hit.collider.gameObject.name == "Globe") {
            Instantiate(PinNeedle, hit.point, Quaternion.identity).transform.SetParent(Globus);
            anzahlAnPins++;
            GlobusTxt.text = $"Zur�cksetzen\nGesetzte Pins: {anzahlAnPins}/4";
            GetColor();
            R�tselAntwortTesten();
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

    public void PinsZur�cksetzten (){
        Counter = 0;
        anzahlAnPins = 0;
        foreach (Transform child in transform) { 
            Destroy(child.gameObject);
        }
        GlobusTxt.text = $"Zur�cksetzen\nGesetzte Pins: {anzahlAnPins}/4";
        spielerEingabe.Clear();
    }
}
