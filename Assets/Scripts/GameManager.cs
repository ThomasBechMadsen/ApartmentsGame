using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Map map;
    public PlayerController pc;
    public CameraController cameracontroller;

    public Player player1;
    public Player player2;
    public Player CurrentPlayer;
    public int turnsCounter = 1;


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
        resetMoves(CurrentPlayer);
        turnsCounter++;
        print("Round: " + Mathf.FloorToInt(turnsCounter/2) + ". Current player is: " + CurrentPlayer.name + ", with moves: " + CurrentPlayer.Moves);
    }

    public bool checkWinConditions()
    {
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

    public void resetMoves(Player player)
    {
        player.Moves = pc.StartMoves;
    }
}
