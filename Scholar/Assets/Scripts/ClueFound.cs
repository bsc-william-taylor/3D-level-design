using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ClueFound : MonoBehaviour
{
    public StateController Controller;
    public GameObject QuestLog;

    private Dictionary<string, string> Plots = new Dictionary<string, string>()
    {
        { "Sword", "Here is a sword" },
        { "Tent", "Here is a tent" },
        { "Bonfire", "Here is a bonfire" }
    };
    private const int MaxClues = 3;
    private static int CluesFound = 0;
    private bool openedQuestLog;

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!openedQuestLog && CluesFound != MaxClues)
            {
                DisplayPlotDevelopment(gameObject.name);
                CluesFound++;
            }
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && openedQuestLog)
        {
            QuestLog.SetActive(false);
            StateController.StopUpdating = false;
            StateController.FreezeCamera(false);
            openedQuestLog = false;

            if(CluesFound == MaxClues)
            {
                Controller.NextSection();
                CluesFound = int.MaxValue;
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
