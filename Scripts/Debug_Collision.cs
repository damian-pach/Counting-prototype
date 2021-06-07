using UnityEngine;

public class Debug_Collision : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Wall")
        {
            Debug.Log("BALL COLLISION" + "\n" + "Collided with " + collision.gameObject.name + " with Tag: " + collision.gameObject.tag);
        }

    }
}
