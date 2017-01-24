using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SceneController : MonoBehaviour
{
    public GameObject Spell;
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
        if (Input.GetMouseButtonDown(0) && !Spell.activeSelf)
        {
            var system = Spell.GetComponent<ParticleSystem>();
            system.Clear();
            system.Play();

            Spell.SetActive(true);

            StartCoroutine(Wait(2, () =>
            {
                Spell.SetActive(false);
            }));
        }
    }

    private void UpdateSpellViewer()
    {
        var spellImage = SpellViewer.GetComponent<Image>();
        spellImage.sprite = SpellIcons[(int)equipedSpell];

        if (Input.GetKeyDown(KeyCode.E))
        {
            equipedSpell++;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            equipedSpell--;
        }

        equipedSpell = (Spells)Mathf.Clamp((int)equipedSpell, (int)Spells.Fire, (int)Spells.Light);
    }

    static IEnumerator Wait(float seconds, Action callback)
    {
        yield return new WaitForSeconds(seconds);
        callback();
    }
}
