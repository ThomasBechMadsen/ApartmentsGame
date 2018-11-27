using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Ability : ScriptableObject{
    
    [SerializeField]
    public string abilityName;
    [SerializeField]
    public int cost;

    public Ability(string abilityName, int cost)
    {
        this.abilityName = abilityName;
        this.cost = cost;
    }

    public abstract bool Use(Tile.Direction direction);
    public abstract void StartVisualEffect();
    public abstract void VisualEffect();
    public abstract void EndVisualEffect();
}
