using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Obsolete class
    private Vector3 playerInitialPosition;
    [SerializeField]
    private GameObject playerBallPrefab;
    public GameObject firstBall;
    public TMPro.TextMeshProUGUI scoreText;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private float score;

    private static GameManager instance;

    private void Awake()
    {
        instance = this;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Start()
    {
        score = 0;
        scoreText.text = "Score: 0";
    }

    public void AddPointsToScore(float points)
    {
        score = score + points;
        scoreText.text = "Score: " + (int)score;
    }

    public void AddNewBall()
    {
        //GameObject ball = Instantiate(playerBallPrefab, playerInitialPosition, Quaternion.identity);
        //CameraManager.Instance.SetFollowingCamNewTarget(ball);
    }
}
