using UnityEngine;
using UnityEngine.UI;

public class TeleporterScript : MonoBehaviour
{
    public CatType m_TypeAccepted = CatType.all;
    public Image m_TeleporterIcon;
    public Sprite[] m_CatIcons = new Sprite[3];

    // --------------------------------------------------------------

    private void Start()
    {
        // Set icon depending on the type of cat the teleporter takes
        switch(m_TypeAccepted)
        {
            case CatType.black: m_TeleporterIcon.sprite = m_CatIcons[0];
                break;
            case CatType.ginger: m_TeleporterIcon.sprite = m_CatIcons[1];
                break;
            case CatType.tabby: m_TeleporterIcon.sprite = m_CatIcons[2];
                break;
            default: m_TeleporterIcon.enabled = false;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Cat" && (other.GetComponent<AIController>().m_CatType == m_TypeAccepted || m_TypeAccepted == CatType.all))
        {
            Destroy(other.gameObject);
        }
    }
}
