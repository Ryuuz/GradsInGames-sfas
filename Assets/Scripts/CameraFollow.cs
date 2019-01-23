using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // The Camera Target
    [SerializeField]
    public Transform m_PlayerTransform;

    // The Z Distance from the Camera Target
    [SerializeField]
    public float m_CameraDistanceZ = 8.0f;

    // --------------------------------------------------------------

    void Update ()
    {
        transform.position = new Vector3(m_PlayerTransform.position.x, transform.position.y, m_PlayerTransform.position.z - m_CameraDistanceZ);
	}
}
