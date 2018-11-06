using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Ability {
    
    public string abilityName;
    public int cost;

    public Ability(string abilityName, int cost)
    {
        this.abilityName = abilityName;
        this.cost = cost;
    }
}
