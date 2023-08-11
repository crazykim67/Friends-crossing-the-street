using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningLight : MonoBehaviour
{
    private Animator anim;

    [SerializeField]
    private Spawner spawner;

    [SerializeField]
    private float currSec = 0f;

    [SerializeField]
    private bool isPrefab;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        //if(isPrefab)
        //    currSec = 10f;
    }

    public void Update()
    {
        currSec += Time.deltaTime;
        //float currSec = Time.time;
        if (Mathf.Round(spawner.nextTime) <= currSec)
        {
            anim.SetBool("isWarning", true);
        }
    }

    public void Safety()
    {
        anim.SetBool("isWarning", false);
    }
}
