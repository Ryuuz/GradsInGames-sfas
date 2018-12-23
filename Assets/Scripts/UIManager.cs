using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    // --------------------------------------------------------------

    [SerializeField]
    Text m_KibbleText;

    // --------------------------------------------------------------

    public void SetAmmoText(int kibbleCount)
    {
        if(m_KibbleText)
        {
            m_KibbleText.text = "Kibbles: " + kibbleCount;
        }
    }
}
