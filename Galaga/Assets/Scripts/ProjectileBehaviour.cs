using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour {
    [SerializeField] protected float maxDistance=100;

    protected float speed;
    protected bool moving = false;
    protected Vector2 startPosition;
    protected Vector2 direction;
    protected Rigidbody2D rigidBody;


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Shoot(Vector2 direction, float speed)
    {
        this.startPosition = transform.position;
        rigidBody.velocity = direction * speed;
    }

    private void Update()
    {
        if (rigidBody.velocity.magnitude > 0)
            if (Vector2.Distance(transform.position, startPosition) > maxDistance)
                Destroy(gameObject);

    }

}
