using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStage : MonoBehaviour
{
    public StateController StageController;
    public AudioClip BattleMusic;

    private bool changedMusic = false;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Player")
        {
            gameObject.SetActive(false);
            StageController.NextSection();

            if (name == "BattleTrigger" && !changedMusic)
            {
                var audioSource = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
                audioSource.clip = BattleMusic;
                audioSource.Play();
                changedMusic = true;
            }
        }
    }
}
