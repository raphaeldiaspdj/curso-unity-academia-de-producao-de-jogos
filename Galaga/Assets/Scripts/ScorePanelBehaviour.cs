using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScorePanelBehaviour : MonoBehaviour {

    [SerializeField] protected GameObject playerScoreAlienCellPrefab;
    [SerializeField] protected GameObject playersScoreCellPrefab;
    [SerializeField] protected Transform playerAlienScorePanel;
    [SerializeField] protected Transform playersScorePanel;
    [SerializeField] protected Text scoreTotalText;
    [SerializeField] protected InputField playerNameFieldName;


    private void Start()
    {
 
        scoreTotalText.text = GameManager.instance.getTotalScore().ToString();

        foreach (var item in GameManager.instance.getPlayerAlienScoreTable())
        {
            //Cria uma instancia do prefab que possui os elementos de cada linha do Score
            GameObject scoreLine = Instantiate<GameObject>(playerScoreAlienCellPrefab);
            //Coloca o objeto como filho do painel de score
            scoreLine.transform.SetParent(playerAlienScorePanel);
            //Devido a escala do Canvas, precisamos forçar para que a escala do elemento fique em 1
            scoreLine.transform.localScale = Vector3.one;

            //Configura o sprite correto segundo o valor que está no dicinário 
            scoreLine.GetComponent<ScoreItemCellPrefabHelperBehaviour>().alienSprite.sprite = Resources.Load<Sprite>("Sprites/" + item.Key);
            scoreLine.GetComponent<ScoreItemCellPrefabHelperBehaviour>().alienCounter.text = item.Value.ToString();
        }
    }

    protected void mountPlayersScoreTable()
    {

        foreach (var item in GameManager.instance.getPlayersScoreTable())
        {
            //Cria uma instancia do prefab que possui os elementos de cada linha do Score
            GameObject scoreLine = Instantiate<GameObject>(playersScoreCellPrefab);
            //Coloca o objeto como filho do painel de score
            scoreLine.transform.SetParent(playersScorePanel);
            //Devido a escala do Canvas, precisamos forçar para que a escala do elemento fique em 1
            scoreLine.transform.localScale = Vector3.one;

            //Configura o sprite correto segundo o valor que está no dicinário 
            scoreLine.GetComponent<ScoreItemCellPrefabHelperBehaviour>().playerName.text = item.Key;
            scoreLine.GetComponent<ScoreItemCellPrefabHelperBehaviour>().playerScore.text = item.Value.ToString();

        }
    }

    public void OnConfirmPlayerName()
    {
        if (playerNameFieldName.text != "")
        {
            GameManager.instance.AddNewPlayerScore(playerNameFieldName.text, GameManager.instance.getTotalScore());
            mountPlayersScoreTable();

            playerNameFieldName.gameObject.SetActive(false);
            scoreTotalText.gameObject.SetActive(false);
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }
}
