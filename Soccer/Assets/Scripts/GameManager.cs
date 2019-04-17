using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] protected Camera playerCamera;
    [SerializeField] protected GameObject ballPrefab;
    [SerializeField] protected float kickForce;
    [SerializeField] protected GameObject goalKeeperA,goalKeeperB;
    [SerializeField] protected float maxTimeBallIdle;

    protected Dictionary<bool,GameObject> teams;
    protected Rigidbody activePlayerRigidBody;
    protected GameObject ball;
    protected GameObject activeTeam;
    protected GameObject activePlayer;
    protected int teamAScore,teamBScore;

    public static GameManager instance;
    private BallBehaviour ballBaheviour;

    //Uso de GETs para encapsular as variaveis
    public int TeamAScore { get => teamAScore; }
    public int TeamBScore { get => teamBScore; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
            Destroy(gameObject);

        instance.StartGame();
    }

    private void StartGame()
    {
        //Define que o time com a bola é do primeiro goleiro
        activeTeam = goalKeeperA;
     

        ball = Instantiate(ballPrefab);
        ballBaheviour = ball.GetComponent<BallBehaviour>();

        //Define que o jogador ativo é o goleiro. É ele quem sai com a bola.
        setActivePlayer(goalKeeperA);
    }

    public void onBallInGoal(Collider other)
    {
        if (other.gameObject.name.Equals("GoalTeamA"))
        {
            teamBScore++;
            setActivePlayer(goalKeeperA);
        }
        else if (other.gameObject.name.Equals("GoalTeamB"))
        {
            teamAScore++;
            setActivePlayer(goalKeeperB);
        }
    }

    private GameObject changeActiveTeam()
    {
        if (activeTeam == goalKeeperA)
            return goalKeeperB;
        else
            return goalKeeperA;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        activePlayerRigidBody.rotation = Quaternion.Euler(0, activePlayerRigidBody.rotation.eulerAngles.y+ Input.GetAxis("Horizontal"), 0);

        ////Testa se a bola passou X segundos fora da posse de um jogador
        if ( ballBaheviour.timeElapsedIdle > maxTimeBallIdle)
        {
            activeTeam = changeActiveTeam();
            setActivePlayer(activeTeam);
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !ball.GetComponent<BallBehaviour>().rolling)
        {
            ball.GetComponent<BallBehaviour>().rolling = true;
            ball.GetComponent<Rigidbody>().isKinematic = false;
            ball.transform.SetParent(null);
            ball.GetComponent<Rigidbody>().AddForce(activePlayer.transform.forward * kickForce);
        }
    }

    //Método chamado quando a bola estiver rolando e tocar em um jogador
    public void onBallTouchPlayer(GameObject gameObject)
    {
        if (ballBaheviour.rolling && gameObject.tag.Split("_"[0])[0].Equals("Player"))
            setActivePlayer(gameObject);
    }

    protected void setActivePlayer(GameObject player)
    {
        activePlayer = player;
        activePlayerRigidBody = activePlayer.GetComponent<Rigidbody>();

        setBallToPlayerFoot(player);
        //configura para que a camera aponte para este novo jogaodor
        playerCamera.GetComponent<CameraFollow>().target = activePlayer.transform;
    }

    protected void setBallToPlayerFoot(GameObject player)
    {
        //posiciona a bola no pé do novo jogador 
        ball.GetComponent<BallBehaviour>().rolling = false;
        ball.GetComponent<Rigidbody>().isKinematic = true;
        ball.transform.SetParent(activePlayer.GetComponent<PlayerFootPinBallHelper>().footPosition);
        ball.transform.localPosition = Vector3.zero;
    }

    public GameObject GetActivePlayer()
    {
        return activePlayer;
    }
}
