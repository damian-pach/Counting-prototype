using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    public bool isHoldingBall = false;
    public GameObject ballPositionOnPickup;

    public GameObject ballPickedUp;

    public Slider slider;
    public Color sliderInitialColor;

    void Start()
    {
        sliderInitialColor = slider.fillRect.GetComponent<Image>().color;
    }

    void Update()
    {
        if (isHoldingBall && slider.gameObject.activeSelf == false)
        {
            slider.gameObject.SetActive(true);
        }
        else if(!isHoldingBall && slider.gameObject.activeSelf == true)
        {
            slider.gameObject.SetActive(false);
        }
    }
}
