using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int carIndex;

    private Transform spwanPos;

    //public GameObject carPrefab;
    public float spawnDelay;

    protected float nextTime = 0f;

    [SerializeField]
    private float minDelay, maxDelay;

    private void Start()
    {
        spwanPos = this.transform;
    }

    private void Update()
    {
        float currSec = Time.time;

        if (nextTime <= currSec)
        {
            CloneCar();
            // �� �κ��� Ư�� ��ƼƼ�� �ش�ǰԲ� �����ؾ���
            spawnDelay = Random.Range(minDelay, maxDelay);
            nextTime = currSec + spawnDelay;
        }
    }

    private void CloneCar()
    {
        Transform clonePos = spwanPos;
        Vector3 offsetPos = clonePos.position;

        offsetPos.y = 0.25f;

        //GameObject cloneObject = Instantiate(carPrefab, offsetPos, clonePos.rotation, this.transform);
        CarScript car = PoolManager.Instance.Get(carIndex, offsetPos, clonePos.rotation, this.transform).GetComponent<CarScript>();
    }
}
