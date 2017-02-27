using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using UnityEngine;

public class StateController : MonoBehaviour
{

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

    public static bool StopUpdating = false;
    public Stages CurrentStage = Stages.FindCampsite;

    public GameObject Player;
    public GameObject QuestLog;
    public GameObject LevelBar;

    private bool ignoreKeyboard = false;
    private Dictionary<Stages, StageInfo> stageState;
    private Dictionary<Stages, string> questLogs = new Dictionary<Stages, string>()
    {
        { Stages.FindCampsite,  "It is getting dark... I should follow this road to find a safe place sleep..." },
        { Stages.FindClues,     "No one is here? Something is wrong, I should invistigate..." },
        { Stages.FindEnemies,   "Something has been killing enemies who use this campsite, I should find them!"  },
        { Stages.KillEnemies,   "I have found the creatures who are numerous, they must be eliminated."  }
    };

    private Dictionary<Stages, string> objectives = new Dictionary<Stages, string>()
    {
        { Stages.FindCampsite,  "Reach the Campsite" },
        { Stages.FindClues,     "Find Clues"    },
        { Stages.FindEnemies,   "Find The Culprits"  },
        { Stages.KillEnemies,   "Kill The Monsters"  }
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
        ignoreKeyboard = true;
        ++CurrentStage;
    }

    void Update()
    {
        if (StopUpdating)
        {
            return;
        }

        var text = LevelBar.GetComponent<Text>();
        text.text = stageState[CurrentStage].objective;

        if (Input.GetKeyDown(KeyCode.Return) && !ignoreKeyboard)
        {
            var state = stageState[CurrentStage];
            state.read = true;
            stageState[CurrentStage] = state;
        }

        var show = !stageState[CurrentStage].read;

        FreezeCamera(show);
        QuestLog.SetActive(show);
        QuestLog.GetComponentsInChildren<Text>()[0].text = stageState[CurrentStage].questlog;
        ignoreKeyboard = false;
    }

    public static void FreezeCamera(bool freeze)
    {
        var player = GameObject.Find("Player");
        var contraints = RigidbodyConstraints.FreezeAll;
        var rigidbody = player.GetComponent<Rigidbody>();
        player.GetComponent<RigidbodyFirstPersonController>().enabled = !freeze;
        rigidbody.constraints = freeze ? contraints : RigidbodyConstraints.FreezeRotation;
    }
}
