using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLevelSound : MonoBehaviour
{
    public AudioClip newLevelSound;
    public AudioSource newLevelAudioSource;

    // Start is called before the first frame update
    void OnEnable()
    {
        newLevelAudioSource.PlayOneShot(newLevelSound);
        GameManager_New.Instance.UpdateButtonGlow();
        UIManager.Instance.DisplayNewLevel(GameManager_New.Instance.levelNumber);
    }

}
