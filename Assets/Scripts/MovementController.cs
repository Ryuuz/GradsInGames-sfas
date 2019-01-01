﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    // --------------------------------------------------------------

    // The gravity strength
    [SerializeField]
    float m_Gravity = 60.0f;

    // The maximum speed the character can fall
    [SerializeField]
    float m_MaxFallSpeed = 20.0f;

    // The character's jump height
    [SerializeField]
    float m_JumpHeight = 4.0f;

    // --------------------------------------------------------------

    // The charactercontroller of the player
    CharacterController m_CharacterController;

    // The current movement direction in x & z.
    Vector3 m_MovementDirection = Vector3.zero;

    // The character's movement speed
    [SerializeField]
    float m_MovementSpeed = 4.0f;

    // The current vertical / falling speed
    float m_VerticalSpeed = 0.0f;

    // The current movement offset
    Vector3 m_CurrentMovementOffset = Vector3.zero;

    // --------------------------------------------------------------

    void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
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
        // Update jumping input and apply gravity
        ApplyGravity();

        // Calculate actual motion
        m_CurrentMovementOffset = (m_MovementDirection * m_MovementSpeed + new Vector3(0, m_VerticalSpeed, 0)) * Time.deltaTime;

        // Move character
        m_CharacterController.Move(m_CurrentMovementOffset);
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    public void RotateCharacter()
    {
        Quaternion lookRotation = Quaternion.LookRotation(m_MovementDirection);
        if (transform.rotation != lookRotation)
        {
            transform.rotation = lookRotation;
        }
    }

    public void Jump()
    {
        if(m_CharacterController.isGrounded)
        {
            m_VerticalSpeed = Mathf.Sqrt(m_JumpHeight * m_Gravity);
        }
    }

    public void SetHorizontalSpeed(float hSpeed)
    {
        m_MovementSpeed = hSpeed;
    }

    public void SetDirection(Vector3 direction)
    {
        m_MovementDirection = direction;
    }
}