using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    // The lifetime of the bullet
    [SerializeField]
    float m_BulletLifeTime = 2.0f;

    // The speed of the bullet
    [SerializeField]
    protected float m_BulletSpeed = 15.0f;

    // The damage of the bullet
    [SerializeField]
    int m_Damage = 20;

    bool m_Active = true;

    // Use this for initialization
    void Start()
    {
        // Add velocity to the bullet
        GetComponent<Rigidbody>().velocity = -transform.up * m_BulletSpeed;
    }

    // Update is called once per frame
    void Update ()
    {
        m_BulletLifeTime -= Time.deltaTime;
        if(m_BulletLifeTime < 0.0f)
        {
            Impact();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!m_Active)
        {
            return;
        }

        Health health = collision.gameObject.GetComponent<Health>();
        if(health)
        {
            health.DoDamage(m_Damage);
        }

        Impact();
        m_Active = false;
    }

    void Impact()
    {
        Explode();
        Destroy(gameObject);
    }

    protected virtual void Explode()
    {
    }
}
