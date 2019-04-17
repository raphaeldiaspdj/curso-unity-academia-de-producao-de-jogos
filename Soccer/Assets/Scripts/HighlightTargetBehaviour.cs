using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightTargetBehaviour : MonoBehaviour
{
    [SerializeField] protected GameObject particleSelectionPrefab;

    protected RaycastHit rayCastInfo;
    protected Ray ray;
    protected GameObject particleObject;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            ray = new Ray(GameManager.instance.GetActivePlayer().transform.position, GameManager.instance.GetActivePlayer().transform.forward);
  
            if (Physics.Raycast(ray, out rayCastInfo,100))
            {
                if (rayCastInfo.collider.gameObject.CompareTag(GameManager.instance.GetActivePlayer().tag))
                {
                    particleObject = Instantiate(particleSelectionPrefab);
                    particleObject.transform.position = rayCastInfo.collider.transform.position;
                    Destroy(particleObject, particleObject.GetComponent<ParticleSystem>().main.duration);
                }
            }
            
        }
    }
}
