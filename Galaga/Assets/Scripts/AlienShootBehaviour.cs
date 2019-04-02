using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienShootBehaviour : MonoBehaviour
{
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected float projectileSpeed;

    protected GameObject projectileTemp;
   
    public void missileShoot()
    {
        projectileTemp = Instantiate(projectilePrefab);
        projectileTemp.transform.position = transform.position;
        projectileTemp.GetComponent<ProjectileBehaviour>().Shoot(Vector2.down, projectileSpeed);
    }
}
