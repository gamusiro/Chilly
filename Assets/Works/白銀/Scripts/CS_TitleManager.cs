using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS_TitleManager : MonoBehaviour
{
    #region �C���X�y�N�^�p�ϐ�

    // �t�F�[�h
    [SerializeField, CustomLabel("�t�F�[�h")]
    Fade m_fade;

    // �t�F�[�h
    [SerializeField, CustomLabel("�t�F�[�h����")]
    float m_setFadeTime;

    // �ǂݍ��ރV�[���̖��O
    [SerializeField, CustomLabel("�V�[����")]
    string m_sceneName;

    #endregion

    #region �����p�ϐ�

    // InputSystem
    IA_Player m_inputAction;

    #endregion

    /// <summary>
    /// ����������
    /// </summary>
    void Start()
    {
        CS_AudioManager.Instance.PlayAudio("TitleAudio", true);

        m_inputAction = new IA_Player();
        m_inputAction.Enable();

        // �t�F�[�h�C���̌�A���ʂ��}�b�N�X�ɂ���
        m_fade.FadeIn(1.0f, () => { CS_AudioManager.Instance.MasterVolume = 1.0f; });
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    void FixedUpdate()
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
        if (m_inputAction.Title.ToGameScene.triggered)
        {
            CS_AudioManager.Instance.PlayAudio("Jump");
            m_fade.FadeOut(m_setFadeTime, () => 
            { 
                CS_AudioManager.Instance.MasterVolume = 0.0f;
                CS_AudioManager.Instance.StopBGM();
                SceneManager.LoadScene(m_sceneName);
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
