using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //Obsolete class
    public Camera shootingCam;
    public Camera[] lookingCams;
    public Camera followingCam;
    public Camera fromBackCam;

    public static CameraManager Instance
    {
        get
        {
            return instance;
        }
    }
    private static CameraManager instance;

    void Start()
    {
        instance = this;
        ShowShootingCam();
    }

    public void ShowLookingCam()
    {
        shootingCam.enabled = false;
        lookingCams[Random.Range(0,lookingCams.Length)].enabled = true;
    }

    public void ShowShootingCam()
    {
        shootingCam.enabled = true;
        foreach (Camera cam in lookingCams)
        {
            cam.enabled = false;
        }
    }

    public void SetFollowingCamNewTarget(GameObject newTarget)
    {
        followingCam.GetComponent<FollowerScript>().SetNewFollowingBall(newTarget);
        fromBackCam.GetComponent<LookAt>().ball = newTarget;
    }
}
