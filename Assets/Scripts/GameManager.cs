using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int m_Level;
    public GameObject m_Gun;
    public GameObject m_Menu;
    public GameObject m_NextButton;
    public Text m_Message;

    // --------------------------------------------------------------

    // All the cats in the level
    private GameObject[] m_Cats;

    private string[] m_LevelNames = new string[] { "LevelOne", "LevelTwo", "LevelThree", "LevelFour", "LevelFive" };
    private string m_Lost = "No kibbles left";
    private string m_Won = "The room is free of cats!";

    // --------------------------------------------------------------

    void Start()
    {
        // Hide the menu
        m_Menu.SetActive(false);
        m_Cats = GameObject.FindGameObjectsWithTag("Cat");
    }

    void Update()
    {
        // If no cats are left then the game is won
        if(!CatsLeft())
        {
            m_Menu.SetActive(true);
            m_Message.text = m_Won;
        }
        // Else if the player runs out of ammo the game is lost
        else if(m_Gun.GetComponent<GunLogic>().GetAllAmmo() == 0)
        {
            m_Menu.SetActive(true);
            m_NextButton.SetActive(false);
            m_Message.text = m_Lost;
        }
    }

    // Returns true as long as there are cats left in the level
    private bool CatsLeft()
    {
        bool cats = false;

        for(int i = 0; i < m_Cats.Length; i++)
        {
            if(m_Cats[i] != null)
            {
                cats = true;
                i = m_Cats.Length;
            }
        }

        return cats;
    }

    //Functions for the buttons in the menu
    public void NextLevel()
    {
        if(m_Level > 0 && m_Level < m_LevelNames.Length)
        {
            SceneManager.LoadScene(m_LevelNames[m_Level]);
        }
    }

    public void ReplayLevel()
    {
        if (m_Level > 0)
        {
            SceneManager.LoadScene(m_LevelNames[m_Level - 1]);
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
