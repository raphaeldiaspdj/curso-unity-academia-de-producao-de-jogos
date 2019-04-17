using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovimentByScreenToRayBehaviour : MonoBehaviour {

    protected Vector3 targetPosition;
    protected Vector3 startPosition;
    protected float deltaTimeCounter;
    protected bool walkingTo = false;
    protected Vector3 nextStep;
    protected RaycastHit rayHit;

    [SerializeField] protected Animator animatorController;

    public void walkToPoint(Vector3 target)
    {
        Debug.Log("Mandou caminhar para " + target);
        //Seta o estado para caminhando para um alvo. Isto será útil para testes durante o trajeto
        walkingTo = true;
        targetPosition = target;
        startPosition = transform.position;
        //Faz o personagem olhar para o ponto alvo
        transform.LookAt(target);

        //animatorController.SetBool("walk", true);
        if (Vector3.Distance(startPosition,targetPosition) >10)
            animatorController.SetFloat("speed", 1f);
        else
            animatorController.SetFloat("speed", 0.5f);

    }

    private void Update()
    {
       
        if (Input.GetMouseButtonDown(0) && !walkingTo)
        {
            Ray rayFromCamera = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(rayFromCamera, out rayHit, 100))
                walkToPoint(rayHit.point);
        }

    }

    private void FixedUpdate()
    {
        if (walkingTo)
        {
            deltaTimeCounter += Time.deltaTime;

            Debug.Log(deltaTimeCounter);
            //                      posição inicial da reta, posição final reta, passo (0-1)
            nextStep = Vector3.Lerp(startPosition, targetPosition, deltaTimeCounter);

            GetComponent<Rigidbody>().MovePosition(nextStep);
            //Se chegou ao objetivo zera os valores e informa que não esta mais caminhando
            if (deltaTimeCounter >= 1)
            {
                deltaTimeCounter = 0;
                walkingTo = false;
                //animatorController.SetBool("walk", false);
                animatorController.SetFloat("speed", 0f);

            }
        }
    }
}
