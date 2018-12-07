using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public Loader loaderPrefab;

    public void PlayGame()
    {
        Loader loader = Instantiate(loaderPrefab);
        loader.sizeX = 5;
        loader.sizeY = 5;
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        UnityEngine.Application.Quit();
    }
}
