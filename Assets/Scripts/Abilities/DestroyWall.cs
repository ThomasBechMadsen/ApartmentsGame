using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DestroyWall", menuName = "DestroyWall", order = 52)]
public class DestroyWall : Ability {

    public DestroyWall(string abilityName, int cost) : base(abilityName, cost)
    {
        this.abilityName = abilityName;
        this.cost = cost;
    }

    public override void EndVisualEffect()
    {
    }

    public override void StartVisualEffect()
    {
    }

    public override bool Use(Tile.Direction direction)
    {
        if(GameManager.instance.map.tiles[GameManager.instance.CurrentPlayer.mapPosition.x, GameManager.instance.CurrentPlayer.mapPosition.y].DestroyWall(direction))
        {
            GameManager.instance.pc.UseMoves(cost);
            return true;
        }
        return false;
    }

    public override void VisualEffect()
    {
    }
}
