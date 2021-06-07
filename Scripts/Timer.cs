using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public static Timer Instance
    {
        get { return instance; }
    }
    private static Timer instance;

    public float timeLeft;
    public float timePerLevel = 5f;
    private float updateInterval = 1f;

    public AudioSource audioSource;
    public List<AudioClip> ticks;

    private bool startedTicking = false;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = timePerLevel;
        InvokeRepeating("UpdateText", 1, updateInterval);
    }

    // Update is called once per frame
    void Update()
    {
        if(timeLeft == 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //Time.timeScale = 0f;
            UIManager.Instance.resetButton.gameObject.SetActive(true);
        }
    }

    private void UpdateText()
    {
        timeLeft -= updateInterval;
        UIManager.Instance.UpdateTime((int)timeLeft);
        if(timeLeft >= 5 && timeLeft <= 10 && !startedTicking)
        {
            //Debug.Log("Tick tock");
            //int rngTickSound = Random.Range(1, ticks.Count);
            audioSource.PlayOneShot(ticks[0]);
        }
        else if(timeLeft < 5 && !startedTicking)
        {
            startedTicking = true;
            audioSource.PlayOneShot(ticks[1]);
        }
        else if(timeLeft > 10)
        {
            startedTicking = false;
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    private void AddTime(int timeAmount)
    {
        timeLeft += timeAmount;
    }
}
