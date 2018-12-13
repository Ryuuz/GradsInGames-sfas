using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    // --------------------------------------------------------------

    // The character's running speed
    [SerializeField]
    float m_MovementSpeed = 4.0f;

    // The gravity strength
    [SerializeField]
    float m_Gravity = 60.0f;

    // The maximum speed the character can fall
    [SerializeField]
    float m_MaxFallSpeed = 20.0f;

    // --------------------------------------------------------------

    // The charactercontroller of the player
    CharacterController m_CharacterController;

    // The current movement direction in x & z.
    Vector3 m_MovementDirection = Vector3.zero;

    // The current vertical / falling speed
    float m_VerticalSpeed = 0.0f;

    // The current movement offset
    Vector3 m_CurrentMovementOffset = Vector3.zero;

    // Whether the player is alive or not
    bool m_IsAlive = true;

    PlayerController m_PlayerController;
    Transform m_PlayerTransform;

    // --------------------------------------------------------------

    void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
    }

    // Use this for initialization
    void Start()
    {
        // Get Player information
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player)
        {
            m_PlayerController = player.GetComponent<PlayerController>();
            m_PlayerTransform = player.transform;
        }
        
    }

    void ApplyGravity()
    {
        // Apply gravity
        m_VerticalSpeed -= m_Gravity * Time.deltaTime;

        // Make sure we don't fall any faster than m_MaxFallSpeed.
        m_VerticalSpeed = Mathf.Max(m_VerticalSpeed, -m_MaxFallSpeed);
        m_VerticalSpeed = Mathf.Min(m_VerticalSpeed, m_MaxFallSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is dead update the respawn timer and exit update loop
        if (!m_IsAlive)
        {
            return;
        }

        float distance = Vector3.Distance(m_PlayerTransform.position, transform.position);

        // Aggro range
        if (distance < 15.0f)
        {
            m_MovementDirection = m_PlayerTransform.position - transform.position;
            m_MovementDirection.Normalize();
        }

        // Attack range
        if(distance < 2.0f)
        {
            // Knock back player
            m_PlayerController.AddForce((m_MovementDirection + new Vector3(0,2,0)) * 20.0f);
        }

        Debug.Log(distance);

        // Update jumping input and apply gravity
        ApplyGravity();

        // Calculate actual motion
        m_CurrentMovementOffset = (m_MovementDirection * m_MovementSpeed + new Vector3(0, m_VerticalSpeed, 0)) * Time.deltaTime;

        // Move character
        m_CharacterController.Move(m_CurrentMovementOffset);

        // Rotate the character in movement direction
        if(m_MovementDirection != Vector3.zero)
        {
            RotateCharacter(m_MovementDirection);
        }
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    void RotateCharacter(Vector3 movementDirection)
    {
        Quaternion lookRotation = Quaternion.LookRotation(movementDirection);
        if (transform.rotation != lookRotation)
        {
            transform.rotation = lookRotation;
        }
    }

    public void Die()
    {
        m_IsAlive = false;
    }
}
