using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // The character's running speed
    public float m_RunSpeed = 5.0f;

    // --------------------------------------------------------------

    private MovementController m_MovementController;

    // --------------------------------------------------------------

    void Awake()
    {
        m_MovementController = GetComponent<MovementController>();
    }

    void UpdateMovementState()
    {
        // Get Player's movement input and determine direction and set run speed
        float horizontalInput = Input.GetAxisRaw("Horizontal_P1");
        float verticalInput = Input.GetAxisRaw("Vertical_P1");

        m_MovementController.SetDirection(new Vector3(horizontalInput, 0, verticalInput));
        m_MovementController.SetHorizontalSpeed(m_RunSpeed);
    }

    void UpdateJumpState()
    {
        // Character can jump when standing on the ground
        if (Input.GetButtonDown("Jump_P1"))
        {
            m_MovementController.Jump();
        }
    }

    void Update()
    {
        // Update movement input
        UpdateMovementState();

        // Update jumping input
        UpdateJumpState();

        // Rotate the character towards the mouse cursor
        RotateCharacterTowardsMouseCursor();
    }

    void RotateCharacterTowardsMouseCursor()
    {
        // Find the direction the player should look in
        Vector3 mousePosInScreenSpace = Input.mousePosition;
        Vector3 playerPosInScreenSpace = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 directionInScreenSpace = mousePosInScreenSpace - playerPosInScreenSpace;

        // Rotate the angle between new and old direction
        float angle = Mathf.Atan2(directionInScreenSpace.y, directionInScreenSpace.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(-angle + 90.0f, Vector3.up);
    }
}
