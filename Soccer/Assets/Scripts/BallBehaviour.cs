using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public bool rolling = false;
    protected Rigidbody rigidBody;
    public float timeElapsedIdle;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();    
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameManager.instance.onBallTouchPlayer(collision.collider.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        print("GOOOOOOOOOOOOOOOOAL " + other.name);
        if (other.gameObject.CompareTag("Goal"))
            GameManager.instance.onBallInGoal(other);
    }

    private void FixedUpdate()
    {
        if (rolling && rigidBody.velocity.magnitude < 2)
            timeElapsedIdle += Time.fixedDeltaTime;
        else
            timeElapsedIdle = 0;

    }
}
