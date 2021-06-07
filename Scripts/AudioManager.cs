using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance
    {
        get { return instance; }
    }

    public List<AudioClip> ballHitSounds;

    private AudioSource audioSource;
    private static AudioManager instance;

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource.Play();
    }

    public void PlayRandomBallHit()
    {
        int rngSound = Random.Range(1, ballHitSounds.Count);
        GetComponent<AudioSource>().PlayOneShot(ballHitSounds[rngSound]);

    }
}
