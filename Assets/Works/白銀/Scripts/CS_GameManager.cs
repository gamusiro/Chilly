using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_GameManager : MonoBehaviour
{
    #region �C���X�y�N�^�p�ϐ�

    // �t�F�[�h
    [SerializeField, CustomLabel("�t�F�[�h")]
    Fade m_fade;

    // �t�F�[�h
    [SerializeField, CustomLabel("�t�F�[�h����")]
    float m_setFadeTime;

    #endregion

    /// <summary>
    /// ����������
    /// </summary>
    void Start()
    {
        m_fade.FadeIn(m_setFadeTime, 
            () => { 
                CS_AudioManager.Instance.MasterVolume = 1.0f;
                CS_MoveController.MoveStart();
            });

        CS_AudioManager.Instance.PlayAudio("GameAudio", true);
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    private void FixedUpdate()
    {
    }
}
