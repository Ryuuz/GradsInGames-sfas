using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    // The speed of the bullet
    [SerializeField]
    protected float m_BulletSpeed = 15.0f;

    // Use this for initialization
    void Start()
    {
        // Add velocity to the bullet
        GetComponent<Rigidbody>().velocity = -transform.up * m_BulletSpeed;
    }

    // Update is called once per frame
    void Update ()
    {

    }
}
