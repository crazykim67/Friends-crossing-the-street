using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int carIndex;

    private Transform spwanPos;

    //public GameObject carPrefab;
    [HideInInspector]
    public float spawnDelay;

    //[HideInInspector]
    public float nextTime = 0f;
    private float currSec = 0f;
    [SerializeField]
    private float minDelay, maxDelay;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float offsetY;

    private void Start()
    {
        spwanPos = this.transform;
    }

    private void Update()
    {
        currSec += Time.deltaTime;
        //float currSec = Time.time;

        if (nextTime <= currSec)
        {
            CloneCar();
            // 이 부분은 특정 엔티티만 해당되게끔 수정해야함
            spawnDelay = Random.Range(minDelay, maxDelay);
            nextTime = currSec + spawnDelay;
        }
    }

    private void CloneCar()
    {
        Transform clonePos = spwanPos;
        Vector3 offsetPos = clonePos.position;

        offsetPos.y = offsetY;

        //GameObject cloneObject = Instantiate(carPrefab, offsetPos, clonePos.rotation, this.transform);
        CarScript car = PoolManager.Instance.Get(carIndex, offsetPos, clonePos.rotation, this.transform).GetComponent<CarScript>();
        car.SetSpeed(speed);
    }
}
