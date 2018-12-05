using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null; //Static instance of GameManager which allows it to be accessed by any other script.
    public Map map;
    public PlayerController pc;
    public EnergyBarController energyBarController;
    public CameraController cameracontroller;

    public Player player1;
    public Player player2;
    public Player CurrentPlayer;
    public int turnsCounter = 1;


    private void Awake()
    {
        Loader loader = (Loader)FindObjectOfType(typeof(Loader));
        if (loader != null)
        {
            map.sizeX = loader.sizeX;
            map.sizeY = loader.sizeY;
        }
    }

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
        print("Round: " + Mathf.FloorToInt(turnsCounter / 2) + ". Current player is: " + CurrentPlayer.name + ", with moves: " + CurrentPlayer.Moves);
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
        CurrentPlayer.Moves = pc.StartMoves;
        if (CurrentPlayer == player1)
        {
            energyBarController.SwitchColor(energyBarController.player1Color);
        }
        else
        {
            energyBarController.SwitchColor(energyBarController.player2Color);
        }
    }

    public void SetMapSize(int x, int y)
    {
        map.sizeX = x;
        map.sizeY = y;
    }
}
