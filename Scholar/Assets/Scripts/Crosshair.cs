using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Crosshair : MonoBehaviour
{
    public float Size = 5.0f;
    public Texture EnemyTexture;
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
        RaycastHit hit;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var player = GameObject.Find("Player");
        var controller = player.GetComponent<RigidbodyFirstPersonController>();

        if (controller.enabled)
        {
            var overEnemy = false;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.name == "Zombie")
            {
                var zombie = hit.collider.gameObject.GetComponent<ZombieController>();

                if (zombie != null && !zombie.IsDead())
                {
                    overEnemy = true;
                }
            }

            GUI.DrawTexture(surface, overEnemy ? EnemyTexture : Texture);
        }
    }


}