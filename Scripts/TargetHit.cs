using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHit : MonoBehaviour
{
    public float pointsWorth = 50f;
    public bool gotHit = false;
    public AudioClip targetHit;

    private float timesHitByABall = 0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBall"))
        {
            timesHitByABall += 1;
            Destroy(collision.gameObject);
            GameManager_New.Instance.AddPointsToScore(pointsWorth/timesHitByABall);

            if(timesHitByABall == 1f)
            {
                GetComponent<MeshRenderer>().material = DataManager.Instance.ShinyMaterial;
            }

            gotHit = true;
            GetComponent<AudioSource>().PlayOneShot(targetHit);

        }
    }
}
