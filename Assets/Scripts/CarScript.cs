using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;

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

}
