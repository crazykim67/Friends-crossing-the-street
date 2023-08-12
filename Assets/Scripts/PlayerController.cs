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
    public static int score = 0;
    private float playerPosition;

    public CameraController cameraController;

    public static bool isDeath = false;

    private Rigidbody rg;
    [Header("Raycast Relate")]
    [SerializeField]
    private Transform rayTr;
    [SerializeField]
    private float rayDistance = 0.5f;

    private void Start()
    {
        playerPosition = this.transform.position.z;
        rg = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        InputPC();
        Raycast();
    }

    public void InputPC()
    {
        if (isDeath)
            return;

        if (Input.GetKeyDown(KeyCode.W) && !isJumping && FrontRaycast())
        {
            anim.SetTrigger("jump");
            isJumping = true;

            transform.position = (transform.position + new Vector3(0, 0, 1));
            transform.rotation = Quaternion.identity;
            IncreaseScore();
        }
        else if(Input.GetKeyDown(KeyCode.A) && !isJumping && LeftRaycast())
        {
            anim.SetTrigger("jump");
            isJumping = true;

            transform.position = (transform.position + new Vector3(-1, 0, 0));
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else if(Input.GetKeyDown(KeyCode.D) && !isJumping && RightRaycast()) 
        {
            anim.SetTrigger("jump");
            isJumping = true;

            transform.position = (transform.position + new Vector3(1, 0, 0));
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if(Input.GetKeyDown(KeyCode.S) && !isJumping && BackRaycast()) 
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

        if (playerPosition < this.transform.position.z)
        {
            score++;
            playerPosition = this.transform.position.z;

            if(cameraController)
            cameraController.CamParentFollow();

            TerrainGenerator.Instance.OnGenerate();
            TerrainGenerator.Instance.OnTerrainDisable();
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
                rg.isKinematic = true;
                isDeath = true;
                transform.position = new Vector3(transform.position.x, 0.75f, transform.position.z);
            }
        }
        else
        {
            this.transform.parent = null;
        }
    }

    #region DirectionCheck

    private void Raycast()
    {
        Debug.DrawRay(rayTr.position, Vector3.right * rayDistance, Color.red);
        Debug.DrawRay(rayTr.position, Vector3.left * rayDistance, Color.red);
        Debug.DrawRay(rayTr.position, Vector3.forward * rayDistance, Color.red);
        Debug.DrawRay(rayTr.position, Vector3.back * rayDistance, Color.red);
    }

    private bool LeftRaycast()
    {
        int layer = 1 << LayerMask.NameToLayer("Env");

        RaycastHit hit;

        if(Physics.Raycast(rayTr.position, Vector3.left, out hit, rayDistance, layer))
            if(hit.transform.tag == "Env")
                return false;

        return true;
    }

    private bool RightRaycast()
    {
        int layer = 1 << LayerMask.NameToLayer("Env");

        RaycastHit hit;

        if (Physics.Raycast(rayTr.position, Vector3.right, out hit, rayDistance, layer))
            if (hit.transform.tag == "Env")
                return false;

        return true;
    }

    private bool FrontRaycast()
    {
        int layer = 1 << LayerMask.NameToLayer("Env");

        RaycastHit hit;

        if (Physics.Raycast(rayTr.position, Vector3.forward, out hit, rayDistance, layer))
            if (hit.transform.tag == "Env")
                return false;

        return true;
    }

    private bool BackRaycast()
    {
        int layer = 1 << LayerMask.NameToLayer("Env");

        RaycastHit hit;

        if (Physics.Raycast(rayTr.position, Vector3.back, out hit, rayDistance, layer))
            if (hit.transform.tag == "Env")
                return false;

        return true;
    }

    #endregion

}
