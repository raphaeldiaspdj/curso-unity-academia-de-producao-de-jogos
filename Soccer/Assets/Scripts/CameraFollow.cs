using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] protected float cameraDistance;
    [SerializeField] protected float cameraHeight;

    //[HideInInspector]
    public Transform target;

    [HideInInspector]
    public bool alignRotation = true;

    // Update is called once per frame
    void Update()
    {
        // Ser há um alvo, posicione-se atras e acima dele.
        if (target)
        {
            //                        iguala a posição  +  Cria um vetor na diagonal /     *  multiplica pela distancia desejada   +  acrescenta uma variável de altura 
            transform.position = target.position + ((target.forward * -1) + target.up) * cameraDistance + (Vector3.up * cameraHeight);
        }
        if (alignRotation)
            transform.LookAt(target);
    }
}
