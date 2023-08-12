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
    private Vector3 currentPosition = new Vector3(0, 0, 17);

    [SerializeField]
    private List<GameObject> terrains = new List<GameObject>();

    private void Awake() => instance = this;

    public List<GameObject> enviromentList = new List<GameObject>();
    public List<GameObject> newEnvList = new List<GameObject>();

    private List<GameObject> keepEnvList = new List<GameObject>();
    private int envIndex = 0;

    public void OnGenerate()
    {
        GameObject terrain = terrains[UnityEngine.Random.Range(0, terrains.Count - 1)];
        Instantiate(terrain, new Vector3(0, terrain.transform.position.y, currentPosition.z), Quaternion.identity);
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
        foreach(var env in enviromentList) 
            env.SetActive(true);

        foreach(var env in newEnvList) 
            Destroy(env);

        enviromentList.Clear();
        newEnvList.Clear();

        currentPosition = new Vector3(0, 0, 17);
        enviromentList = keepEnvList;
        envIndex = 0;
    }
}
