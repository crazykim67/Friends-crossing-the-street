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

    private void Update()
    {
        InputPC();
    }

    public void InputPC()
    {
        if (Input.GetKeyDown(KeyCode.W) && !isJumping)
        {
            anim.SetTrigger("jump");
            isJumping = true;

            //float anothZ = 0;

            //if(transform.position.z % 1 != 0)
            //{
            //    anothZ =  Mathf.Round(transform.position.z) -transform.position.z;
            //}

            transform.position = (transform.position + new Vector3(0, 0, -1));
            transform.rotation = Quaternion.identity;
        }
        else if(Input.GetKeyDown(KeyCode.A) && !isJumping)
        {
            anim.SetTrigger("jump");
            isJumping = true;

            transform.position = (transform.position + new Vector3(1, 0, 0));
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else if(Input.GetKeyDown(KeyCode.D) && !isJumping) 
        {
            anim.SetTrigger("jump");
            isJumping = true;

            transform.position = (transform.position + new Vector3(-1, 0, 0));
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if(Input.GetKeyDown(KeyCode.S) && !isJumping) 
        {
            anim.SetTrigger("jump");
            isJumping = true;

            transform.position = (transform.position + new Vector3(0, 0, 1));
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
