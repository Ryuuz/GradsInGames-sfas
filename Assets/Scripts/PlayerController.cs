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

    public void UpdateMovementState(Vector3 playerInput)
    {
        // Determine direction and set run speed
        m_MovementController.SetDirection(playerInput);
        m_MovementController.SetHorizontalSpeed(m_RunSpeed);
    }

    public void UpdateJumpState()
    {
        // Character can jump when standing on the ground
        m_MovementController.Jump();
    }

    public void RotateCharacterTowardsMouseCursor()
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
