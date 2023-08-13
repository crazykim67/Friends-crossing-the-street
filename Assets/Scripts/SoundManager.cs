using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    public static SoundManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new SoundManager();
                return instance;
            }

            return instance;
        }
    }

    public AudioSource bgmAd;

    private void Awake()
    {
        instance = this;
    }
}
