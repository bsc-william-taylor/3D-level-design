using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class VideoPlayer : MonoBehaviour, IPointerClickHandler
{
    public bool PlayOnLoad = false;
    public MovieTexture movTexture;

    private AudioSource audioSource;

    void Start()
    {
        TryLoad();
    }

    public void TryLoad()
    {
        var rawImage = GetComponent<RawImage>();
        audioSource = GetComponent<AudioSource>();

        if (rawImage != null && audioSource != null && movTexture != null)
        {
            movTexture.Stop();

            rawImage.texture = movTexture;
            audioSource.clip = movTexture.audioClip;

            if (PlayOnLoad)
            {
                Play();
            }
        }
        else
        {
            Debug.Log("Sources for video player are null");
        }
    }

    public void Play()
    {
        if (audioSource != null && movTexture != null)
        {
            audioSource.Play();
            movTexture.Play();

            StartCoroutine(WaitForMovie(movTexture));
        }
    }

    IEnumerator WaitForMovie(MovieTexture texture)
    {
        yield return new WaitForSeconds(texture.duration);

        movTexture.Stop();

        SceneManager.LoadScene(1);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Stop();
    }

    public void Stop()
    {
        if (!movTexture.isPlaying)
        {
            audioSource.Play();
            movTexture.Play();
        }
        else
        {
            audioSource.Pause();
            movTexture.Pause();
        }
    }
}