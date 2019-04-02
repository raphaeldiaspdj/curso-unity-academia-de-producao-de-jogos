using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalAutoMovimentBehaviour : MonoBehaviour {

    public int distance;
    public float speed = 0.2f;
    int direction = 1;
    Vector2 startPosition;

	// Use this for initialization
	void Start () {
        startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Abs(transform.position.x - startPosition.x) > distance)
            direction *= -1;
        transform.Translate(speed * direction, 0, 0);
	}
}
