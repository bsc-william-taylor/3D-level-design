using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ClueFound : MonoBehaviour
{
    public StateController Controller;
    public GameObject QuestLog;

    private const int MaxClues = 2;
    private static int CluesFound = 0;
    private bool openedQuestLog;

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!openedQuestLog)
            {
                CluesFound++;
                DisplayPlotDevelopment(this.gameObject.name);
            }
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && openedQuestLog)
        {
            StateController.StopUpdating = false;
            StateController.FreezeCamera(false);
            QuestLog.SetActive(false);
        }

        if(CluesFound == MaxClues)
        {
            Controller.NextSection();
            CluesFound = int.MaxValue;
        }
    }

    void DisplayPlotDevelopment(string message)
    {
        StateController.StopUpdating = true;
        StateController.FreezeCamera(true);

        QuestLog.SetActive(true);
        QuestLog.GetComponentsInChildren<Text>()[0].text = message;
        
        openedQuestLog = true;
    }
}
