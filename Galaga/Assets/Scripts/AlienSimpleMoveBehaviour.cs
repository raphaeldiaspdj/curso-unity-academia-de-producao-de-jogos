using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSimpleMoveBehaviour : MonoBehaviour
{
    protected Animator animatorComponent;

    // Start is called before the first frame update
    void Start()
    {
        animatorComponent = GetComponent<Animator>();
        Invoke("flyMove", Random.Range(3, 8));
    }

    protected void flyMove()
    {
        animatorComponent.SetTrigger("attack");

        Invoke("flyMove", Random.Range(3, 5));
    }
}
