using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour {

    public Text roundsCounter;
    public Text player1Score;
    public Text player2Score;

    public void setRounds(int value)
    {
        roundsCounter.text = value.ToString();
    }

    public void setPlayer1Score(int value)
    {
        player1Score.text = value.ToString();
    }

    public void setPlayer2Score(int value)
    {
        player2Score.text = value.ToString();
    }
}
