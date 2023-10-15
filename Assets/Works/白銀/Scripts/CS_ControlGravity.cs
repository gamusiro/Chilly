using JetBrains.Rider.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ControlGravity : MonoBehaviour
{
    [SerializeField, CustomLabel("�d�͂̋���")]
    [Range(1.0f, 1000.0f)]
    float m_strength;

    Rigidbody m_rigidBody;
    Vector3 m_gravity;

    /// <summary>
    /// ����������
    /// </summary>
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_gravity = new Vector3(0.0f, -m_strength, 0.0f);
    }

    /// <summary>
    /// �C���X�y�N�^�[����̒l�ύX
    /// </summary>
    private void OnValidate()
    {
        m_gravity = new Vector3(0.0f, -m_strength, 0.0f);
    }

    /// <summary>
    /// ���t���[���X�V
    /// </summary>
    private void FixedUpdate()
    {
        m_rigidBody.AddForce(m_gravity, ForceMode.Acceleration);
    }
}
