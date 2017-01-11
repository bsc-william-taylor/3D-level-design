using System;
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public GameObject Spell;

    void Update()
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

    static IEnumerator Wait(float seconds, Action callback)
    {
        yield return new WaitForSeconds(seconds);
        callback();
    }
}
