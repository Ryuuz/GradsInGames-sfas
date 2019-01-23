using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void LevelOne()
    {
        SceneManager.LoadScene("LevelOne");
    }

    public void LevelTwo()
    {
        SceneManager.LoadScene("LevelTwo");
    }

    public void LevelThree()
    {
        SceneManager.LoadScene("LevelThree");
    }

    public void LevelFour()
    {
        SceneManager.LoadScene("LevelFour");
    }

    public void LevelFive()
    {
        SceneManager.LoadScene("LevelFive");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
