using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalKeeperBehaviour : MonoBehaviour
{
    [SerializeField] protected float range;
    [SerializeField] protected float speed;

    protected Rigidbody myRigidbody;
    protected Vector3 startPosition;
    protected Vector3 newPosition;
    protected Vector3 orientation;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        startPosition = transform.position;
        orientation = transform.right;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        newPosition = myRigidbody.position + (orientation * speed);
        myRigidbody.MovePosition(newPosition);
       
        if (Vector3.Distance(startPosition, myRigidbody.position) > range)
            orientation *= -1;
    }
}
