using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {

        source.pitch = Input.GetKey(KeyCode.LeftShift) ? 1.6f : 1.0f;

        if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
        {
            source.Play();
        }
        else if (!Input.GetButton("Horizontal") && !Input.GetButton("Vertical") && source.isPlaying)
        {
            source.Stop();
        }
    }
}
