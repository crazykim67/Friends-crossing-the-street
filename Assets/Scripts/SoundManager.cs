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

    public AudioSource death_1;
    public AudioSource death_2;

    public AudioSource jumpAd;

    private void Awake()
    {
        instance = this;
    }

    public void OnDeath()
    {
        death_1.Play();
        death_2.Play();
    }
    public void OnJump()
    {
        jumpAd.Play();
    }
}
