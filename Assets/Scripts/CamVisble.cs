using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamVisble : MonoBehaviour
{
    public bool isVisble = true;

    public void OnBecameVisible()
    {
        Debug.Log("보임");
        isVisble = true;
    }

    public void OnBecameInvisible()
    {
        Debug.Log("안보임");
        isVisble = false;
    }
}
