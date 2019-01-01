using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{

    // --------------------------------------------------------------

    // The charactercontroller of the player
    MovementController m_MovementController;
    List<GameObject> m_Treats = new List<GameObject>();
    GameObject m_CurrentTreat;

    // --------------------------------------------------------------

    void Awake()
    {
        m_MovementController = GetComponent<MovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        m_MovementController.RotateCharacter();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Treat")
        {
            m_Treats.Add(other.gameObject);
        }
    }
}
