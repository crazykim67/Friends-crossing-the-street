using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private float smoothness;

    private Transform camTr;

    private void Start()
    {
        camTr = Camera.main.transform;
    }

    private void Update()
    {
        float move = speed * Time.deltaTime;
        transform.Translate(0f, 0f, move);

        CameraFollow();
    }

    public void CamParentFollow()
    {
        if (this.transform.position.z > player.transform.position.z)
            return;

        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            player.transform.position.z);
    }

    private void CameraFollow()
    {
        camTr.position = Vector3.Lerp(camTr.position, this.transform.position, smoothness);
    }
}
