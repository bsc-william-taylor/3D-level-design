using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Crosshair : MonoBehaviour
{
    public float Size = 5.0f;
    public Texture Texture;

    private Rect surface;

    void Start()
    {
        var crosshairLength = Screen.width * Size / 100.0f;
        var x = Screen.width / 2.0f - crosshairLength / 2.0f;
        var y = Screen.height / 2.0f - crosshairLength / 2.0f;

        surface = new Rect(x, y, crosshairLength, crosshairLength);
    }

    void OnGUI()
    {
        var player = GameObject.Find("Player");
        var controller = player.GetComponent<RigidbodyFirstPersonController>();

        if (controller.enabled)
        {
            GUI.DrawTexture(surface, Texture);
        }
    }
}