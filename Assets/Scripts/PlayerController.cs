using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameManager gm;
    public int StartMoves;
    public int MoveCost;
    public Ability[] abilities;
    public Ability currentAbility;

    void Start()
    {
        currentAbility = abilities[0];
    }

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
        if (Input.GetMouseButtonDown(0))
        {
            UseCurrentAbility();
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

    void UseCurrentAbility()
    {
        if (currentAbility.cost > gm.CurrentPlayer.Moves)
        {
            return;
        }
        switch (currentAbility.abilityName)
        {
            case "BuildWall":
                Tile.Direction direction = GetMouseDirection();
                Build(direction);
                print(gm.CurrentPlayer.name + " tried to build a wall in direction: " + direction);
                break;
            case "DestroyWall":
                Destroy(GetMouseDirection());
                break;
        }
        UseMoves(currentAbility.cost);
    }

    void Build(Tile.Direction direction)
    {
        gm.map.tiles[gm.CurrentPlayer.mapPosition.x, gm.CurrentPlayer.mapPosition.y].CreateWall(direction);
    }

    void Destroy(Tile.Direction direction)
    {
        gm.map.tiles[gm.CurrentPlayer.mapPosition.x, gm.CurrentPlayer.mapPosition.y].DestroyWall(direction);
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

    Tile.Direction GetMouseDirection()
    {
        //https://answers.unity.com/questions/269760/ray-finding-out-x-and-z-coordinates-where-it-inter.html by aldonaletto · Jun 19, 2012 at 03:10 AM 
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane hPlane = new Plane(Vector3.up, Vector3.zero);
        float distance = 0;
        if (hPlane.Raycast(mouseRay, out distance))
        {
            Vector3 hitPoint = mouseRay.GetPoint(distance) + new Vector3(0, 0.5f, 0);
            Vector3 direction = (gm.CurrentPlayer.transform.position + hitPoint).normalized;
            Debug.DrawLine(gm.CurrentPlayer.transform.position, hitPoint, Color.blue, 1);

            if (direction.x > 0.5f)
            {
                return Tile.Direction.East;
            }
            else if (direction.x < -0.5f)
            {
                return Tile.Direction.West;
            }
            else if (direction.z > 0.5f)
            {
                return Tile.Direction.North;
            }
            else if(direction.z < -0.5f)
            {
                return Tile.Direction.South;
            }
        }
        return Tile.Direction.North;
    }
}
