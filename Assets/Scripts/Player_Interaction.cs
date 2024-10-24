using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
public bool Animation;
public bool isTrigger;
public bool Activat;
public GameObject[] activateObject;
public Animator ani;
public string ani_parameter;
public bool action;



    // Update is called once per frame
    void Update()
    {
        if(action)
        {
            action = false;

            if(Animation)
            {
                if(isTrigger)ani.SetTrigger(ani_parameter);
                else
                {
                    var t = ani.GetBool(ani_parameter);
                    ani.SetBool(ani_parameter, !t);
                }
            }
            else if(Activat)
            {
                for(int i = 0; i < activateObject.Length; i++)
                {
                    activateObject[i].SetActive(!activateObject[i].activeInHierarchy);
                }
            }
        }
        
    }
}
