using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollector : MonoBehaviour
{
    public bool hasCube = false;
    

 

    public void ReceiveCube()
    {
        hasCube = true;
    }
}
