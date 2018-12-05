using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {

    public int StartMoves;
    public int MoveCost;
    public List<Ability> abilities = new List<Ability>();
    public Ability currentAbility;

    public Tile.Direction currentMouseDirection;
    
    void Start()
    {
        SetCurrentAbility(abilities[0]);
    }

    // Update is called once per frame
    void Update()
    {
        currentMouseDirection = UpdateMouseDirection();

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
        
        currentAbility.VisualEffect();
    }

    void Move(Tile.Direction direction)
    {
        switch (direction)
        {
            case Tile.Direction.North:
                if (!GameManager.instance.map.tiles[GameManager.instance.CurrentPlayer.mapPosition.x, GameManager.instance.CurrentPlayer.mapPosition.y].north 
                    && !IsTileOccupied(GameManager.instance.CurrentPlayer.mapPosition.x, GameManager.instance.CurrentPlayer.mapPosition.y + 1))
                {
                    GameManager.instance.CurrentPlayer.transform.Translate(new Vector3(0, 0, 1 + GameManager.instance.map.tilePadding));
                    GameManager.instance.CurrentPlayer.mapPosition.y++;
                }
                else
                {
                    return;
                }
                break;
            case Tile.Direction.East:
                if (!GameManager.instance.map.tiles[GameManager.instance.CurrentPlayer.mapPosition.x, GameManager.instance.CurrentPlayer.mapPosition.y].east 
                    && !IsTileOccupied(GameManager.instance.CurrentPlayer.mapPosition.x + 1, GameManager.instance.CurrentPlayer.mapPosition.y))
                {
                    GameManager.instance.CurrentPlayer.transform.Translate(new Vector3(1 + GameManager.instance.map.tilePadding, 0, 0));
                    GameManager.instance.CurrentPlayer.mapPosition.x++;
                }
                else
                {
                    return;
                }
                break;
            case Tile.Direction.South:
                if (!GameManager.instance.map.tiles[GameManager.instance.CurrentPlayer.mapPosition.x, GameManager.instance.CurrentPlayer.mapPosition.y].south 
                    && !IsTileOccupied(GameManager.instance.CurrentPlayer.mapPosition.x, GameManager.instance.CurrentPlayer.mapPosition.y - 1))
                {
                    GameManager.instance.CurrentPlayer.transform.Translate(new Vector3(0, 0, -1 - GameManager.instance.map.tilePadding));
                    GameManager.instance.CurrentPlayer.mapPosition.y--;
                }
                else
                {
                    return;
                }
                break;
            case Tile.Direction.West:
                if (!GameManager.instance.map.tiles[GameManager.instance.CurrentPlayer.mapPosition.x, GameManager.instance.CurrentPlayer.mapPosition.y].west 
                    && !IsTileOccupied(GameManager.instance.CurrentPlayer.mapPosition.x - 1, GameManager.instance.CurrentPlayer.mapPosition.y))
                {
                    GameManager.instance.CurrentPlayer.transform.Translate(new Vector3(-1 - GameManager.instance.map.tilePadding, 0, 0));
                    GameManager.instance.CurrentPlayer.mapPosition.x--;
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
        if (currentAbility.cost > GameManager.instance.CurrentPlayer.Moves)
        {
            return;
        }
        Tile.Direction direction = currentMouseDirection;
        currentAbility.Use(direction);
        
    }

    public void UseMoves(int moves)
    {
        GameManager.instance.CurrentPlayer.Moves -= moves;
        GameManager.instance.energyBarController.UseMoves(moves);
        print("Moves left: " + GameManager.instance.CurrentPlayer.Moves);
        if (GameManager.instance.CurrentPlayer.Moves <= 0)
        {
            GameManager.instance.EndTurn();
        }
    }

    private Tile.Direction UpdateMouseDirection()
    {
        //https://answers.unity.com/questions/269760/ray-finding-out-x-and-z-coordinates-where-it-inter.html by aldonaletto · Jun 19, 2012 at 03:10 AM 
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane hPlane = new Plane(Vector3.up, Vector3.zero);
        float distance = 0;
        if (hPlane.Raycast(mouseRay, out distance))
        {
            Vector3 hitPoint = mouseRay.GetPoint(distance) + new Vector3(0, 0.5f, 0);
            Vector3 direction = (hitPoint - GameManager.instance.CurrentPlayer.transform.position).normalized;
            Debug.DrawLine(GameManager.instance.CurrentPlayer.transform.position, GameManager.instance.CurrentPlayer.transform.position + direction, Color.green, 1);

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
        if(currentAbility != null)
        {
            currentAbility.EndVisualEffect();
        }
        currentAbility = ability;
        print("CurrentAbility is now: " + currentAbility.abilityName);
        currentAbility.StartVisualEffect();
    }

    public bool IsTileOccupied(int mapX, int mapY)
    {
        if ((GameManager.instance.player1.mapPosition.x == mapX && GameManager.instance.player1.mapPosition.y == mapY) ||
            (GameManager.instance.player2.mapPosition.x == mapX && GameManager.instance.player2.mapPosition.y == mapY))
        {
            return true;
        }
        return false;
    }
}
