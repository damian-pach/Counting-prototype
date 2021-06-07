using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Throwing : MonoBehaviour
{

    public float throwForce;
    private PlayerState playerState;
    private float timeToMaxForce = 1.5f;
    [SerializeField]
    private float _timePassed = 0f;
    // Start is called before the first frame update
    void Start()
    {
        playerState = GetComponent<PlayerState>();
    }

    float GetBetterRange(float timePassed)
    {
        return 1 - 1 / (8 * (timePassed / timeToMaxForce) + 1);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && playerState.isHoldingBall)
        {
            _timePassed = _timePassed + Time.deltaTime;
            float currentTimeValue = GetBetterRange(_timePassed);
            playerState.slider.value = currentTimeValue;
            Color tempColor = new Color(Mathf.Lerp(playerState.sliderInitialColor.r, Color.red.r, currentTimeValue),
                                        Mathf.Lerp(playerState.sliderInitialColor.g, Color.red.g, currentTimeValue),
                                        Mathf.Lerp(playerState.sliderInitialColor.b, Color.red.b, currentTimeValue));
            playerState.slider.fillRect.GetComponent<Image>().color = tempColor;

        }

        if (Input.GetKeyUp(KeyCode.Mouse0) || _timePassed >= timeToMaxForce)
        {
            if (playerState.isHoldingBall)
            {
                GameObject pickedBall = playerState.ballPickedUp;
                pickedBall.transform.parent = null;
                pickedBall.transform.position = playerState.ballPositionOnPickup.transform.position;
                pickedBall.GetComponent<Rigidbody>().isKinematic = false;
                pickedBall.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * throwForce * GetBetterRange(_timePassed), ForceMode.Impulse);
                pickedBall.GetComponent<Rigidbody>().useGravity = true;
                playerState.isHoldingBall = false;
                playerState.slider.value = 0f;
                playerState.slider.fillRect.GetComponent<Image>().color = playerState.sliderInitialColor;
                _timePassed = 0f;
            }
        }
    }
}
