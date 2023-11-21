using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Shadow : MonoBehaviour
{
    #region �C���X�y�N�^�p�ϐ�

    // �������̃X�P�[��
    [SerializeField, CustomLabel("�����X�P�[��")]
    Vector3 m_originScale;

    // �ŏI�I�ȃX�P�[��
    [SerializeField, CustomLabel("�ŏI�X�P�[��")]
    Vector3 m_targetScale;

    // �p�[�t�F�N�g�^�C���̉��b�O����
    [SerializeField, CustomLabel("���b�O����")]
    float m_befTime;

    #endregion

    #region �����p�ϐ�

    // �p�[�t�F�N�g�^�C��
    float m_perfTime;

    #endregion

    /// <summary>
    /// ����������
    /// </summary>
    void Start()
    {
        transform.localScale = m_originScale;
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    void FixedUpdate()
    {
        float tmp = Mathf.Clamp(CS_AudioManager.Instance.TimeBGM - (m_perfTime - m_befTime), 0.0f, m_befTime);
        tmp /= m_befTime;

        transform.localScale = Vector3.Lerp(m_originScale, m_targetScale, tmp);
    }

    /// <summary>
    /// �p�[�t�F�N�g�^�C���̐ݒ�
    /// </summary>
    /// <param name="time"></param>
    public void SetPerfectTime(float time)
    {
        m_perfTime = time;
    }
}
