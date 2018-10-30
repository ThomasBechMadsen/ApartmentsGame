using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameManager gm;
    public int StartMoves;
    public int MoveCost;
    public int BuildCost;
    public int DestoryCost;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Tile.Direction.East);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Tile.Direction.West);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(Tile.Direction.North);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(Tile.Direction.South);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Build(Tile.Direction.North);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Destroy(Tile.Direction.North);
        }
    }

    void Move(Tile.Direction direction)
    {
        switch (direction)
        {
            case Tile.Direction.North:
                if (!gm.map.tiles[gm.CurrentPlayer.mapPosition.x, gm.CurrentPlayer.mapPosition.y].north)
                {
                    gm.CurrentPlayer.transform.Translate(new Vector3(0, 0, 1 + gm.map.tilePadding));
                    gm.CurrentPlayer.mapPosition.y++;
                }
                break;
            case Tile.Direction.East:
                if (!gm.map.tiles[gm.CurrentPlayer.mapPosition.x, gm.CurrentPlayer.mapPosition.y].east)
                {
                    gm.CurrentPlayer.transform.Translate(new Vector3(1 + gm.map.tilePadding, 0, 0));
                    gm.CurrentPlayer.mapPosition.x++;
                }
                break;
            case Tile.Direction.South:
                if (!gm.map.tiles[gm.CurrentPlayer.mapPosition.x, gm.CurrentPlayer.mapPosition.y].south)
                {
                    gm.CurrentPlayer.transform.Translate(new Vector3(0, 0, -1 - gm.map.tilePadding));
                    gm.CurrentPlayer.mapPosition.y--;
                }
                break;
            case Tile.Direction.West:
                if (!gm.map.tiles[gm.CurrentPlayer.mapPosition.x, gm.CurrentPlayer.mapPosition.y].west)
                {
                    gm.CurrentPlayer.transform.Translate(new Vector3(-1 - gm.map.tilePadding, 0, 0));
                    gm.CurrentPlayer.mapPosition.x--;
                }
                break;
        }
        UseMoves(MoveCost);
    }

    void Build(Tile.Direction direction)
    {
        gm.map.tiles[gm.CurrentPlayer.mapPosition.x, gm.CurrentPlayer.mapPosition.y].CreateWall(direction);
        UseMoves(BuildCost);
    }

    void Destroy(Tile.Direction direction)
    {
        gm.map.tiles[gm.CurrentPlayer.mapPosition.x, gm.CurrentPlayer.mapPosition.y].DestroyWall(direction);
        UseMoves(DestoryCost);
    }

    void UseMoves(int moves)
    {
        gm.CurrentPlayer.Moves -= moves;
        print("Moves left: " + gm.CurrentPlayer.Moves);
        if (gm.CurrentPlayer.Moves <= 0)
        {
            gm.EndTurn();
        }
    }
}
