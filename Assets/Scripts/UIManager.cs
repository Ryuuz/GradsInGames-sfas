using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text[] m_KibbleText = new Text[4];
    public Image[] m_KibbleImage = new Image[4];

    // --------------------------------------------------------------

    private int m_CurrentActive = 0;

    // --------------------------------------------------------------

        //Update the amount of kibble ammo
    public void SetAmmoText(int kibbleCount, int kibbleType)
    {
        if(m_KibbleText[kibbleType])
        {
            m_KibbleText[kibbleType].text = kibbleCount.ToString();
        }
    }

    // Highlight the active kibble type and darken the inactive
    public void SetActiveAmmo(int kibbleType)
    {
            m_KibbleImage[m_CurrentActive].color = Color.black;
            m_KibbleImage[kibbleType].color = Color.white;

            m_CurrentActive = kibbleType;
    }
}
