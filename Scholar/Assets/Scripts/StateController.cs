using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using UnityEngine;

public class StateController : MonoBehaviour
{
    private string s = @"
        Here is one description
    ";

    public struct StageInfo
    {
        public string objective;
        public string questlog;
        public bool read;
    }

    public enum Stages 
    {
        FindCampsite,
        FindClues,
        FindEnemies,
        KillEnemies   
    };

    public Stages CurrentStage = Stages.FindCampsite;

    public GameObject Player;
    public GameObject QuestLog;
    public GameObject LevelBar;

    private Dictionary<Stages, StageInfo> stageState;
    private Dictionary<Stages, string> questLogs = new Dictionary<Stages, string>()
    {
        { Stages.FindCampsite,  "HelloWorld1" },
        { Stages.FindClues,     "HelloWorld2" },
        { Stages.FindEnemies,   "HelloWorld3"  },
        { Stages.KillEnemies,   "HelloWorld4"  }
    };

    private Dictionary<Stages, string> objectives = new Dictionary<Stages, string>() 
    {
        { Stages.FindCampsite,  "Find Campsite" },
        { Stages.FindClues,     "Find Clues"    },
        { Stages.FindEnemies,   "Find Enemies"  },
        { Stages.KillEnemies,   "Kill Enemies"  }
    };

    void Start()
    {
        Player = GameObject.Find("Player");
        stageState = new Dictionary<Stages, StageInfo>();

        for (Stages i = Stages.FindCampsite; i <= Stages.KillEnemies; i++)
        {
            stageState[i] = new StageInfo
            { 
                objective = objectives[i], 
                questlog = questLogs[i],
                read = false 
            };
        }
    }

    public void NextSection()
    {
        ++CurrentStage;
    }

    void Update()
    {
        var text = LevelBar.GetComponent<Text>();
        text.text = stageState[CurrentStage].objective;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            var state = stageState[CurrentStage];
            state.read = true;
            stageState[CurrentStage] = state;
        }

        var show = !stageState[CurrentStage].read;

        FreezeCamera(show);
        QuestLog.SetActive(show);
        QuestLog.GetComponentsInChildren<Text>()[0].text = stageState[CurrentStage].questlog;
    }

    private void FreezeCamera(bool freeze)
    {
        var contraints = RigidbodyConstraints.FreezeAll;
        var rigidbody = Player.GetComponent<Rigidbody>();
        Player.GetComponent<RigidbodyFirstPersonController>().enabled = !freeze;
        rigidbody.constraints = freeze ? contraints : RigidbodyConstraints.FreezeRotation;
    }
}
