using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButtonControl : MonoBehaviour {

    public Image theBackground;
    public Toggle[] toggles;
    private ColorBlock theColor;

    public void SetCurrentAbility(string abilityName)
    {
        GameManager.instance.playerController.SetCurrentAbility(GameManager.instance.playerController.abilities.Find(a => a.abilityName == abilityName));
    }

    private void Start()
    {
        ChangeBackgroundColour();
    }

    public void ChangeBackgroundColour()
    {
        foreach(var toggled in toggles)
        {
            theColor = toggled.GetComponent<Toggle>().colors;
            if (toggled.isOn)
            {
                theColor.normalColor = new Color32(253, 164, 54, 255);
                toggled.colors = theColor;
            }
            else
            {
                theColor.normalColor = Color.white;
                toggled.colors = theColor;
            }
        }
    }

}
