using System;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    [SerializeField] private Vector3 m_direction;
    [SerializeField] private float m_speed;
    [SerializeField] private float m_maxDistance;

    private Vector3 m_startPosition;
    private Vector3 m_endPosition;
    private Vector3 m_targetPosition;
    private int m_signedDirection = 1;
    private bool m_canMoveAutomatically = true;
    
    
    private void Start()
    {
        m_startPosition = transform.position;
        m_endPosition = transform.position + m_direction * m_maxDistance;
        m_targetPosition = m_endPosition;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            m_canMoveAutomatically = false;
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            m_canMoveAutomatically = true;
        }
        if (m_canMoveAutomatically)
        {
            Move();
        }
    }

    private void Move()
    {
        transform.position += m_direction * m_speed * Time.deltaTime * m_signedDirection;
        if (Vector3.Distance(transform.position, m_targetPosition) <= 0.1f)
        {
            m_signedDirection *= -1;
            m_targetPosition = m_targetPosition == m_endPosition ? m_startPosition : m_endPosition;
        }
    }
}
