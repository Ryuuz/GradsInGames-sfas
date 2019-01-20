using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KibbleType
{
    oval, star, fish, triangle
}

public class BulletLogic : MonoBehaviour
{
    // The speed of the bullet
    [SerializeField]
    protected float m_BulletSpeed = 15.0f;

    public KibbleType m_KibbleType;

    private bool m_BeingEaten = false;
    private bool m_HasLanded = false;

    // Use this for initialization
    void Start()
    {
        // Add velocity to the bullet
        GetComponent<Rigidbody>().velocity = -transform.up * m_BulletSpeed;
    }

    // Update is called once per frame
    void Update ()
    {
        if(!m_HasLanded)
        {
            Landed();
        }
    }

    // Checks if the kibble has stopped moving
    void Landed()
    {
        if(GetComponent<Rigidbody>().velocity.magnitude == 0.0f)
        {
            m_HasLanded = true;
        }
    }

    public bool GetIfLanded()
    {
        return m_HasLanded;
    }

    // Should be called when a cat starts eating the kibble
    public void SetEating()
    {
        m_BeingEaten = true;
    }

    public bool GetBeingEaten()
    {
        return m_BeingEaten;
    }
}
