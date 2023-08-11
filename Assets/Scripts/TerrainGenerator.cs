using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        
    }

    public void OnGenerate()
    {
        GameObject terrain = terrains[Random.Range(0, terrains.Count - 1)];
        Instantiate(terrain, new Vector3(0, terrain.transform.position.y, currentPosition.z), Quaternion.identity);
        currentPosition.z++;
    }
}
