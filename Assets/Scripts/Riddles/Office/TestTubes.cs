using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTubes : MonoBehaviour
{
    public string elementName;
    public string zustand;
    public string k�rzel;

    public string ElementName { 
        get { return elementName; } set { elementName = value; }
    }

    public string Zustand { 
        get {return zustand; } set {  zustand = value; } 
    }

    public string K�rzel { 
        get { return k�rzel; } set { k�rzel = value; }
    }
}
