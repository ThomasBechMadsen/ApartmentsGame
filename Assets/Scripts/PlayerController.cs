using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {

    public GameManager gm;
    public int StartMoves;
    public int MoveCost;
    public List<Ability> abilities = new List<Ability>();
    public Ability currentAbility;
    
    void Start()
    {
        SetCurrentAbility(abilities[0]);
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
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            UseCurrentAbility();
        }
    }

    void Move(Tile.Direction direction)
    {
        switch (direction)
        {
            case Tile.Direction.North:
                if (!gm.map.tiles[gm.CurrentPlayer.mapPosition.x, gm.CurrentPlayer.mapPosition.y].north && !IsTileOccupied(gm.CurrentPlayer.mapPosition.x, gm.CurrentPlayer.mapPosition.y + 1))
                {
                    gm.CurrentPlayer.transform.Translate(new Vector3(0, 0, 1 + gm.map.tilePadding));
                    gm.CurrentPlayer.mapPosition.y++;
                }
                else
                {
                    return;
                }
                break;
            case Tile.Direction.East:
                if (!gm.map.tiles[gm.CurrentPlayer.mapPosition.x, gm.CurrentPlayer.mapPosition.y].east && !IsTileOccupied(gm.CurrentPlayer.mapPosition.x + 1, gm.CurrentPlayer.mapPosition.y))
                {
                    gm.CurrentPlayer.transform.Translate(new Vector3(1 + gm.map.tilePadding, 0, 0));
                    gm.CurrentPlayer.mapPosition.x++;
                }
                else
                {
                    return;
                }
                break;
            case Tile.Direction.South:
                if (!gm.map.tiles[gm.CurrentPlayer.mapPosition.x, gm.CurrentPlayer.mapPosition.y].south && !IsTileOccupied(gm.CurrentPlayer.mapPosition.x, gm.CurrentPlayer.mapPosition.y - 1))
                {
                    gm.CurrentPlayer.transform.Translate(new Vector3(0, 0, -1 - gm.map.tilePadding));
                    gm.CurrentPlayer.mapPosition.y--;
                }
                else
                {
                    return;
                }
                break;
            case Tile.Direction.West:
                if (!gm.map.tiles[gm.CurrentPlayer.mapPosition.x, gm.CurrentPlayer.mapPosition.y].west && !IsTileOccupied(gm.CurrentPlayer.mapPosition.x - 1, gm.CurrentPlayer.mapPosition.y))
                {
                    gm.CurrentPlayer.transform.Translate(new Vector3(-1 - gm.map.tilePadding, 0, 0));
                    gm.CurrentPlayer.mapPosition.x--;
                }
                else
                {
                    return;
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
        Tile.Direction direction = GetMouseDirection();
        bool result = false;
        switch (currentAbility.abilityName)
        {
            case "BuildWall":
                result = Build(direction);
                if (result)
                {
                    gm.CheckWinConditions();
                }
                print(gm.CurrentPlayer.name + " tried to build a wall in direction: " + direction);
                break;
            case "DestroyWall":
                result = Destroy(direction);
                print(gm.CurrentPlayer.name + " tried to destroy a wall in direction: " + direction);
                break;
        }
        if (result)
        {
            UseMoves(currentAbility.cost);
        }
    }

    bool Build(Tile.Direction direction)
    {
        return gm.map.tiles[gm.CurrentPlayer.mapPosition.x, gm.CurrentPlayer.mapPosition.y].CreateWall(direction, gm.CurrentPlayer);
    }

    bool Destroy(Tile.Direction direction)
    {
        return gm.map.tiles[gm.CurrentPlayer.mapPosition.x, gm.CurrentPlayer.mapPosition.y].DestroyWall(direction);
    }

    void UseMoves(int moves)
    {
        gm.CurrentPlayer.Moves -= moves;
        gm.energyBarController.UseMoves(moves);
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
            Vector3 direction = (hitPoint - gm.CurrentPlayer.transform.position).normalized;
            Debug.DrawLine(gm.CurrentPlayer.transform.position, direction, Color.blue, 1);

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

    public void SetCurrentAbility(Ability ability)
    {
        currentAbility = ability;
        print("CurrentAbility is now: " + currentAbility.abilityName);
    }

    public bool IsTileOccupied(int mapX, int mapY)
    {
        if ((gm.player1.mapPosition.x == mapX && gm.player1.mapPosition.y == mapY) || (gm.player2.mapPosition.x == mapX && gm.player2.mapPosition.y == mapY))
        {
            return true;
        }
        return false;
    }
}
