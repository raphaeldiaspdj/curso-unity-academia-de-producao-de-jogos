using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBehaviour : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (gameObject.CompareTag("Alien"))
            GameManager.instance.OnAlienHit(gameObject, collider);

        else if(gameObject.CompareTag("Player"))
            GameManager.instance.OnShipHit(collider);
    }
}
