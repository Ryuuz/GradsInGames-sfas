using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCrateLogic : MonoBehaviour
{
    [SerializeField]
    int m_BulletAmmo = 50;

    [SerializeField]
    int m_GrenadeAmmo = 10;

    void OnTriggerEnter(Collider other)
    {
        GunLogic gunLogic = other.GetComponentInChildren<GunLogic>();
        if(gunLogic)
        {
            gunLogic.AddAmmo(m_BulletAmmo, m_GrenadeAmmo);
            Destroy(gameObject);
        }
    }
}
