using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int m_Level;
    public GameObject m_Gun;
    public GameObject m_Player;
    public GameObject m_Menu;
    public GameObject m_NextButton;
    public Text m_Message;

    // --------------------------------------------------------------

    // All the cats in the level
    private GameObject[] m_Cats;

    private PlayerController m_PlayerScript;
    private GunLogic m_GunScript;
    private bool m_Paused = false;
    private string[] m_LevelNames = new string[] { "LevelOne", "LevelTwo", "LevelThree", "LevelFour", "LevelFive" };
    private string m_Lost = "No kibbles left";
    private string m_Won = "The room is free of cats!";

    // --------------------------------------------------------------

    void Start()
    {
        // Hide the menu
        m_Menu.SetActive(false);
        m_Cats = GameObject.FindGameObjectsWithTag("Cat");

        if(m_Player)
        {
            m_PlayerScript = m_Player.GetComponent<PlayerController>();
        }

        if(m_Gun)
        {
            m_GunScript = m_Gun.GetComponent<GunLogic>();
        }
    }

    void Update()
    {
        if(!m_Paused)
        {
            // Check for movement input
            if(m_PlayerScript)
            {
                m_PlayerScript.UpdateMovementState(new Vector3(Input.GetAxisRaw("Horizontal_P1"), 0f, Input.GetAxisRaw("Vertical_P1")));
                m_PlayerScript.RotateCharacterTowardsMouseCursor();

                if (Input.GetButtonDown("Jump_P1"))
                {
                    m_PlayerScript.UpdateJumpState();
                }
            }
            
            if(m_GunScript)
            {
                if(Input.GetButtonDown("Fire1"))
                {
                    m_GunScript.ShootKibble();
                }

                if(Input.GetButtonDown("Ammo1"))
                {
                    m_GunScript.ChangeKibble(0);
                }
                else if(Input.GetButtonDown("Ammo2"))
                {
                    m_GunScript.ChangeKibble(1);
                }
                else if(Input.GetButtonDown("Ammo3"))
                {
                    m_GunScript.ChangeKibble(2);
                }
                else if(Input.GetButtonDown("Ammo4"))
                {
                    m_GunScript.ChangeKibble(3);
                }
            }

            // If no cats are left then the game is won
            if(!CatsLeft())
            {
                m_Menu.SetActive(true);
                m_Message.text = m_Won;
                m_Paused = true;
            }
            // Else if the player runs out of ammo the game is lost
            else if(m_Gun.GetComponent<GunLogic>().GetAllAmmo() == 0)
            {
                m_Menu.SetActive(true);
                m_NextButton.SetActive(false);
                m_Message.text = m_Lost;
                m_Paused = true;
            }
        }
        
        if(Input.GetButtonDown("Pause"))
        {
            PauseGame();
        }
    }

    // Pause or resume the game
    private void PauseGame()
    {
        if(m_Paused)
        {
            m_Menu.SetActive(false);
            m_Paused = false;
        }
        else
        {
            m_Menu.SetActive(true);
            m_NextButton.SetActive(false);
            m_Message.text = "Paused";
            m_Paused = true;
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
