using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private static PoolManager instance;

    public static PoolManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new PoolManager();
                return instance;
            }

            return instance;
        }
    }

    // Prefab
    public GameObject[] carList;

    public List<GameObject>[] pools;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        pools = new List<GameObject>[carList.Length];

        for(int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index, Vector3 pos, Quaternion quat,Transform parent)
    {
        GameObject car = null;

        // 비활성화 되어있는 게임오브젝트 접근
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
            // 발견하면 car 변수에 할당
                car = item;
                car.transform.position = pos;
                car.SetActive(true);
                break;
            }
        }

        // 발견하지 못했다면
        // 새롭게 생성
        if (!car)
        {
            car = Instantiate(carList[index], pos, quat, parent);
            pools[index].Add(car);
        }

        return car;
    }

    public void Destroy(GameObject _obj)
    {
        _obj.SetActive(false);
    }
}
