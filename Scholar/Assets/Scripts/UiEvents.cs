using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UiEvents : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
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
