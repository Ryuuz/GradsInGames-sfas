using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBulletLogic : BulletLogic
{
    // The Explosion ParticleEmitter Prefab
    [SerializeField]
    GameObject m_ExplosionPE;

    protected override void Explode()
    {
        if (m_ExplosionPE)
        {
            Instantiate(m_ExplosionPE, transform.position, transform.rotation);
        }
    }
}
