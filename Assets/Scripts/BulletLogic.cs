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
        //GetComponent<Rigidbody>().AddForce(new Vector3(1.0f, 0.0f, 0.0f));
    }

    // Update is called once per frame
    void Update ()
    {

    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {

        }
    }*/
}
