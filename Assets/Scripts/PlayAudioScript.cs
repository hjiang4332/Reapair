using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioScript : MonoBehaviour
{
    public AudioClip[] audioSources;
    public AudioSource randomSound;
    void Start()
    {
        CallAudio();
    }

    // Update is called once per frame
    void CallAudio()
    {
        Invoke("PlayRandom", 10);
    }

    void PlayRandom()
    {
        randomSound.clip = audioSources[Random.Range(0, audioSources.Length)];
        randomSound.Play();
    }
}
