using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevitationItem : MonoBehaviour
{
    [SerializeField] private float m_heightRange = 1.3f;
    [SerializeField] private float m_speed = 1.5f;

    [SerializeField] private float m_rotationSpeed = 1.5f;
    [SerializeField] private bool m_rotateX = false;
    [SerializeField] private bool m_rotateY = false;
    [SerializeField] private bool m_rotateZ = false;

    private float m_StartHeight;

    void Start()
    {
        m_StartHeight = transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, (float)(m_StartHeight + Mathf.Sin(Time.time * m_speed) * m_heightRange / 2.0), transform.position.z);

        if (m_rotateX)
            transform.Rotate(m_rotationSpeed, 0, 0);
        if (m_rotateY)
            transform.Rotate(0, m_rotationSpeed, 0);
        if (m_rotateZ)
            transform.Rotate(0, 0, m_rotationSpeed);
    }
}
