using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// The different types of cats
public enum CatType
{
    ginger, tabby, black, all
};

public class AIController : MonoBehaviour
{
    //The type of the cat
    public CatType m_CatType;

    // --------------------------------------------------------------

    private NavMeshAgent m_Agent;

    // The special type of kibble the cat likes
    private KibbleType m_KibbleType;

    // List of all the treats the cat has noticed
    private List<GameObject> m_Treats = new List<GameObject>();

    // The treat the cat is currently going for
    private GameObject m_CurrentTreat;

    // --------------------------------------------------------------

    void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        // Set preferred kibble based on type of cat
        switch (m_CatType)
        {
            case CatType.black:
                m_KibbleType = KibbleType.star;
                break;
            case CatType.ginger:
                m_KibbleType = KibbleType.fish;
                break;
            case CatType.tabby:
                m_KibbleType = KibbleType.triangle;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CleanUpTreats();

        // Only do this if the cat has noticed any treats
        if (m_Treats.Count > 0)
        {
            SetCurrentTreat();
            DistanceToKibble();
        }
    }

    // If a kibble is within the trigger sphere, the cat will notice it
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Treat")
        {
            KibbleType type = other.GetComponent<BulletLogic>().m_KibbleType;
            if(type == m_KibbleType || type == KibbleType.oval)
            {
                m_Treats.Add(other.gameObject);
            }
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
            // If close enough to stop and eat the kibble
            if (Vector3.Distance(m_CurrentTreat.transform.position, gameObject.transform.position) <= 0.6f)
            {
                BulletLogic aKibble = m_CurrentTreat.GetComponent<BulletLogic>();

                if (!aKibble.GetBeingEaten() && aKibble.GetIfLanded())
                {
                    // Makes it unavailable for other cats and starts eating it
                    aKibble.SetEating();
                    StartCoroutine(EatKibble());
                }
            }
            else
            {
                ChaseTreat();
            }
        }
    }

    IEnumerator EatKibble()
    {
        yield return new WaitForSeconds(1);
        Destroy(m_CurrentTreat);
    }

    // Sets the destination of the NavMesh Agent to within range of the kibble
    void ChaseTreat()
    {
        Vector3 destination = m_CurrentTreat.transform.position - transform.position;
        destination = destination.normalized * 0.5f;

        m_Agent.SetDestination(m_CurrentTreat.transform.position - destination);
    }

    // Decides on what kibble the cat should go for
    void SetCurrentTreat()
    {   
        if(m_CurrentTreat)
        {
            if(Vector3.Distance(m_CurrentTreat.transform.position, transform.position) > GetComponent<SphereCollider>().radius)
            {
                m_CurrentTreat = null;
            }
        }
        if(!m_CurrentTreat)
        {
            //Finds the closest kibble
            float minDistance = 1000.0f;
            float distance;
            GameObject closest = null;

            foreach(GameObject treat in m_Treats)
            {
                distance = Vector3.Distance(treat.transform.position, gameObject.transform.position);

                if(distance < minDistance && !treat.GetComponent<BulletLogic>().GetBeingEaten())
                {
                    minDistance = distance;
                    closest = treat;
                }
            }

            m_CurrentTreat = closest;
        }

        ChaseTreat();
    }

    // Clean up the list of kibbles by removing eaten ones
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
