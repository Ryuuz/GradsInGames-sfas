using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterScript : MonoBehaviour
{
    public CatType m_TypeAccepted = CatType.all;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Cat" && (other.GetComponent<AIController>().m_CatType == m_TypeAccepted || m_TypeAccepted == CatType.all))
        {
            Destroy(other.gameObject);
        }
    }
}
