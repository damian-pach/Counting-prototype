using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupableBall : MonoBehaviour, IInteractable
{
    private PlayerState _playerState;

    public void Interact(PlayerState playerState)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        this.transform.parent = playerState.gameObject.transform;
        this.transform.localPosition = playerState.ballPositionOnPickup.transform.localPosition;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        _playerState = playerState;
    }

    private void OnDestroy()
    {
        GameManager_New.Instance.ActiveBalls.Remove(gameObject);
        if(_playerState != null)
        {
            if (gameObject == _playerState?.ballPickedUp)
            {
                _playerState.ballPickedUp = null;
                _playerState.isHoldingBall = false;
            }
        }
    }

}
