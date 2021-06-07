using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_New : MonoBehaviour
{

    #region Fields

    public static GameManager_New Instance
    {
        get { return instance; }
    }
    private static GameManager_New instance;

    public TMPro.TextMeshProUGUI scoreText;
    public float score;

    public GameObject playerBallPrefab;
    public GameObject SmallTargetPrefab;
    public GameObject MediumTargetPrefab;

    public List<GameObject> ballPositions;
    public List<GameObject> smallTargetPositions;
    public List<GameObject> mediumTargetsPositions;

    public List<GameObject> ActiveBalls;
    public List<GameObject> ActiveTargets;
    public List<GameObject> LevelButtons;

    public int levelNumber;
    public float levelScaleFactor = 0.714f;
    public float newLevelDelay = 2f;
    public int secondToAddPerLevel = 20;

    public AudioSource audioSource;
    [ColorUsage(true, true)]
    public Color buttonGlow;
    [ColorUsage(true, true)]
    public Color defaultButtonGlow;

    private bool startedNewLevel = false;

    #endregion  

    private void Awake()
    {
        instance = this;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        levelNumber = 1;
    }

    void Start()
    {
        score = 0;
        scoreText.text = "Score: 0";
        InstantiateTargetsAndBalls(1, 1, 0);
        UpdateButtonGlow();
    }

    void Update()
    {
        if (ActiveBalls.Count == 0 && !startedNewLevel)
        {
            SetNewLevel();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            levelNumber++;
            UpdateButtonGlow();
        }
    }

    private void SetNewLevel()
    {
        startedNewLevel = true;

        foreach(GameObject target in ActiveTargets)
        {
            target.GetComponent<Animator>().SetTrigger("Anihilation");
            target.GetComponent<AudioSource>().PlayOneShot(target.GetComponent<AudioSource>().clip);
        }

        levelNumber++;
        StartCoroutine(SetNewLevelCoroutine(newLevelDelay));
    }

    IEnumerator SetNewLevelCoroutine(float secondsDelay)
    {
        yield return new WaitForSeconds(secondsDelay);

        foreach (GameObject target in ActiveTargets)
        {
            Destroy(target);
        }

        ActiveTargets.Clear();

        int itemsOnNextLevel = (int)Mathf.Ceil(levelNumber * levelScaleFactor);
        InstantiateTargetsAndBalls(itemsOnNextLevel * 2, itemsOnNextLevel, itemsOnNextLevel);
        UIManager.Instance.ShowNewLevel();
        Timer.Instance.timeLeft += secondToAddPerLevel;
        //UpdateButtonGlow();
        startedNewLevel = false;
    }

    public void AddPointsToScore(float points)
    {
        score = score + points;
        UIManager.Instance.ShowNewPoints((int)points);
        scoreText.text = "Score: " + (int)score;
    }

    //Currently not implemented
    public void AddTimeScoreToScore()
    {
        int timeScore = (int)Timer.Instance.timeLeft * 5 * levelNumber;
    }

    private void InstantiateTargetsAndBalls(int numberOfBalls, int numberOfSmallTarets, int numberOfMediumTargets)
    {
        List<GameObject> newBallPositions = GetRandomItems<GameObject>(numberOfBalls, ballPositions);
        List<GameObject> newSmallTargets = GetRandomItems<GameObject>(numberOfSmallTarets, smallTargetPositions);
        List<GameObject> newMediumTargets = GetRandomItems<GameObject>(numberOfMediumTargets, mediumTargetsPositions);

        foreach(GameObject gameObj in newBallPositions){
            GameObject ball = Instantiate(playerBallPrefab, gameObj.transform);
            ActiveBalls.Add(ball);
        }

        foreach (GameObject gameObj in newSmallTargets)
        {
            //GameObject smallTarget = Instantiate(SmallTargetPrefab, gameObj.transform);
            //ActiveTargets.Add(smallTarget);
            //smallTarget.GetComponent<Animator>().SetTrigger("Initialization");
            StartCoroutine(InstantiateWithDelay(SmallTargetPrefab, gameObj.transform, ActiveTargets));
        }

        foreach (GameObject gameObj in newMediumTargets)
        {
            //GameObject mediumTarget = Instantiate(MediumTargetPrefab, gameObj.transform);
            //ActiveTargets.Add(mediumTarget);
            //mediumTarget.GetComponent<Animator>().SetTrigger("Initialization");
            StartCoroutine(InstantiateWithDelay(MediumTargetPrefab, gameObj.transform, ActiveTargets));
        }

    }

    private List<T> GetRandomItems<T>(int numberOfItems, List<T> ListToPickFrom)
    {
        List<T> itemsToPick = new List<T>();
        List<int> itemNumbersToPick = new List<int>();
        bool gotNumber;
        
        int maxNumberInList = ListToPickFrom.Count;

        for (int i = 0; i < numberOfItems; i++)
        {
            gotNumber = false;
            while(!gotNumber)
            {
                int randomNumber = UnityEngine.Random.Range(0, maxNumberInList);
                if (itemNumbersToPick.Contains(randomNumber))
                {
                    gotNumber = false;
                }
                else
                {
                    itemNumbersToPick.Add(randomNumber);
                    gotNumber = true;
                }
            }
        }

        foreach(int number in itemNumbersToPick)
        {
            itemsToPick.Add(ListToPickFrom[number]);
        }

        return itemsToPick;
    }

    IEnumerator InstantiateWithDelay(GameObject prefab, Transform transform, List<GameObject> list)
    {
        yield return new WaitForSeconds(Random.Range(0f, 1f));
        GameObject gameObject = Instantiate(prefab, transform);
        list.Add(gameObject);
        gameObject.GetComponent<Animator>().SetTrigger("Initialization");
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateButtonGlow()
    {
        if(levelNumber == 1)
        {
            LevelButtons[0].GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", buttonGlow);
            return;
        }
        Debug.Log(levelNumber + " " + LevelButtons.Count);
        LevelButtons[levelNumber - 1].GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", buttonGlow);
        LevelButtons[levelNumber - 2].GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", defaultButtonGlow);
    }

}
