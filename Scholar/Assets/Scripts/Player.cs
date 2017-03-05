using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Player : MonoBehaviour
{
    public enum Spells { Fire, Heal, Light }
    public GameObject[] SpellObjects;
    public GameObject SpellViewer;
    public Sprite[] SpellIcons;

    private Spells equipedSpell = Spells.Fire;
    private Image healthbar;
    private Image magicbar;

    void Start()
    {
        healthbar = GameObject.Find("HealthBar").GetComponent<Image>();
        magicbar = GameObject.Find("MagicBar").GetComponent<Image>();
    }

    void Update()
    {
        UpdateSpellViewer();
        UpdateCastSpell();
        AddMagica(1);
    }

    public int GetHealth()
    {
        return (int)(healthbar.fillAmount * 100.0f);
    }

    public int GetMagic()
    {
        return (int)(magicbar.fillAmount * 100.0f);
    }

    public void TakeDamage(int damage)
    {
        healthbar.fillAmount -= (damage / 100.0f);
    }

    public void AddHealth(int health)
    {
        healthbar.fillAmount += (health / 100.0f);
    }

    public void UseMagica(int magic)
    {
        magicbar.fillAmount -= (magic / 100.0f);
    }

    public void AddMagica(int magic) 
    {
        magicbar.fillAmount += (magic / 100.0f);
    }

    public static IEnumerator Wait(float seconds, Action callback)
    {
        yield return new WaitForSeconds(seconds);
        callback();
    }

    private void UpdateCastSpell()
    {
        var spell = SpellObjects[(int)equipedSpell];

        if (Input.GetMouseButtonDown(0))
        {
            var system = spell.GetComponent<ParticleSystem>();

            if (equipedSpell == Spells.Fire && GetMagic() >= 10)
            {
                RaycastHit hit;
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    var height = Math.Abs(hit.transform.lossyScale.y) / 2.0f;
                    var target = hit.transform.position;
                    spell.transform.position = target;
                    spell.transform.Translate(Vector3.up * height, Space.World);
                }

                StartCoroutine(Wait(2, () => spell.SetActive(false)));
                UseMagica(10);
            }
            else if (equipedSpell == Spells.Heal && GetMagic() >= 50)
            {
                StartCoroutine(Wait(4, () => spell.SetActive(false)));
                UseMagica(50);
                AddHealth(50);
            }
            else if (equipedSpell == Spells.Light && GetMagic() >= 50)
            {
                if (!spell.activeSelf)
                {
                    UseMagica(50);
                }
            }
            else 
            {
                return;
            }

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
}
