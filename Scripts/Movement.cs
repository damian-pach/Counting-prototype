using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public bool isGroundedThere;
    public float pushPower = 10f;

    [SerializeField] private float moveVelocity = 10f;
    [SerializeField] private Vector2 moveVector;

    private CharacterController characterController;
    private Vector3 cameraForward;
    private Vector3 cameraRight;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        isGroundedThere = characterController.isGrounded;
        SetForwardAndRight();
        GetMoveVector();
        MovePlayer(moveVector);
        //Debug.Log(characterController.velocity);
    }

    private void SetForwardAndRight()
    {
        cameraForward = Camera.main.transform.forward;
        cameraRight = Camera.main.transform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();
    }

    private void GetMoveVector()
    {
        if (Input.GetKey(KeyCode.W))
        {
            moveVector.y = 1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveVector.y = -1f;
        }
        else
        {
            moveVector.y = 0f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveVector.x = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveVector.x = 1f;
        }
        else
        {
            moveVector.x = 0f;
        }
    }

    private void MovePlayer(Vector2 vectorToMove)
    {
        Vector3 _moveVector = (vectorToMove.x * cameraRight + vectorToMove.y * cameraForward).normalized * moveVelocity;
        characterController.Move((_moveVector + Vector3.down * 9.81f) * Time.fixedDeltaTime);

        if (_moveVector.magnitude > 0f)
        {
            characterController.transform.forward = _moveVector;
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        if (body == null || body.isKinematic)
            return;

        if (hit.moveDirection.y < -0.3f)
            return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        body.velocity = pushDir * (moveVelocity+1);
    }
}
