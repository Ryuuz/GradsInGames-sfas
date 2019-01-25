using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // The Camera Target
    public Transform m_PlayerTransform;

    // The Z Distance from the Camera Target
    public float m_CameraDistanceZ = 10.0f;

    // --------------------------------------------------------------

    void Update ()
    {
        transform.position = new Vector3(m_PlayerTransform.position.x, transform.position.y, m_PlayerTransform.position.z - m_CameraDistanceZ);
	}
}
