using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CustomGameController : MonoBehaviour {

    public Text horizontalTextValue;
    public Text verticalTextValue;
    public Slider horizontalSlider;
    public Slider verticalSlider;
    public Loader loaderPrefab;
    public ToggleGroup toggleGroup;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        horizontalTextValue.text = horizontalSlider.value.ToString("0");
        verticalTextValue.text = verticalSlider.value.ToString("0");
	}

    public void CreateCustomGame()
    {
        Toggle theActiveToggle = toggleGroup.ActiveToggles().FirstOrDefault();

        if(theActiveToggle.gameObject.name.Equals("ToggleVersusAI"))
        {
            Debug.Log("versus AI");
        }
        else if (theActiveToggle.gameObject.name.Equals("ToggleVersusPlayer"))
        {
            Debug.Log("Versus Player");
        }

        Loader loader = Instantiate(loaderPrefab);
        loader.sizeX = (int)horizontalSlider.value;
        loader.sizeY = (int)verticalSlider.value;
        SceneManager.LoadScene("SampleScene");
    }
}
