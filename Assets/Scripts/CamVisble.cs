using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamVisble : MonoBehaviour
{
    public bool isVisble = true;

    public void OnBecameVisible()
    {
        isVisble = true;
    }

    public void OnBecameInvisible()
    {
        isVisble = false;
    }
}
