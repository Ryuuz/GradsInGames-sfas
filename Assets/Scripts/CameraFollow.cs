using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // The Camera Target
    [SerializeField]
    Transform m_PlayerTransform;

    // The Z Distance from the Camera Target
    [SerializeField]
    float m_CameraDistanceZ = 8.0f;
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3(m_PlayerTransform.position.x, transform.position.y, m_PlayerTransform.position.z - m_CameraDistanceZ);
	}
}
