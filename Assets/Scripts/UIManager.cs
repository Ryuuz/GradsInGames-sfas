using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    public Text[] m_KibbleText = new Text[4];

    [SerializeField]
    public Image[] m_KibbleImage = new Image[4];

    int m_CurrentActive = 0;

    public void SetAmmoText(int kibbleCount, int kibbleType)
    {
        if(m_KibbleText[kibbleType])
        {
            m_KibbleText[kibbleType].text = kibbleCount.ToString();
        }
    }

    public void SetActiveAmmo(int kibbleType)
    {
            m_KibbleImage[m_CurrentActive].color = Color.black;
            m_KibbleImage[kibbleType].color = Color.white;

            m_CurrentActive = kibbleType;
    }
}
