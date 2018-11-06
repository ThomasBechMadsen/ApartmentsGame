using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Map map;
    public PlayerController pc;

    public Player player1;
    public Player player2;
    public Player CurrentPlayer { get; private set; }
    public int turnsCounter = 1;

    // Use this for initialization
    void Start () {
        EndTurn();
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
        CurrentPlayer.Moves = pc.StartMoves;
        turnsCounter++;
        print("Round: " + Mathf.FloorToInt(turnsCounter/2) + ". Current player is: " + CurrentPlayer.name + ", with moves: " + CurrentPlayer.Moves);
    }
}
