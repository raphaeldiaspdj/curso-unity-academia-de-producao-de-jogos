using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDHelperBehaviour : MonoBehaviour
{
    [SerializeField] protected Transform playerLifeIconCanvas;
    [SerializeField] protected GameObject playerLifeIconPrefab;
    [SerializeField] protected Text scoreTextField;

    private void Start()
    {
        for (int i = 0; i < GameManager.instance.getPlayerLifes(); i++)
        {
            GameObject playerLifeIcon = Instantiate(playerLifeIconPrefab);
            playerLifeIcon.transform.SetParent(playerLifeIconCanvas);
            playerLifeIcon.transform.localScale = Vector3.one;
        }
    }

    void Update()
    {
       scoreTextField.text =  GameManager.instance.getTotalScore().ToString();
       if(GameManager.instance.getPlayerLifes()>0 && GameManager.instance.getPlayerLifes() < playerLifeIconCanvas.transform.childCount)
            Destroy(playerLifeIconCanvas.transform.GetChild(GameManager.instance.getPlayerLifes()-1).gameObject);

    }

}
