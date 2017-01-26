using UnityEngine;

public class CursorShow : MonoBehaviour
{
    public static bool Block { get; set; }

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        if (Block)
            return;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
