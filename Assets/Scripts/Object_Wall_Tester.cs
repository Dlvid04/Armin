using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectWallTester : MonoBehaviour
{
    public Transform Target;
    public float Distance = 1f;
    public LayerMask Mask;

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Distance, Mask))
        {
            Target.position = hit.point;
        }else{
            Target.position = transform.position + transform.forward * Distance;
        }
    }
}
