using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnCollision : MonoBehaviour
{
    public List<AudioClip> ballHitSounds;
    private AudioSource audioSource;
    private float maxVelocity = 4f;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        float intensity = GetComponent<Rigidbody>().velocity.magnitude / maxVelocity;
        intensity = intensity > 2f ? 2f : intensity;
        int soundNumber = Random.Range(1, ballHitSounds.Count);
        audioSource.PlayOneShot(ballHitSounds[soundNumber],intensity/2);
    }
}
