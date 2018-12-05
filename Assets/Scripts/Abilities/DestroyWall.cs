using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DestroyWall", menuName = "DestroyWall", order = 52)]
public class DestroyWall : Ability {

    public Material vfxMaterial;

    Wall lastVfxedWall;
    Material lastVfxedWallMaterial;

    public DestroyWall(string abilityName, int cost, Material vfxMaterial) : base(abilityName, cost)
    {
        this.abilityName = abilityName;
        this.cost = cost;
        this.vfxMaterial = vfxMaterial;
    }

    public override void StartVisualEffect()
    {
    }

    public override void VisualEffect()
    {
        Player player = GameManager.instance.CurrentPlayer;
        Tile.Direction direction = GameManager.instance.playerController.currentMouseDirection;
        Tile playerTile = GameManager.instance.map.tiles[player.mapPosition.x, player.mapPosition.y];
        Wall newVfxWall = null;
        switch (direction)
        {
            case Tile.Direction.North:
                if (playerTile.north)
                {
                    newVfxWall = playerTile.north;
                }
                break;
            case Tile.Direction.East:
                if (playerTile.east)
                {
                    newVfxWall = playerTile.east;
                }
                break;
            case Tile.Direction.South:
                if (playerTile.south)
                {
                    newVfxWall = playerTile.south;
                }
                break;
            case Tile.Direction.West:
                if (playerTile.west)
                {
                    newVfxWall = playerTile.west;
                }
                break;
        }

        if (lastVfxedWall != newVfxWall)
        {
            if (lastVfxedWall != null)
            {
                lastVfxedWall.GetComponent<MeshRenderer>().material = lastVfxedWallMaterial;
            }
            if (newVfxWall != null)
            {
                lastVfxedWallMaterial = newVfxWall.GetComponent<MeshRenderer>().material;
                if (newVfxWall.destructable)
                {
                    newVfxWall.GetComponent<MeshRenderer>().material = vfxMaterial;
                }
            }
            lastVfxedWall = newVfxWall;
        }
    }

    public override void EndVisualEffect()
    {
    }

    public override bool Use(Tile.Direction direction)
    {
        if(GameManager.instance.map.tiles[GameManager.instance.CurrentPlayer.mapPosition.x, GameManager.instance.CurrentPlayer.mapPosition.y].DestroyWall(direction))
        {
            GameManager.instance.playerController.UseMoves(cost);
            return true;
        }
        return false;
    }
}
