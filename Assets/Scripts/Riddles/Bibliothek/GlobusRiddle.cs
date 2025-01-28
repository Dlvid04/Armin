using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobusRiddle : MonoBehaviour
{
    public Transform Globus,CameraHolder,CameraHolderPlayer;
    public IsLookingAt LA;
    public GameObject CrossHairUI,InventoryUI;
    public bool IsOnGlobus;
    public Player_Camera PCScript;
    public Player_Movement PMScript;
    public Camera PlayerCamera;
    public Texture2D idMap;

    void Update()
    {
        if (LA.LookingAt() != null && LA.LookingAt().name == "Globe" && Input.GetKeyDown("e") && IsOnGlobus == false) {
            OnGlobus();
        }else if (IsOnGlobus == true && Input.GetKeyDown("e")) {
            OffGlobus();
        }

        if (Input.GetMouseButton(0) && IsOnGlobus)
        {
            float horizontal = Input.GetAxis("Mouse X") * 200f * Time.deltaTime;
            float Vertical = Input.GetAxis("Mouse Y") * 200f * Time.deltaTime;
            transform.Rotate(Vector3.up, -horizontal, Space.World);
            transform.Rotate(Vector3.right, -Vertical, Space.Self);
        }

        if (Input.GetMouseButtonDown(0) && IsOnGlobus) {
            GetColor();
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
    }

    public void GetColor() {
        Ray ray = PlayerCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit)) {
                if(hit.transform.gameObject == gameObject){
                Renderer renderer = hit.collider.GetComponent<Renderer>();
                MeshCollider meshCollider = hit.collider as MeshCollider;
                if (renderer != null && meshCollider != null) {
                    Vector2 uv;
                    if (GetUVCoordinates(renderer, hit, out uv)) {
                        Color color = idMap.GetPixelBilinear(uv.x, uv.y);
                        Debug.Log(color);
                        string country = GetCountryFromColor(color);
                        Debug.Log("Angeklicktes Land: " + country);
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

    private string GetCountryFromColor(Color color) {
        if (color == Color.red) return "Deutschland";
        if (color == Color.blue) return "Brasilien";
        if (color == Color.magenta) return "Italien";
        if (color == Color.green) return "Indien";
        if (color == Color.cyan) return "Australien";
        if (color == new Color(1,1,0,1)) return "Russland";
        return "Unbekannt";
    }
}
