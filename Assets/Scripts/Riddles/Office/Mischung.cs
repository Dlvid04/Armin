using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mischung : MonoBehaviour
{
    public List<TestTubes> testTubes = new List<TestTubes>();

    public List<TestTubes> TestTubes { 
        get { return testTubes; } set { testTubes = value; }
    }

    public Mischung() { 
    
    }
}
