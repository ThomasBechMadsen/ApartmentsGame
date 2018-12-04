using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null; //Static instance of GameManager which allows it to be accessed by any other script.
    public Map map;
    public PlayerController playerController;
    public EnergyBarController energyBarController;
    public CameraController cameracontroller;
    public ScoreboardManager scoreboard;

    public Player player1;
    public Player player2;
    public Player CurrentPlayer;
    public int turnsCounter = 0;

    void Start () {
        StartTurn();
	}

    public void StartTurn()
    {
        if (instance == null)
        {
            instance = this;
        }
        ResetMoves();
        scoreboard.setRounds(Mathf.FloorToInt(turnsCounter / 2));
    }

    public void EndTurn()
    {
        if (CurrentPlayer == player1)
        {
            CurrentPlayer = player2;
        }
        else
        {
            CurrentPlayer = player1;
        }
        turnsCounter++;
        StartTurn();
    }

    public bool CheckWinConditions()
    {
        scoreboard.setPlayer1Score(player1.Points);
        scoreboard.setPlayer2Score(player2.Points);
        if (map.unclaimedTiles == 2)
        {
            print("Game Over!");
            if (player1.Points == player2.Points)
            {
                print("No winner :(");
            } else if (player1.Points > player2.Points)
            {
                print("Player 1 wins");
            }
            else
            {
                print("Player 2 wins");
            }
            return true;
        }
        return false;
    }

    public void ResetMoves()
    {
        CurrentPlayer.Moves = playerController.StartMoves;
        if (CurrentPlayer == player1)
        {
            energyBarController.SwitchColor(energyBarController.player1Color);
        }
        else
        {
            energyBarController.SwitchColor(energyBarController.player2Color);
        }
    }
}
