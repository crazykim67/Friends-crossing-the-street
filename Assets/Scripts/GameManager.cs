using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new GameManager();
                return instance;
            }

            return instance;
        }
    }

    [Header("Player Relate")]
    public GameObject playerObject;
    public Transform spawnPoint;

    [Header("Camera Relate")]
    public CameraController cameraController;
    public Transform cameraInitTr;

    [Header("Current PlayerController")]
    public PlayerController playerController;

    public WaterDeath waterDeath;
    private WaterDeath currentWaterDeath;

    public bool isStart = false;
    public bool isDeath = false;

    [Header("Main UI")]
    public GameObject mainUI;
    public Button startBtn, exitBtn;

    [Header("Over UI")]
    public GameObject overUI;
    public GameObject highScoreObj;
    public TextMeshProUGUI overscoreText;
    public Button retryBtn, mainBtn;

    [Header("Save Score")]
    public GameObject inGameUI;
    public TextMeshProUGUI scoreText;
    public int saveScore = 0;

    [Header("Mobile Pad")]
    public GameObject pad;
    public Button up, left, right, down;

    [Header("Test")]
    public Button destroyPrefs;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);


        if(PlayerPrefs.HasKey("Score"))
            saveScore = PlayerPrefs.GetInt("Score");

        retryBtn.onClick.AddListener(() => { Init(); });
        mainBtn.onClick.AddListener(() => { MainInit(); });
        startBtn.onClick.AddListener(() => { Init(); });

        destroyPrefs.onClick.AddListener(() =>
        {
            PlayerPrefs.DeleteKey("Score");
            saveScore = 0;
        });
    }

    public void MainInit()
    {
#if UNITY_ANDROID
        pad.SetActive(false);
#endif

        inGameUI.SetActive(false);
        overUI.SetActive(false);

        mainUI.SetActive(true);

        isStart = false;
    }

    public void Init()
    {
#if UNITY_ANDROID
        pad.SetActive(true);
#else
        pad.SetActive(false);
#endif

        isStart = true;
        isDeath = false;

        if (playerController)
            Destroy(playerController.gameObject);

        if (overUI.activeSelf)
            overUI.SetActive(false);

        TerrainGenerator.Instance.OnReset();
        
        PlayerController.score = 0;

        playerController = Instantiate(playerObject, spawnPoint.position, spawnPoint.rotation).GetComponent<PlayerController>();
        playerController.cameraController = cameraController;

        cameraController.camTr.position = cameraInitTr.position;
        cameraController.transform.position = cameraInitTr.position;
        cameraController.player = playerController.gameObject;

        waterDeath.coll = playerController.GetComponent<BoxCollider>();

        UpdateScore(0);

        mainUI.SetActive(false);
        inGameUI.SetActive(true);

        playerController.InitMobilePad(up, left, right, down);
    }

#region WaterOnDeath()

    public void OnWater()
    {
        if (!playerController || !cameraController)
            return;

        Vector3 waterPos = new Vector3(playerController.transform.position.x, waterDeath.transform.position.y, playerController.transform.position.z);
        currentWaterDeath = Instantiate(waterDeath, waterPos, Quaternion.identity);
        GameOverInit();
        currentWaterDeath.OnAction();
        StartCoroutine(DestroyWater());
    }

    public IEnumerator DestroyWater()
    {
        yield return new WaitForSeconds(1f);

        Destroy(currentWaterDeath.gameObject);
        currentWaterDeath = null;
    }

#endregion

    public void UpdateScore(int _score)
    {
        scoreText.text = $"점수 : {_score.ToString()}";
    }

    public void GameOverInit() 
    {
#if UNITY_ANDROID
        pad.SetActive(false);
#endif

        isStart = false;
        inGameUI.SetActive(false);
        overUI.SetActive(true);

        overscoreText.text = $"점수 : {PlayerController.score}";
        if (PlayerPrefs.HasKey("Score"))
        {
            if (saveScore < PlayerController.score)
            {
                highScoreObj.SetActive(true);
                PlayerPrefs.SetInt("Score", PlayerController.score);
                saveScore = PlayerPrefs.GetInt("Score");
            }
            else
                highScoreObj.SetActive(false);
        }
        else
        {
            highScoreObj.SetActive(true);
            PlayerPrefs.SetInt("Score", PlayerController.score);
            saveScore = PlayerPrefs.GetInt("Score");
        }
    }
}
