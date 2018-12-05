using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public Loader loaderPrefab;

    public void PlayGame()
    {
        Loader loader = Instantiate(loaderPrefab);
        loader.sizeX = 10;
        loader.sizeY = 10;
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        UnityEngine.Application.Quit();
    }
}
