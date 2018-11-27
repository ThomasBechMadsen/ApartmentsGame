using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityButtonControl : MonoBehaviour {

    public void SetCurrentAbility(string abilityName)
    {
        GameManager.instance.pc.SetCurrentAbility(GameManager.instance.pc.abilities.Find(a => a.abilityName == abilityName));
    }
}
