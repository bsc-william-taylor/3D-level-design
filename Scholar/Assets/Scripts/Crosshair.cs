using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public float size = 5.0f;
    public Texture crosshairTexture;

    private Rect crosshairSurface;

    void Start()
    {
        var crosshairLength = Screen.width * size / 100.0f;

        crosshairSurface = new Rect(
            Screen.width / 2.0f - crosshairLength / 2.0f,
            Screen.height / 2.0f - crosshairLength / 2.0f,
            crosshairLength,
            crosshairLength);

    }

    void OnGUI()
    {
        GUI.DrawTexture(crosshairSurface, crosshairTexture);
    }
}