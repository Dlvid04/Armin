using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTubes : MonoBehaviour
{
    public string elementName;
    public string zustand;
    public string kürzel;

    public string ElementName { 
        get { return elementName; } set { elementName = value; }
    }

    public string Zustand { 
        get {return zustand; } set {  zustand = value; } 
    }

    public string Kürzel { 
        get { return kürzel; } set { kürzel = value; }
    }
}
