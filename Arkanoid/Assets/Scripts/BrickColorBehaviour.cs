using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickColorBehaviour : MonoBehaviour {
    //Vamos utilizar o conceito de lista/Array para armazenar as cores
    public Color[] damageColor;

	int maxHits;
	protected int hitNumbers;

	// Use this for initialization
	void Start () {
		hitNumbers = 0;
        maxHits = damageColor.Length;
        GetComponent<SpriteRenderer>().color = damageColor[hitNumbers];
    }
	
	private void OnCollisionExit2D(Collision2D collision)
	{
        hitNumbers++;
        if (hitNumbers == maxHits)
            Destroy(gameObject);
        else
            GetComponent<SpriteRenderer> ().color = damageColor [hitNumbers];
	}
}
