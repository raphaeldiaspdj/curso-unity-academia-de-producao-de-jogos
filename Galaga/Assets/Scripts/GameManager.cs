using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum AlienType
    {
        ALIEN_ZAKO,
        ALIEN_GOEI,
        ALIEN_BOSS
    }

    [SerializeField] protected GameObject[] alienFormations;

    [SerializeField] protected GameObject[] alienKinds;

    [SerializeField] protected GameObject playerShipPrefab;


    [SerializeField] protected int initialPlayerLifes;
    [SerializeField] protected GameObject playerLifeIconPrefab;
    [SerializeField] protected Transform playerLifeIconCanvas;
    [SerializeField] protected Text scoreTextField;
    [SerializeField] protected int alienPoints;
    [SerializeField] protected GameObject alienExplosionPrefab;

    protected int activeStage;
    protected int aliensCount;
    protected int playerLifes;
    private GameObject playerShip;
    private GameObject currentFormation;
    protected int score;

    protected Dictionary<string,int> playerAlienScoreTable;

    protected Dictionary<string, int> playersScoreTable;


    public static GameManager instance;
    

    //Awake é sempre chamado antes de qualquer método Start
    void Awake()
    {
        //PlayerPrefs.DeleteAll();
        if (instance == null)
        { //Se a variável estática estiver nula,
            instance = this; //Atribui o OBJETO GameManager a variável estática.
            DontDestroyOnLoad(gameObject);//Define que o objeto não deve ser destruído
        }
        else if (instance != this) //Se a variável for diferente de this, já foi criada antes
            Destroy(gameObject);//Destrói o objeto    

        instance.StartGame();
    }

    public void StartGame()
    {
        playerAlienScoreTable = new Dictionary<string, int>();
        activeStage = 0;
        aliensCount = 0;
        score = 0;
        playerLifes = initialPlayerLifes;

        playerShip = Instantiate(playerShipPrefab);

        currentFormation = MountAlienFormation(activeStage);
        CreateAliensInFormation(currentFormation.transform);

        //scoreTextField.text = score.ToString();
        loadPlayersScoreData();
    }

    protected void loadPlayersScoreData()
    {
        playersScoreTable = new Dictionary<string, int>();

        string[] scoreTable;

        if (PlayerPrefs.HasKey("score"))
        {
            string scoreList = PlayerPrefs.GetString("score");
            scoreTable = scoreList.Split(";"[0]);
            for (int i = 0; i < scoreTable.Length; i++)
                playersScoreTable.Add(scoreTable[i].Split(":"[0])[0], int.Parse(scoreTable[i].Split(":"[0])[1]));
        }
    }

    public void AddNewPlayerScore(string playerName, int score)
    {
        //Testa se não existe um registro com este nome
        if (!playersScoreTable.ContainsKey(playerName))
            playersScoreTable.Add(playerName, score);
        //Se já existe um registro com este nome e o valor atual é menor que o novo valor
        else if (playersScoreTable[playerName] < score)
            playersScoreTable[playerName] = score;

        savePlayersScoreData();
    }

    protected void savePlayersScoreData()
    {
        string playersScoreData="";

        foreach (var item in playersScoreTable)
            playersScoreData += ";" + item.Key +":"+item.Value.ToString();

        playersScoreData = playersScoreData.Substring(1);

        PlayerPrefs.SetString("score", playersScoreData);
        PlayerPrefs.Save();
    }

    protected GameObject MountAlienFormation(int activeFormation)
    {
        //Cria a primeira formação indicada na lista.
        GameObject currentFormation = Instantiate<GameObject>(alienFormations[activeFormation]);
        //Define que o parent será o objeto específico para a formação dos aliens
        //currentFormation.transform.SetParent(formationContent);
        //Certifica-se que ele estará no centro
        currentFormation.transform.localPosition = Vector3.zero;
        //Neste momento o "wireframe" com a formação está na cena
        return currentFormation;
    }

    protected void CreateAliensInFormation(Transform formation)
    {
        foreach (Transform item in formation)
        {
            GameObject alien = createAlienByType(item.GetComponent<AlienPositionFormationBehaviour>().alienType);
            alien.transform.SetParent(item);
            alien.transform.localPosition = Vector3.zero;
        }

        aliensCount = formation.childCount;
    }

    protected GameObject createAlienByType(AlienType alienType)
    {
        switch (alienType)
        {
            case AlienType.ALIEN_GOEI:
                return Instantiate(alienKinds[0]);
            case AlienType.ALIEN_ZAKO:
                return Instantiate(alienKinds[1]);
            default:
                return null;
        }
    }


    public void OnShipHit(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Alien"))
        {
            playerShip.GetComponent<Animator>().SetTrigger("hit");
            //Destroi o projetil
            Destroy(collider.gameObject);
            --playerLifes;
        }

        if (playerLifes == 0)
            SceneManager.LoadScene(2);

    }

    public void OnAlienHit(GameObject alienGameObject, Collider2D collider)
    {

        if (!collider.gameObject.CompareTag("Alien"))
        {
            #region Particle
            //Cria a particula no mesmo local onde a nave alien está
            GameObject particleExplosion = Instantiate<GameObject>(alienExplosionPrefab);
            particleExplosion.transform.position = alienGameObject.transform.position;
            #endregion

            #region ScoreTable
            ////Utiliza como Key o nome do sprite.
            string alienType = alienGameObject.GetComponent<SpriteRenderer>().sprite.name;

            ////Testa se já existe uma chave com o tipo do alien. Se já existe, só incrementa. Caso contrário, cria uma nova.
            
            if (playerAlienScoreTable.ContainsKey(alienType))
                playerAlienScoreTable[alienType] += 1;
            else
                playerAlienScoreTable.Add(alienType, 1);
            #endregion
            
            //Desconta da lista de aliens ativos
            aliensCount--;

            //Atualiza o Score
            score += alienPoints;
                        
            //Destruir o Alien
            Destroy(alienGameObject);
            //Destruir a projétil que o acertou
            Destroy(collider.gameObject);
        }

        if (aliensCount == 0)
        {
            //Destroi a formação atual
            Destroy(currentFormation);
            //troca para a próxima formação
            activeStage++;
            //Cria a formação e cria os aliens da formação
            currentFormation = MountAlienFormation(activeStage);
            CreateAliensInFormation(currentFormation.transform);
        }
    }

    public int getTotalScore()
    {
        return score;
    }

    public int getPlayerLifes()
    {
        return playerLifes;
    }

    public Dictionary<string,int> getPlayerAlienScoreTable()
    {
        return playerAlienScoreTable;
    }

    public Dictionary<string, int> getPlayersScoreTable()
    {
        return playersScoreTable;
    }
}
