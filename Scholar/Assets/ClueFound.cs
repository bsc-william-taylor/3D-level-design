using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueFound : MonoBehaviour
{
    private static int CluesFound = 0;

    void OnMouseDown()
    {
        CluesFound++;
        Debug.Log("Found: " + CluesFound);
    }

    void Update()
    {

    }
}
