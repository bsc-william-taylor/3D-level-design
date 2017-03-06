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
        KillEnemies,
        Finished
    };

    public static bool StopUpdating = false;
    public Stages CurrentStage { get; set; }
    public ZombieController[] Enemies;

    public GameObject QuestLog;
    public GameObject LevelBar;

    private bool ignoreKeyboard = false;
    private bool playAlert = true;
    private Dictionary<Stages, StageInfo> stageState;
    private Dictionary<Stages, string> questLogs = new Dictionary<Stages, string>()
    {
        { Stages.FindCampsite,  "It is getting dark... There is a campsite down this road, I should rest there and continue my trip tomorrow." },
        { Stages.FindClues,     "No one is here? Something is seriously wrong, I should invistigate to find out what happened..." },
        { Stages.FindEnemies,   "Something has been killing travellers who use this campsite, I should find them!, Maybe that dirt path near the logs will lead me to them..."  },
        { Stages.KillEnemies,   "I have found the creatures who are numerous, they must be killed so travellers can once again move freely through this area."  }
    };

    private Dictionary<Stages, string> objectives = new Dictionary<Stages, string>()
    {
        { Stages.FindCampsite,  "Reach the Campsite" },
        { Stages.FindClues,     "Find Clues"    },
        { Stages.FindEnemies,   "Find The Culprits"  },
        { Stages.KillEnemies,   "Kill The Monsters"  },
        { Stages.Finished,      "Get The Loot"  }
    };

    void Start()
    {
        CurrentStage = Stages.FindCampsite;
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
        playAlert = true;
        ++CurrentStage;

        if(CurrentStage == Stages.KillEnemies)
        {
            foreach(var zombie in Enemies) 
            {
                zombie.Show();
            }
        }
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

        if (playAlert)
        {
            QuestLog.GetComponent<AudioSource>().Play();
            playAlert = false;
        }
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
