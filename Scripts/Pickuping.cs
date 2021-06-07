using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickuping : MonoBehaviour
{
    private PlayerState playerState;

    void Start()
    {
        playerState = GetComponent<PlayerState>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (!playerState.isHoldingBall)
            {
                if (other.GetComponent<IInteractable>() != null)
                {
                    other.GetComponent<IInteractable>().Interact(playerState);
                    playerState.isHoldingBall = true;
                    playerState.ballPickedUp = other.gameObject;
                }
            }
        }
    }
}
