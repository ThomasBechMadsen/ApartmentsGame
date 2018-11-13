using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBarController : MonoBehaviour {

    public GameManager gm;
    public GameObject energyIcon;
    public Color player1Color;
    public Color player2Color;
    public Color spentColor;

    Image[] moves;

    void Start()
    {
        moves = new Image[gm.pc.StartMoves];
        for (int i = 0; i < moves.Length; i++)
        {
            moves[i] = Instantiate(energyIcon, transform).GetComponent<Image>();
            moves[i].GetComponent<Image>().color = spentColor;
        }
    }

    public void SwitchColor(Color color)
    {
        foreach(Image im in moves)
        {
            im.color = color;
        }
    }

    public void UseMoves()
    {
        for (int i = moves.Length; i > 0; i--)
        {
            if (moves[i].color != spentColor)
            {
                moves[i].color = spentColor;
                break;
            }
        }
    }

    public void UseMoves(int times)
    {
        for (int i = moves.Length-1; i > 0; i--)
        {
            if (moves[i].color != spentColor)
            {
                times--;
                moves[i].color = spentColor;
                if (times == 0)
                {
                    break;
                }
            }
        }
    }
}
