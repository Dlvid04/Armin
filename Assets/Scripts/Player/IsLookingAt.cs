using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IsLookingAt : MonoBehaviour
{
    public float rayLength = 4f;
    // Update is called once per frame
    public GameObject LookingAt(){
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);
        if (Physics.Raycast(ray, out hit, rayLength)) 
        {
            return hit.collider?.gameObject;
        }else return null;
    }
}
