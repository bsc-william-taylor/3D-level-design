using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Assets.Scripts;

public class ClueFound : MonoBehaviour
{
    public StateController Controller;
    public GameObject QuestLog;

    private const int MaxClues = 3;
    private bool openedQuestLog = false;
    private static Dictionary<string, string> FoundClues = new Dictionary<string, string>();
    private Dictionary<string, string> Plots = new Dictionary<string, string>()
    {
        { "Sword", "Here is a sword" },
        { "Tent", "Here is a tent" },
        { "Bonfire", "Here is a bonfire" }
    };

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!FoundClues.ContainsKey(gameObject.name) && !openedQuestLog && FoundClues.Keys.Count != MaxClues)
            {
                DisplayPlotDevelopment(gameObject.name);
                FoundClues[gameObject.name] = string.Empty;
            }
        }

        if (ActionService.Text == string.Empty && !FoundClues.ContainsKey(gameObject.name))
        {
            ActionService.Text = "Discover Clue";
        }
    }

    void OnMouseExit()
    {
        if (ActionService.Text == "Discover Clue")
        {
            ActionService.Text = string.Empty;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && openedQuestLog)
        {
            QuestLog.SetActive(false);
            StateController.StopUpdating = false;
            StateController.FreezeCamera(false);
            openedQuestLog = false;

            ActionService.Text = string.Empty;

            if (FoundClues.Keys.Count == MaxClues)
            {
                Controller.NextSection();
                FoundClues.Add("", "");
            }
        }
    }

    void DisplayPlotDevelopment(string id)
    {
        StateController.StopUpdating = true;
        StateController.FreezeCamera(true);

        QuestLog.SetActive(true);
        QuestLog.GetComponentsInChildren<Text>()[0].text = Plots[id];

        openedQuestLog = true;
    }
}
