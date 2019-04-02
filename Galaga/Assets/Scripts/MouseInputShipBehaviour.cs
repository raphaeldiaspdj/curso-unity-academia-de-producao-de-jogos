using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputShipBehaviour : MonoBehaviour
{
    protected Vector2 nextPosition;
    protected SpriteRenderer spriteRender;
    protected Vector3 worldDimension;

    private void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        worldDimension = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    // Update is called once per frame
    void Update()
    {
        nextPosition =  new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y);
        
        if (nextPosition.x < (worldDimension.x - spriteRender.size.x/2) && nextPosition.x > (-worldDimension.x + spriteRender.size.x/2))
            GetComponent<Rigidbody2D>().MovePosition(nextPosition);
    }
}
