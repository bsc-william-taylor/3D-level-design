using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using Assets.Scripts;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public enum Spells { Fire, Heal, Light }
    public AudioClip[] SpellSoundEffects;
    public GameObject[] SpellObjects;
    public GameObject SpellViewer;
    public Sprite[] SpellIcons;

    private RigidbodyFirstPersonController controller;
    private Spells equipedSpell = Spells.Fire;
    private Image healthbar;
    private Image magicbar;

    void Start()
    {
        controller = GetComponent<RigidbodyFirstPersonController>();
        healthbar = GameObject.Find("HealthBar").GetComponent<Image>();
        magicbar = GameObject.Find("MagicBar").GetComponent<Image>();
    }

    void Update()
    {
        if (!IsDead())
        {
            UpdateSpellViewer();
            AddMagica(1);
        }
        else
        {
            controller.enabled = false;

            if (transform.eulerAngles.x != 360.0f - 90.0f)
            {
                transform.Rotate(new Vector3(-18.0f, 0.0f, 0.0f));
            }
            else
            {
                ActionService.PostAction(this, "You died... Thanks for playing the level!", 5);
                StartCoroutine(Wait(5.0f, () => SceneManager.LoadScene("Menu")));
            }
        }

        UpdateCastSpell();
    }

    public bool IsDead()
    {
        return healthbar.fillAmount == 0.0f;
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

    private void PlaySoundEffect()
    {
        var index = (int)equipedSpell;
        var source = GetComponent<AudioSource>();

        source.clip = SpellSoundEffects[index];
        source.Play();
    }

    private void UpdateCastSpell()
    {
        var index = (int)equipedSpell;
        var spell = SpellObjects[index];

        if (Input.GetMouseButtonDown(0))
        {
            var system = spell.GetComponent<ParticleSystem>();

            if (equipedSpell == Spells.Fire && GetMagic() >= 10)
            {
                RaycastHit hit;
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.name == "Zombie" && !spell.activeSelf)
                {
                    var zombie = hit.collider.gameObject.GetComponent<ZombieController>();

                    if (!zombie.IsDead())
                    {
                        var height = Math.Abs(hit.transform.lossyScale.y) / 2.0f;
                        var target = hit.transform.position;
                        spell.transform.position = target;
                        spell.transform.Translate(Vector3.up * height * 2, Space.World);

                        zombie.TakeDamage(100);

                        StartCoroutine(Wait(2, () => spell.SetActive(false)));
                        PlaySoundEffect();
                        UseMagica(25);
                    }
                }
                else
                {
                    return;
                }
            }
            else if (equipedSpell == Spells.Heal && GetMagic() >= 50)
            {
                StartCoroutine(Wait(4, () => spell.SetActive(false)));
                PlaySoundEffect();
                UseMagica(50);
                AddHealth(50);
            }
            else if (equipedSpell == Spells.Light && GetMagic() >= 50)
            {
                if (!spell.activeSelf)
                {
                    PlaySoundEffect();
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

        equipedSpell = (Spells)Mathf.Clamp((int)equipedSpell, 0, 2);
    }
}
