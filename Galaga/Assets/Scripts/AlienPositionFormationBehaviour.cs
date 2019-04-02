using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienPositionFormationBehaviour : MonoBehaviour
{

    public GameManager.AlienType alienType;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,.2f);
    }
}
