using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingProjetileBehaviour : MonoBehaviour {

    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected float projectileSpeed;

    protected GameObject projectileTemp;

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            projectileTemp = Instantiate(projectilePrefab);
            projectileTemp.transform.position = transform.position;
            projectileTemp.GetComponent<ProjectileBehaviour>().Shoot(Vector2.up, projectileSpeed);
        }
    }
}
