using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamVisble : MonoBehaviour
{
    public PlayerController playerController;

    public void OnBecameInvisible()
    {
        if (!GameManager.Instance.playerController)
            return;

        if (playerController != GameManager.Instance.playerController)
            return;

        //if (GameManager.Instance.isDeath && !GameManager.Instance.isStart)
        //    return;

        Debug.Log("»Æ¿Œ");
        GameManager.Instance.isDeath = true;
        GameManager.Instance.GameOverInit();
        SoundManager.Instance.OnDeath();
    }
}
