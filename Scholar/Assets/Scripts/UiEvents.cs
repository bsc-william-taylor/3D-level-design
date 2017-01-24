using UnityEngine;
using UnityEngine.SceneManagement;

public class UiEvents : MonoBehaviour
{
    public Texture2D CursorTexture;
    public GameObject QuestLog;
    public bool ExitOnEscape = true;
    public bool MenuOnEscape = false;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (ExitOnEscape)
            {
                QuitApp();
            }

            if (MenuOnEscape)
            {
                SceneManager.LoadScene("Menu");
            }
        }

        // DEBUG CODE
        if (Input.GetKeyDown(KeyCode.L))
        {
            QuestLog.SetActive(!QuestLog.activeSelf);
        }
    }

    public void QuitApp()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level");
    }
}
