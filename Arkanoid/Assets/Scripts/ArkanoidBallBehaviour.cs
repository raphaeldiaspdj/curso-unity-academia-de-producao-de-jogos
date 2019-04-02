using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArkanoidBallBehaviour : MonoBehaviour {

	// Use this for initialization
	public float startSpeed;

    public GameManager gameManager;

	void Start () {

		GetComponent<Rigidbody2D>().velocity = (Vector2.up+Vector2.right)*startSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameManager.onBallCollision(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        gameManager.onBallCollision(other.gameObject);
    }
}
