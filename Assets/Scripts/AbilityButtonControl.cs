using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityButtonControl : MonoBehaviour {

    public PlayerController playerController;

    public void SetCurrentAbility(string abilityName)
    {
        playerController.SetCurrentAbility(playerController.abilities.Find(a => a.abilityName == abilityName));
    }
}
