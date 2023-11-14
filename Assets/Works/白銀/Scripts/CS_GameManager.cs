using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
                CS_AudioManager.Instance.PlayAudio("GameAudio", true);
            });
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    private void FixedUpdate()
    {
        Fade.STATE state = m_fade.GetState();

        if (state == Fade.STATE.NONE)
            StateNone();
        else if (state == Fade.STATE.IN)
            StateIn();
        else
            StateOut();
    }

    /// <summary>
    /// �t�F�[�h��Ԃł͂Ȃ��Ƃ�
    /// </summary>
    void StateNone()
    {
        if (CS_AudioManager.Instance.TimeBGM >= CS_AudioManager.Instance.LengthBGM - 1.0f)
        {
            m_fade.FadeOut(m_setFadeTime, 
                () => {
                    CS_AudioManager.Instance.MasterVolume = 0.0f;
                    SceneManager.LoadScene("Title");
                });
        }
    }

    /// <summary>
    /// �t�F�[�h�C��(���ʂ̕ύX���s��)
    /// </summary>
    void StateIn()
    {
        float vol = 1.0f - m_fade.GetRange();
        CS_AudioManager.Instance.MasterVolume = (vol);
    }

    void StateOut()
    {
        float vol = 1.0f - m_fade.GetRange();
        CS_AudioManager.Instance.MasterVolume = (vol);
    }
}
