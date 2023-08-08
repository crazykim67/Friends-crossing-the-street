using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);

        Init();
    }

    public void Init()
    {
        playerController = Instantiate(playerObject, spawnPoint.position, spawnPoint.rotation).GetComponent<PlayerController>();
        playerController.cameraController = cameraController;

        cameraController.transform.position = cameraInitTr.position;
        cameraController.player = playerController.gameObject;

        waterDeath.coll = playerController.GetComponent<BoxCollider>();
    }

    #region WaterOnDeath()

    public void OnWater()
    {
        if (!playerController || !cameraController)
            return;

        Vector3 waterPos = new Vector3(playerController.transform.position.x, waterDeath.transform.position.y, playerController.transform.position.z);
        currentWaterDeath = Instantiate(waterDeath, waterPos, Quaternion.identity);
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
}
