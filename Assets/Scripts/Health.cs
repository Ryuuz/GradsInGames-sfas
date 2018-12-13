using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // The total health of this unit
    [SerializeField]
    int m_Health = 100;

    public void DoDamage(int damage)
    {
        m_Health -= damage;

        if(m_Health < 0)
        {
            Destroy(gameObject);
        }
    }

    public bool IsAlive()
    {
        return m_Health > 0;
    }
}
