using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerScript : MonoBehaviour
{

    public GameObject followingBall;

    void Update()
    {
        //transform.position = followingBall.transform.position + new Vector3(3f, 1, 1);
        //transform.LookAt(followingBall.transform.position + new Vector3(0, 0.5f, 0.7f));
    }

    public void SetNewFollowingBall(GameObject ball)
    {
        followingBall = ball;
    }
}
