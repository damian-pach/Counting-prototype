using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    private CharacterController characterController;
    public float UpVelocity = 10f;
    private float timePassed = 0f;
    private float jumpTime = 0.5f;

    [SerializeField]
    private bool isJumping;

    private bool initialJump;



    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (initialJump)
            {
                timePassed += Time.deltaTime;
                if (timePassed < jumpTime)
                {
                    characterController.Move((Vector3.up * UpVelocity + Vector3.up * 9.81f) * Time.fixedDeltaTime);
                }
                else
                {
                    initialJump = false;
                    timePassed = 0f;
                }
            }
        }
        else if (characterController.isGrounded)
        {
            //Debug.Log("not pressing space");
            initialJump = true;
            timePassed = 0f;
        }
    }

    private void StartJumping()
    {

    }

    // Update is called once per frame
   // void FixedUpdate()
   // {
        //if (isJumping /*&& Input.GetKey(KeyCode.Space)*/)
        //{
        //    characterController.Move(Vector3.up * Time.fixedDeltaTime * UpVelocity + Vector3.up * Time.fixedDeltaTime * 9.81f);
        //    timePassed += Time.fixedDeltaTime;
        //    if (timePassed >= jumpTime)
        //    {
        //        isJumping = false;
        //        timePassed = 0f;
        //    }
        //}

        //if (characterController.velocity.y < 0)
        //{
        //    characterController.Move(Vector3.down * 9.81f * (fallMultiplier - 1) * Time.fixedDeltaTime);
        //}
        //else if (characterController.velocity.y > 0 && !isJumping)
        //{
        //    characterController.Move(Vector3.down * 9.81f * (lowJumpMultiplier - 1) * Time.fixedDeltaTime);
        //}

    //}

   // public float fallMultiplier = 2.5f;
    //public float lowJumpMultiplier = 2f;

}
