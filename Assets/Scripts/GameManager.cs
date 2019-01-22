using System.Collections;
using System.Collections.Generic;
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

    private GameObject[] m_Cats;
    private string[] m_LevelNames = new string[] { "LevelOne", "LevelTwo", "LevelThree", "LevelFour", "LevelFive" };
    private string m_Lost = "No kibbles left";
    private string m_Won = "The room is free of cats!";

    // Start is called before the first frame update
    void Start()
    {
        m_Menu.SetActive(false);
        m_Cats = GameObject.FindGameObjectsWithTag("Cat");
    }

    // Update is called once per frame
    void Update()
    {
        if(!CatsLeft())
        {
            m_Menu.SetActive(true);
            m_Message.text = m_Won;
        }
        else if(m_Gun.GetComponent<GunLogic>().GetAllAmmo() <= 0)
        {
            m_Menu.SetActive(true);
            m_NextButton.SetActive(false);
            m_Message.text = m_Lost;
        }
    }

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
