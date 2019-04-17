using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDBehaviour : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreTeamA, scoreTeamB;

    // Update is called once per frame
    void Update()
    {
        scoreTeamA.text = GameManager.instance.TeamAScore.ToString();
        scoreTeamB.text = GameManager.instance.TeamBScore.ToString();
    }
}
