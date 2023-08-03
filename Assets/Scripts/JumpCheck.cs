using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCheck : MonoBehaviour
{
    [SerializeField]
    private PlayerController controller;

    public void FinishJump()
    {
        controller.isJumping = false;
    }

}
