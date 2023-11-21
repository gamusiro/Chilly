using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_EnemyAttackSideBar : MonoBehaviour
{
    #region �����p�ϐ�

    // �w��ʒu�|�W�V����
    Vector3 m_targetPosition;

    // �^�C�~���O
    public float m_perfTime;

    // �ړ����x
    float m_vel;

    #endregion


    /// <summary>
    /// ����������
    /// </summary>
    void Start()
    {
        // �������ꂽ�Ƃ��̌��݃|�W�V�������擾
        
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    private void FixedUpdate()
    {
        Vector3 setPosition = m_targetPosition;
        setPosition.x = setPosition.x + (m_vel * (m_perfTime - CS_AudioManager.Instance.TimeBGM));
        transform.localPosition = setPosition;
    }

    public void SetLane(int lane, float offset = 0.0f, bool left = false)
    {
        const int laneNum = 5;

        m_targetPosition = Vector3.zero;
        m_targetPosition.x = -60.0f + 30.0f * (laneNum - lane);
        m_targetPosition.x += 30.0f * offset;

        m_vel = 200.0f;     // �E���痬��Ă���
        if (left)
            m_vel *= -1.0f; // �����痬��Ă���
    }
}
