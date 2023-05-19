using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjList : MonoBehaviour
{
    public int StructureCounter = 0;

    public void StructureIncreased()
    {
        StructureCounter++;
        Debug.Log("Structure No. " + StructureCounter);
    }
}
