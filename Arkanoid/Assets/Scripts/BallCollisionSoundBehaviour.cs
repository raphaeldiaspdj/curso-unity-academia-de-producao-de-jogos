using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisionSoundBehaviour : MonoBehaviour {
    public AudioSource soundFXWall;
    public AudioSource soundFXBrickHit;
    public AudioSource soundFXBrickBroken;
    public AudioSource soundTrack;

    private void Start()
    {
       // soundTrack.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall" || collision.collider.tag == "Paddle")
            soundFXWall.Play();
        else if (collision.collider.tag == "Brick")
            soundFXBrickHit.Play();
    }
    
}
