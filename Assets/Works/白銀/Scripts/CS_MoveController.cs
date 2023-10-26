using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_MoveController : MonoBehaviour
{
    [SerializeField, CustomLabel("�O�i���x")]
    float m_moveVel;

    Vector3 m_vecVel;

    /// <summary>
    /// ����������
    /// </summary>
    void Start()
    {
        m_vecVel = new Vector3(0.0f, 0.0f, m_moveVel);
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    void Update()
    {
        transform.position -= m_vecVel * Time.deltaTime;
    }

    /// <summary>
    /// �ړ����x�̎擾
    /// </summary>
    /// <returns></returns>
    public float GetMoveVel()
    {
        return m_moveVel;
    }
}
