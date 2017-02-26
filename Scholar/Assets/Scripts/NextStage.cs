using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NextStage : MonoBehaviour
{
    public StateController StageController;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Player")
        {
            gameObject.SetActive(false);
            StageController.NextSection();
        }
    }
}
