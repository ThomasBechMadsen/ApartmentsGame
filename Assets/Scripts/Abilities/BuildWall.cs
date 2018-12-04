using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildWall", menuName = "BuildWall", order = 51)]
public class BuildWall : Ability {

    [SerializeField]
    public GameObject vfxWall;
    GameObject vfxWallInstance;

    public BuildWall(string abilityName, int cost, GameObject vfxWall) : base(abilityName, cost)
    {
        this.abilityName = abilityName;
        this.cost = cost;
        this.vfxWall = vfxWall;
    }

    public override void StartVisualEffect()
    {
        vfxWallInstance = Instantiate(vfxWall);
    }
    
    public override void VisualEffect()
    {
        Player player = GameManager.instance.CurrentPlayer;
        Tile.Direction direction = GameManager.instance.playerController.GetMouseDirection();
        Tile playerTile = GameManager.instance.map.tiles[player.mapPosition.x, player.mapPosition.y];
        switch (direction)
        {
            case Tile.Direction.North:
                if (!playerTile.north)
                {
                    vfxWallInstance.transform.position = player.transform.position + new Vector3(0, 0, 0.5f + GameManager.instance.map.tilePadding / 2);
                    vfxWallInstance.transform.LookAt(player.transform.position + Vector3.forward);
                }
                break;
            case Tile.Direction.East:
                if (!playerTile.east)
                {
                    vfxWallInstance.transform.position = player.transform.position + new Vector3(0.5f + GameManager.instance.map.tilePadding / 2, 0, 0);
                    vfxWallInstance.transform.LookAt(player.transform.position + Vector3.right);
                }
                break;
            case Tile.Direction.South:
                if (!playerTile.south)
                {
                    vfxWallInstance.transform.position = player.transform.position + new Vector3(0, 0, -0.5f - GameManager.instance.map.tilePadding / 2);
                    vfxWallInstance.transform.LookAt(player.transform.position - Vector3.forward);
                }
                break;
            case Tile.Direction.West:
                if (!playerTile.west)
                {
                    vfxWallInstance.transform.position = player.transform.position + new Vector3(-0.5f - GameManager.instance.map.tilePadding / 2, 0, 0);
                    vfxWallInstance.transform.LookAt(player.transform.position - Vector3.right);
                }
                break;
        }
    }

    public override void EndVisualEffect()
    {
        if (vfxWallInstance)
        {
            Destroy(vfxWallInstance.gameObject);
        }
    }

    public override bool Use(Tile.Direction direction)
    {
        Wall result =  GameManager.instance.map.tiles[GameManager.instance.CurrentPlayer.mapPosition.x, GameManager.instance.CurrentPlayer.mapPosition.y].CreateWall(direction, GameManager.instance.CurrentPlayer);
        if (result)
        {
            GameManager.instance.playerController.UseMoves(cost);
            if (result.back.north && result.back.south && result.back.east && result.back.west)
            {
                if (!GameManager.instance.playerController.IsTileOccupied(result.back.mapPosition.x, result.back.mapPosition.x))
                {
                    Tile.ClaimTile(result.back, GameManager.instance.CurrentPlayer);
                    GameManager.instance.CheckWinConditions();
                }
            }
            return true;
        }
        return false;
    }
}
