using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamVisble : MonoBehaviour
{
    public bool isVisble = true;

    public void OnBecameVisible()
    {
        Debug.Log("����");
        isVisble = true;
    }

    public void OnBecameInvisible()
    {
        Debug.Log("�Ⱥ���");
        isVisble = false;
    }
}
