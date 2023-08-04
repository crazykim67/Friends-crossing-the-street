using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningLight : MonoBehaviour
{
    private Animator anim;

    [SerializeField]
    private Spawner spawner;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        float currSec = Time.time;
        if(spawner.nextTime <= currSec)
        {
            anim.SetBool("isWarning", true);
        }
    }

    public void Safety()
    {
        anim.SetBool("isWarning", false);
    }
}
