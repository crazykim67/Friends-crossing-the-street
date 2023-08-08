using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    public bool isJumping;

    [SerializeField]
    private CamVisble visible;

    [Header("Player Score")]
    private int score = 0;
    private float playerPosition;

    public CameraController cameraController;

    public static bool isDeath = false;

    private void Start()
    {
        playerPosition = this.transform.position.z;
    }

    private void Update()
    {
        InputPC();
    }

    public void InputPC()
    {
        if (isDeath)
            return;

        if (Input.GetKeyDown(KeyCode.W) && !isJumping)
        {
            anim.SetTrigger("jump");
            isJumping = true;

            transform.position = (transform.position + new Vector3(0, 0, 1));
            transform.rotation = Quaternion.identity;
            IncreaseScore();
        }
        else if(Input.GetKeyDown(KeyCode.A) && !isJumping)
        {
            anim.SetTrigger("jump");
            isJumping = true;

            transform.position = (transform.position + new Vector3(-1, 0, 0));
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else if(Input.GetKeyDown(KeyCode.D) && !isJumping) 
        {
            anim.SetTrigger("jump");
            isJumping = true;

            transform.position = (transform.position + new Vector3(1, 0, 0));
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if(Input.GetKeyDown(KeyCode.S) && !isJumping) 
        {
            anim.SetTrigger("jump");
            isJumping = true;

            transform.position = (transform.position + new Vector3(0, 0, -1));
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

    }

    public void IncreaseScore()
    {
        if (isDeath)
            return;

        //scorePosition = Vector3.Distance(playerPosition, );
        if (playerPosition < this.transform.position.z)
        {
            score++;
            playerPosition = this.transform.position.z;

            if(cameraController)
            cameraController.CamParentFollow();
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (isDeath)
            return;

        if (coll.transform.tag == "Water")
        {
            isDeath = true;
            GameManager.Instance.OnWater();
        }

        if(coll.transform.GetComponent<CarScript>() != null)
        {
            if (coll.transform.GetComponent<CarScript>().isRaft)
            {
                this.transform.parent = coll.transform;
            }
            else
            {
                anim.SetTrigger("isDeath");
                isDeath = true;
            }
        }
        else
        {
            this.transform.parent = null;
        }
    }

}
