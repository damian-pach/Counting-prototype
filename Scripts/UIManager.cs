using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static UIManager instance;
    public TextMeshProUGUI pointsToAdd;
    public TextMeshProUGUI level;
    public TextMeshProUGUI timer;
    public Button resetButton;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        pointsToAdd.enabled = false;
    }

    public void UpdateTime(int timeToShow)
    {
        timer.text = timeToShow.ToString();
    }

    public void ShowNewPoints(int pointsAmount)
    {
        pointsToAdd.enabled = true;
        pointsToAdd.text = "+" + pointsAmount + " pts";
        pointsToAdd.gameObject.GetComponent<Animator>().SetTrigger("CanFade");
    }

    public void ShowNewLevel()
    {
        level.GetComponent<Animator>().SetTrigger("LevelUp");
    }

    public void ResetGame()
    {
        Time.timeScale = 1f;
        GameManager_New.Instance.ResetScene();
    }

    public void DisplayNewLevel(int levelNumber)
    {
        level.text = "Lv " + levelNumber;
    }

    //public void AddTimeScore()
    //{
    //    TextMeshProUGUI timerPoints = timer;
    //    timerPoints.transform.position -= Vector3.down * 15f;
    //    timerPoints.text += " * " + GameManager_New.Instance.levelNumber * 10;
    //    int pointsToAdd = (int)GameManager_New.Instance.score + (int)Timer.Instance.timeLeft * (int)GameManager_New.Instance.levelNumber * 10;
    //    GameManager_New.Instance.scoreText.text = pointsToAdd.ToString();
    //}
}
