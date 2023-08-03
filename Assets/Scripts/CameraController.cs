using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;

    private void Update()
    {
        float move = speed * Time.deltaTime;
        transform.Translate(0f, 0f, move);
    }
}
