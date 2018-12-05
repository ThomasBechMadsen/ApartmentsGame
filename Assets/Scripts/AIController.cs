using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIController : MonoBehaviour
{
    public PlayerController playerController;

    private void Start()
    {

    }

 

    public IEnumerator MakeTurn()
    {
        IList<Tile.Direction> directions = new List<Tile.Direction>(){ Tile.Direction.North, Tile.Direction.West, Tile.Direction.East, Tile.Direction.South };

        while (GameManager.instance.CurrentPlayer.isAI)
        {
            Application.IListExtensions.Shuffle<Tile.Direction>(directions); // shuffle directions

            // try to build first
            playerController.SetCurrentAbility(playerController.abilities[0]);
            foreach (var d in directions)
            {
                if (IsTileFree(d))
                {
                    playerController.UseCurrentAbility(d);
                    yield return new WaitForSeconds(0.5f);
                    break;
                }
            }

            // try to destroy second
            playerController.SetCurrentAbility(playerController.abilities[1]);
            foreach (var d in directions)
            {
                if (!IsWalled(d))
                {
                    playerController.UseCurrentAbility(d);
                    yield return new WaitForSeconds(0.5f);
                    break;
                }
            }

            Application.IListExtensions.Shuffle<Tile.Direction>(directions); // shuffle directions

            foreach (var d in directions)
            {
                if (IsTileFree(d))
                {
                    playerController.Move(d);
                    yield return new WaitForSeconds(0.5f);
                    break;
                }
            }

            GameManager.instance.EndTurn();


        }
    }

    public bool IsTileFree(Tile.Direction direction)
    {
        switch (direction)
        {
            case Tile.Direction.North:
                if (!playerController.IsTileOccupied(GameManager.instance.CurrentPlayer.mapPosition.x, GameManager.instance.CurrentPlayer.mapPosition.y + 1))
                {
                    return true;
                }
                break;
            case Tile.Direction.East:
                if (!GameManager.instance.map.tiles[GameManager.instance.CurrentPlayer.mapPosition.x, GameManager.instance.CurrentPlayer.mapPosition.y].east
                    && !playerController.IsTileOccupied(GameManager.instance.CurrentPlayer.mapPosition.x + 1, GameManager.instance.CurrentPlayer.mapPosition.y))
                {
                    return true;
                }
                break;
            case Tile.Direction.South:
                if (!playerController.IsTileOccupied(GameManager.instance.CurrentPlayer.mapPosition.x, GameManager.instance.CurrentPlayer.mapPosition.y - 1))
                {
                    return true;
                }
                break;
            case Tile.Direction.West:
                if (!playerController.IsTileOccupied(GameManager.instance.CurrentPlayer.mapPosition.x - 1, GameManager.instance.CurrentPlayer.mapPosition.y))
                {
                    return true;
                }
                break;
        }
        return false;
    }

    public bool IsWalled(Tile.Direction direction)
    {
        switch (direction)
        {
            case Tile.Direction.North:
                if (!GameManager.instance.map.tiles[GameManager.instance.CurrentPlayer.mapPosition.x, GameManager.instance.CurrentPlayer.mapPosition.y].north)
                {
                    return true;
                }
                break;
            case Tile.Direction.East:
                if (!GameManager.instance.map.tiles[GameManager.instance.CurrentPlayer.mapPosition.x, GameManager.instance.CurrentPlayer.mapPosition.y].east)
                {
                    return true;
                }
                break;
            case Tile.Direction.South:
                if (!GameManager.instance.map.tiles[GameManager.instance.CurrentPlayer.mapPosition.x, GameManager.instance.CurrentPlayer.mapPosition.y].south)
                {
                    return true;
                }
                break;
            case Tile.Direction.West:
                if (!GameManager.instance.map.tiles[GameManager.instance.CurrentPlayer.mapPosition.x, GameManager.instance.CurrentPlayer.mapPosition.y].west)
                {
                    return true;
                }
                break;
        }
        return false;
    }
}
