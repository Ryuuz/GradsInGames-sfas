using UnityEngine;

public class MovementController : MonoBehaviour
{
    // The gravity strength
    [SerializeField]
    public float m_Gravity = 60.0f;

    // The maximum speed the character can fall
    [SerializeField]
    public float m_MaxFallSpeed = 20.0f;

    // The character's jump height
    [SerializeField]
    public float m_JumpHeight = 4.0f;

    // --------------------------------------------------------------

    // The charactercontroller of the player
    private CharacterController m_CharacterController;

    // The current movement direction in x & z.
    private Vector3 m_MovementDirection = Vector3.zero;

    // The character's movement speed at a given time
    private float m_MovementSpeed = 0.0f;

    // The current vertical / falling speed
    private float m_VerticalSpeed = 0.0f;

    // The current movement offset
    private Vector3 m_CurrentMovementOffset = Vector3.zero;

    // --------------------------------------------------------------

    void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
    }

    void ApplyGravity()
    {
        // Apply gravity
        m_VerticalSpeed -= m_Gravity * Time.deltaTime;

        // Make sure the character doesn't fall any faster than m_MaxFallSpeed.
        m_VerticalSpeed = Mathf.Max(m_VerticalSpeed, -m_MaxFallSpeed);
        m_VerticalSpeed = Mathf.Min(m_VerticalSpeed, m_MaxFallSpeed);
    }

    void Update()
    {
        // Update jumping input and apply gravity
        ApplyGravity();

        // Calculate actual motion
        m_CurrentMovementOffset = (m_MovementDirection * m_MovementSpeed + new Vector3(0, m_VerticalSpeed, 0)) * Time.deltaTime;

        // Move character
        m_CharacterController.Move(m_CurrentMovementOffset);
    }

    // Rotate character to face the direction it is moving in
    public void RotateCharacter()
    {
        Quaternion lookRotation = Quaternion.LookRotation(m_MovementDirection);
        if (transform.rotation != lookRotation)
        {
            transform.rotation = lookRotation;
        }
    }

    // Make the character jump if grounded
    public void Jump()
    {
        if(m_CharacterController.isGrounded)
        {
            m_VerticalSpeed = Mathf.Sqrt(m_JumpHeight * m_Gravity);
        }
    }

    // Set the movement speed of the character
    public void SetHorizontalSpeed(float hSpeed)
    {
        m_MovementSpeed = hSpeed;
    }

    // Set direction the character should move in
    public void SetDirection(Vector3 direction)
    {
        m_MovementDirection = direction;
    }
}
