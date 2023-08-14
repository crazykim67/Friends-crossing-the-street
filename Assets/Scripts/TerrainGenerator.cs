using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    private static TerrainGenerator instance;

    public static TerrainGenerator Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new TerrainGenerator();
                return instance;
            }

            return instance;
        }
    }

    // 초기 z 값은 17임
    private Vector3 currentPosition = new Vector3(0, 0, 16);

    [SerializeField]
    private List<GameObject> terrains = new List<GameObject>();

    public List<GameObject> enviromentList = new List<GameObject>();
    public List<GameObject> newEnvList = new List<GameObject>();

    public List<GameObject> keepEnvList = new List<GameObject>();
    private int envIndex = 0;

    private void Awake() => instance = this;
    public void OnGenerate()
    {
        int randomIndex = UnityEngine.Random.Range(0, terrains.Count - 1);
        GameObject terrain = Instantiate(terrains[randomIndex], new Vector3(0, terrains[randomIndex].transform.position.y, currentPosition.z), Quaternion.identity);
        currentPosition.z++;
        enviromentList.Add(terrain);
        newEnvList.Add(terrain);
    }

    public void OnTerrainDisable()
    {
        enviromentList[envIndex].SetActive(false);
        envIndex++;
    }

    public void OnReset()
    {
        // PoolManager에 있는 리스트 초기화
        PoolManager.Instance.Clear();

        enviromentList.Clear();
        foreach(var env in keepEnvList)
        {
            enviromentList.Add(env);
        }

        foreach (var env in enviromentList) 
            env.SetActive(true);

        foreach(var env in newEnvList) 
            Destroy(env);

        newEnvList.Clear();

        currentPosition = new Vector3(0, 0, 16);
        envIndex = 0;
    }
}
