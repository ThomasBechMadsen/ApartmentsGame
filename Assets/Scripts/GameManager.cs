using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null; //Static instance of GameManager which allows it to be accessed by any other script.
    public Map map;
    public PlayerController playerController;
    public AIController aiController;
    public EnergyBar energyBar;
    public CameraController cameracontroller;
    public Scoreboard scoreboard;
    public TextMeshProUGUI winnerText;

    public Player player1;
    public Player player2;
    public Player CurrentPlayer;
    public int turnsCounter = 0;


    private void Awake()
    {
        Loader loader = (Loader)FindObjectOfType(typeof(Loader));
        if (loader != null)
        {
            map.sizeX = loader.sizeX;
            map.sizeY = loader.sizeY;
            player2.isAI = loader.hasAI;
        }
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start () {
        StartTurn();
	}

    public void StartTurn()
    {

        ResetMoves();
        playerController.SetCurrentAbility(playerController.abilities[0]); // reset current abilities to build  
        scoreboard.setRounds(Mathf.FloorToInt(turnsCounter / 2));

        // call AI
        if(CurrentPlayer.isAI)
        {
            StartCoroutine(aiController.MakeTurn());
        }
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
            winnerText.gameObject.SetActive(true);
            if (player1.Points == player2.Points)
            {
                winnerText.SetText("EVERYONE IS A WINNER!");
            } else if (player1.Points > player2.Points)
            {
                winnerText.SetText("BLUE WINS!");
            }
            else
            {
                winnerText.SetText("RED WINS!");
            }
            StartCoroutine(returnToMenu(5));
            return true;
        }
        return false;
    }

    public void ResetMoves()
    {
        CurrentPlayer.Moves = playerController.StartMoves;
        if (CurrentPlayer == player1)
        {
            energyBar.SwitchColor(energyBar.player1Color);
        }
        else
        {
            energyBar.SwitchColor(energyBar.player2Color);
        }
    }

    public void SetMapSize(int x, int y)
    {
        map.sizeX = x;
        map.sizeY = y;
    }

    private IEnumerator returnToMenu(int delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(0);
    }
}
