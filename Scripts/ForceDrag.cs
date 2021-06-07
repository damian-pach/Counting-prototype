using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceDrag : MonoBehaviour
{
    //Obsolete class
    public bool isInteractive = true;

    public float forceAmount = 50f;
    public bool isForwardLerped = true;
    public bool isHorizontalLerped;

    private Rigidbody rb;
    private Vector3 initialMousePos;
    private Vector3 newMousePos;
    private Vector3 initialObjectPos;

    private float minDrag;
    private float maxDrag;

    private bool isHoldingMouse = false;
    private bool isMouseReleased = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        initialObjectPos = transform.position;
    }

    private void Update()
    {
        if (isInteractive)
        {
            //newMousePos = Input.mousePosition;
            Vector2 normalizedMouseDrag = (initialMousePos - newMousePos).normalized;
            //Redukcja min i max
            float forwardSpeed_unlerped = Mathf.Clamp(initialMousePos.y - newMousePos.y, 0, Screen.height * 0.8f);
            float forwardSpeed = isForwardLerped ? Mathf.Lerp(0, 1, forwardSpeed_unlerped / (Screen.height * 0.8f)) : 1;
            
            float horizontalSpeed_unlerped = initialMousePos.x - newMousePos.x;
            float horizontalSpeed = isHorizontalLerped ? Mathf.Lerp(0, 1, horizontalSpeed_unlerped*0.5f / (Screen.width * 0.8f)) : 1;


            Vector3 forceVector = new Vector3(-normalizedMouseDrag.y * forwardSpeed, normalizedMouseDrag.y * forwardSpeed * 0.212121f, normalizedMouseDrag.x * horizontalSpeed);
            Debug.DrawRay(transform.position, forceVector * 5f, Color.red);

            if (isHoldingMouse && isMouseReleased)
            {
                isHoldingMouse = false;
                rb.AddForce(forceVector * forceAmount, ForceMode.Impulse);
                rb.useGravity = true;
                CameraManager.Instance.ShowLookingCam();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetPosition();
                CameraManager.Instance.ShowShootingCam();
            }
        }
    }

    public void DisableInteractiveness()
    {
        isInteractive = false;
        gameObject.tag = "Untagged";
    }
    
    public void ResetPosition()
    {
        transform.position = initialObjectPos;
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        rb.rotation = Quaternion.identity;
    }

    private void OnMouseDown()
    {
        isMouseReleased = false;
        isHoldingMouse = true;
        //initialMousePos = Input.mousePosition;
        initialMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0,0,Camera.main.nearClipPlane));
        Debug.Log("init: " + initialMousePos);
    }

    private void OnMouseUp()
    {
        isMouseReleased = true;
        //newMousePos = Input.mousePosition;
        newMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0,0,Camera.main.nearClipPlane));
        Debug.Log("new: " + newMousePos);
    }

}
