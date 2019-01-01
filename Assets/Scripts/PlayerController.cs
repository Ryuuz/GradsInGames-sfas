using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    // --------------------------------------------------------------

    // The character's running speed
    [SerializeField]
    float m_RunSpeed = 5.0f;

    MovementController m_MovementController;

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

    // Update is called once per frame
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
        Vector3 mousePosInScreenSpace = Input.mousePosition;
        Vector3 playerPosInScreenSpace = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 directionInScreenSpace = mousePosInScreenSpace - playerPosInScreenSpace;

        float angle = Mathf.Atan2(directionInScreenSpace.y, directionInScreenSpace.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(-angle + 90.0f, Vector3.up);
    }
}
