using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{

    // --------------------------------------------------------------

    // The charactercontroller of the player
    MovementController m_MovementController;

    // List of all the treats the cat has noticed
    List<GameObject> m_Treats = new List<GameObject>();

    // The treat the cat is currently going for
    GameObject m_CurrentTreat;

    // --------------------------------------------------------------

    void Awake()
    {
        m_MovementController = GetComponent<MovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        CleanUpTreats();

        // Only do this if the cat has noticed any treats
        if (m_Treats.Count != 0)
        {
            SetCurrentTreat();
            DistanceToKibble();
        }
        else
        {
            m_MovementController.SetHorizontalSpeed(0.0f);
        }

        FindDirection();
        m_MovementController.RotateCharacter();
    }

    // If a kibble is within the trigger sphere, the cat will notice it
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Treat")
        {
            m_Treats.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // When a kibble is no longer in the sphere, find it in the list and remove it
        // The cat will no longer know about it
        if(other.tag == "Treat")
        {
            for (int i = 0; i < m_Treats.Count; i++)
            {
                if(m_Treats[i] == other.gameObject)
                {
                    m_Treats.RemoveAt(i);
                    i = m_Treats.Count;
                }
            }
        }
        
    }

    // Finds the distance to the kibble the cat is currently going for
    void DistanceToKibble()
    {
        if (m_CurrentTreat)
        {
            float speed = 4.0f;

            // If close enough to stop and eat the kibble
            if (Vector3.Distance(m_CurrentTreat.transform.position, gameObject.transform.position) <= 1.0f)
            {
                speed = 0.0f;
                BulletLogic aKibble = m_CurrentTreat.GetComponent<BulletLogic>();

                if(!aKibble.GetBeingEaten() && aKibble.GetIfLanded())
                {
                    // Makes it unavailable for other cats and starts eating it
                    aKibble.SetEating();
                    StartCoroutine(EatKibble());
                }
            }

            m_MovementController.SetHorizontalSpeed(speed);
        }
    }

    IEnumerator EatKibble()
    {
        yield return new WaitForSeconds(1);
        Destroy(m_CurrentTreat);
    }

    // Decides on what kibble the cat should go for
    void SetCurrentTreat()
    {
        if(!m_CurrentTreat) //-------Make it possible for the cat to change its mind
        {
            //Finds the closest kibble
            float minDistance = 1000.0f;
            float distance;
            GameObject closest = null;

            foreach(GameObject treat in m_Treats)
            {
                distance = Vector3.Distance(treat.transform.position, gameObject.transform.position);

                if(distance < minDistance)
                {
                    minDistance = distance;
                    closest = treat;
                }
            }

            m_CurrentTreat = closest;
        }
    }

    // The direction the cat should walk in
    void FindDirection()
    {
        Vector3 direction = gameObject.transform.forward;

        if (m_CurrentTreat)
        {
            direction = m_CurrentTreat.transform.position - gameObject.transform.position;
            direction.y = 0.0f;
            direction.Normalize();
        }

        m_MovementController.SetDirection(direction);
    }

    //Clean up the list of kibbles by removing eaten ones
    void CleanUpTreats()
    {
        for(int i = 0; i < m_Treats.Count; i++)
        {
            if(m_Treats[i] == null)
            {
                m_Treats.RemoveAt(i);
                i--;
            }
        }
    }
}
