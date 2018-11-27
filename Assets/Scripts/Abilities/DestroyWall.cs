using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWall : Ability {

    public DestroyWall(string abilityName, int cost) : base(abilityName, cost)
    {
        this.abilityName = abilityName;
        this.cost = cost;
    }

    public override void EndVisualEffect()
    {
        throw new System.NotImplementedException();
    }

    public override void StartVisualEffect()
    {
        throw new System.NotImplementedException();
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
        throw new System.NotImplementedException();
    }
}
