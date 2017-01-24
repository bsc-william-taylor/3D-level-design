using System;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class OpenableChest : MonoBehaviour
{
    public int LootLowerBound;
    public int LootUpperBound;

    private bool lootTaken = false;

    void OnMouseOver()
    {
        if (!lootTaken && ActionService.Text == string.Empty)
        {
            ActionService.Text = "Open Chest";
        }
    }

    void OnMouseExit()
    {
        if (!lootTaken && ActionService.Text == "Open Chest")
        {
            ActionService.Text = string.Empty;
        }
    }

    void Update()
    {
        if (lootTaken)
            return;

        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var lookingAt = Physics.Raycast(ray, out hit);

        if (Input.GetMouseButtonDown(1) && lookingAt && transform == hit.transform)
        {
            lootTaken = true;

            var text = GameObject.Find("Gold").GetComponent<Text>();
            var currentGold = Convert.ToInt32(text.text);
            currentGold += Random.Range(LootLowerBound, LootUpperBound);
            text.text = currentGold.ToString();

            ActionService.PostAction(this, currentGold + " Gold Found", 3);
        }
    }
}
