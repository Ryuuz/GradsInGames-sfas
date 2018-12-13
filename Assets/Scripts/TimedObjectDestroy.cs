using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedObjectDestroy : MonoBehaviour
{
    // The lifetime of the bullet
    [SerializeField]
    float m_Lifetime = 5.0f;

	// Update is called once per frame
	void Update ()
    {
        m_Lifetime -= Time.deltaTime;
        if(m_Lifetime < 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
