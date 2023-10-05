using JetBrains.Rider.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ControlGravity : MonoBehaviour
{
    [SerializeField]
    [Range(0.0f, 100.0f)]
    float m_yVel;

    Rigidbody m_rigidBody;
    Vector3 m_gravity;

    /// <summary>
    /// ����������
    /// </summary>
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_gravity = new Vector3(0.0f, -m_yVel, 0.0f);
    }

    /// <summary>
    /// �C���X�y�N�^�[����̒l�ύX
    /// </summary>
    private void OnValidate()
    {
        m_gravity = new Vector3(0.0f, -m_yVel, 0.0f);
    }

    /// <summary>
    /// ���t���[���X�V
    /// </summary>
    private void FixedUpdate()
    {
        m_rigidBody.AddForce(m_gravity, ForceMode.Acceleration);
    }
}
