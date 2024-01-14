using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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

    [SerializeField, CustomLabel("�J�����}�l�[�W��")]
    TitleCameraPhaseManager m_manager;

    //// �t�@�[�X�g�e�C�N�p�l��UI
    //[SerializeField, CustomLabel("FirstTake")]
    //GameObject m_firstTakePanel;

    //// ���j���[�e�C�N�p�l��UI
    //[SerializeField, CustomLabel("MenuTake")]
    //GameObject m_menuTakePanel;

    #endregion

    #region �����p�ϐ�

    // InputSystem
    PlayerInput m_inputAction;


    enum TAKE
    {
        FIRST,
        MENU
    };

    TAKE m_take;

    #endregion

    /// <summary>
    /// ����������
    /// </summary>
    void Start()
    {
        Cursor.visible = false;
        CS_AudioManager.Instance.PlayAudio("TitleAudio", true);

        m_inputAction = GetComponent<PlayerInput>();

        // �t�F�[�h�C���̌�A���ʂ��}�b�N�X�ɂ���
        m_fade.FadeIn(1.0f, () => { CS_AudioManager.Instance.MasterVolume = 1.0f; });
    
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    void Update()
    {
        Fade.STATE state = m_fade.GetState();

        if (state == Fade.STATE.NONE)
            StateNone();
        else
        {
            float vol = 1.0f - m_fade.GetRange();
            CS_AudioManager.Instance.MasterVolume = (vol);
        }
    }

    /// <summary>
    /// �t�F�[�h��Ԃł͂Ȃ��Ƃ�
    /// </summary>
    void StateNone()
    {
        // ����{�^���������ꂽ��
        if (m_inputAction.currentActionMap["Commit"].triggered)
        {
            CS_AudioManager.Instance.PlayAudio("Commit");
            m_manager.NextCamera();
        } 
    }

    /// <summary>
    /// ���̃V�[���ֈړ����鏈��
    /// </summary>
    void NextScene()
    {
        m_fade.FadeOut(m_setFadeTime, () =>
        {
            CS_AudioManager.Instance.MasterVolume = 0.0f;
            CS_AudioManager.Instance.StopBGM();
            SceneManager.LoadScene(m_sceneName);
        });
    }
}
