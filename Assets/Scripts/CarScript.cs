using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour
{
    private float speed;

    private void Update()
    {
        if (!this.gameObject.activeSelf)
            return;

        float move = speed * Time.deltaTime;
        transform.Translate(0f, 0f, move);
    }

    public void OnTriggerEnter(Collider coll)
    {
        if(coll.tag == "Destroyer")
            PoolManager.Instance.Destroy(this.gameObject);
    }

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }
}
