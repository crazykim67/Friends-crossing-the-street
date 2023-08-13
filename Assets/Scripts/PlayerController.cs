using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    public bool isJumping;

    [SerializeField]
    private CamVisble visible;

    public static int score = 0;

    private float playerPosition;

    public CameraController cameraController;

    private Rigidbody rg;
    [Header("Raycast Relate")]
    [SerializeField]
    private Transform rayTr;
    [SerializeField]
    private float rayDistance = 0.5f;

    [Header("Mobile Pad")]
    public Button up, left, right, down;

    private void Start()
    {
        playerPosition = this.transform.position.z;
        rg = GetComponent<Rigidbody>();
    }

    private void Update()
    {
#if !UNITY_ANDROID
        InputPC();
#endif
        Raycast();
    }

    public void InputPC()
    {
        if (GameManager.Instance.isDeath || !GameManager.Instance.isStart)
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

    #region Mobile Input

    public void InitMobilePad(Button _up, Button _left, Button _right, Button _down)
    {
        up = _up;
        left = _left;
        right = _right;
        down = _down;

        up.onClick.RemoveAllListeners();
        left.onClick.RemoveAllListeners();
        right.onClick.RemoveAllListeners();
        down.onClick.RemoveAllListeners();

        up.onClick.AddListener(InputUp);
        left.onClick.AddListener(InputLeft);
        right.onClick.AddListener(InputRight);
        down.onClick.AddListener(InputDown);
    }

    public void InputUp()
    {
        if (!isJumping && FrontRaycast())
        {
            anim.SetTrigger("jump");
            isJumping = true;

            transform.position = (transform.position + new Vector3(0, 0, 1));
            transform.rotation = Quaternion.identity;
            IncreaseScore();
        }
    }
    public void InputLeft()
    {
        if (!isJumping && LeftRaycast())
        {
            anim.SetTrigger("jump");
            isJumping = true;

            transform.position = (transform.position + new Vector3(-1, 0, 0));
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
    }
    public void InputRight()
    {
        if (!isJumping && RightRaycast())
        {
            anim.SetTrigger("jump");
            isJumping = true;

            transform.position = (transform.position + new Vector3(1, 0, 0));
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }
    public void InputDown()
    {
        if (!isJumping && BackRaycast())
        {
            anim.SetTrigger("jump");
            isJumping = true;

            transform.position = (transform.position + new Vector3(0, 0, -1));
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    #endregion

    public void IncreaseScore()
    {
        if (GameManager.Instance.isDeath || !GameManager.Instance.isStart)
            return;

        if (playerPosition < this.transform.position.z)
        {
            score++;
            playerPosition = this.transform.position.z;

            if(cameraController)
            cameraController.CamParentFollow();

            TerrainGenerator.Instance.OnGenerate();
            TerrainGenerator.Instance.OnTerrainDisable();
            GameManager.Instance.UpdateScore(score);
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (GameManager.Instance.isDeath || !GameManager.Instance.isStart)
            return;

        if (coll.transform.tag == "Water")
        {
            GameManager.Instance.isDeath = true;
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
                GameManager.Instance.isDeath = true;
                GameManager.Instance.GameOverInit();
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
