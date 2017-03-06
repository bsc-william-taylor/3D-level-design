using System;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class OpenableChest : MonoBehaviour
{
    private AudioSource SoundEffect;
    public int LootLowerBound;
    public int LootUpperBound;

    public string FinalChestMessage;
    public bool FinalChest;

    private bool lootTaken = false;

    void Start()
    {
        SoundEffect = GetComponent<AudioSource>();
    }

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
            SoundEffect.Play();
            lootTaken = true;

            var text = GameObject.Find("Gold").GetComponent<Text>();
            var currentGold = Convert.ToInt32(text.text);
            currentGold += Random.Range(LootLowerBound, LootUpperBound);
            text.text = currentGold.ToString();

            ActionService.PostAction(this, currentGold + " Gold Found", 3);

            if (FinalChest)
            {
                StartCoroutine(Player.Wait(3.0f, () => ActionService.PostAction(this, FinalChestMessage, 7)));
                StartCoroutine(Player.Wait(10.0f, () => SceneManager.LoadScene("Menu")));
            }
        }
    }
}
