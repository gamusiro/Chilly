using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_EnemyAttackNotes : MonoBehaviour
{
    #region �����p�ϐ�

    // �w��ʒu�|�W�V����
    Vector3 m_targetPosition;

    // �^�C�~���O
    public float m_perfTime;
    
    #endregion


    /// <summary>
    /// ����������
    /// </summary>
    void Start()
    {
        // �������ꂽ�Ƃ��̌��݃|�W�V�������擾
        m_targetPosition = transform.position;
        m_targetPosition.z *= -1.0f;
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    private void FixedUpdate()
    {
        Vector3 setPosition = m_targetPosition;
        setPosition.z += 400.0f * 1.0f * (m_perfTime - CS_AudioManager.Instance.TimeBGM);
        transform.position = setPosition;
    }
}
