using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityButtonControl : MonoBehaviour {

    public void SetCurrentAbility(string abilityName)
    {
        GameManager.instance.playerController.SetCurrentAbility(GameManager.instance.playerController.abilities.Find(a => a.abilityName == abilityName));
    }
}
