using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SceneController : MonoBehaviour
{
    public GameObject[] SpellObjects;
    public Sprite[] SpellIcons;
    public GameObject SpellViewer;

    enum Spells
    {
        Fire, 
        Heal, 
        Light
    }

    private Spells equipedSpell = Spells.Fire;

    void Start()
    {
    }

    void Update()
    {
        UpdateSpellViewer();
        UpdateCastSpell();
    }

    private void UpdateCastSpell()
    {
        var spell = SpellObjects[(int)equipedSpell];

        if (Input.GetMouseButtonDown(0))
        {
            if (equipedSpell == Spells.Fire)
            {
                RaycastHit hit;
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    var height = Math.Abs(hit.transform.lossyScale.y)/2.0f;
                    var target = hit.transform.position;
                    spell.transform.position = target;
                    spell.transform.Translate(Vector3.up * height, Space.World);
                }

                StartCoroutine(Wait(2, () => spell.SetActive(false)));
            }

            if (equipedSpell == Spells.Heal)
            {
                StartCoroutine(Wait(4, () => spell.SetActive(false)));
            }

            var system = spell.GetComponent<ParticleSystem>();
            system.Clear();
          
            if (!spell.activeSelf)
            {
                system.Play();
            }

            spell.SetActive(!spell.activeSelf);
        }
    }

    private void UpdateSpellViewer()
    {
        var spell = SpellObjects[(int)equipedSpell];
        var spellImage = SpellViewer.GetComponent<Image>();
        spellImage.sprite = SpellIcons[(int)equipedSpell];

        if (Input.GetKeyDown(KeyCode.E))
        {
            spell.SetActive(false);
            equipedSpell++;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            spell.SetActive(false);
            equipedSpell--;
        }

        equipedSpell = (Spells)Mathf.Clamp((int)equipedSpell, (int)Spells.Fire, (int)Spells.Light);
    }

    public static IEnumerator Wait(float seconds, Action callback)
    {
        yield return new WaitForSeconds(seconds);
        callback();
    }
}
