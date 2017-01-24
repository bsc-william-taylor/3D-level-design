using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorShow : MonoBehaviour
{
    public static bool Block { get; set; }
    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Block)
            return;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
