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

    // HP�f�[�^
    [SerializeField, CustomLabel("HP")]
    HP m_hp;

    // �t�F�[�h
    [SerializeField, CustomLabel("�S�[���̃V�[����")]
    string m_nextSceneName;

    // �Q�[���I�[�o�[�V�[��
    [SerializeField, CustomLabel("�Q�[���I�[�o�[�̃V�[����")]
    string m_gameoverSceneName;



    #endregion

    #region �����p�ϐ�

    static bool m_onTutorial;

    #endregion

    #region ���J�p�ϐ�

    public static bool GetOnTutorial
    {
        get { return m_onTutorial; }
    }

    #endregion


    /// <summary>
    /// ����������
    /// </summary>
    void Start()
    {
        string audioName = CS_LoadNotesFile.GetFolderName();
        CS_AudioManager.Instance.PlayAudio(audioName, true);

        m_fade.FadeIn(m_setFadeTime,
          () =>
          {
              CS_AudioManager.Instance.MasterVolume = 1.0f;
              CS_MoveController.MoveStart();
          });
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    private void Update()
    {
        Fade.STATE state = m_fade.GetState();

        if (state == Fade.STATE.NONE)
            StateNone();
        else
        {
            float tmp = m_fade.GetRange();
            CS_AudioManager.Instance.FadeVolume(tmp);
        }
    }

    /// <summary>
    /// �t�F�[�h��Ԃł͂Ȃ��Ƃ�
    /// </summary>
    void StateNone()
    {
        float correctionValue = 3.5f;
        // �Q�[���N���A���̑J��
        if (CS_AudioManager.Instance.TimeBGM >= CS_AudioManager.Instance.LengthBGM - correctionValue - m_setFadeTime) 
        {
            m_fade.FadeOut(m_setFadeTime, 
                () => {
                    CS_AudioManager.Instance.MasterVolume = 0.0f;
                    CS_AudioManager.Instance.StopBGM();
                    SceneManager.LoadScene(m_nextSceneName);
                });
        }

        // �Q�[���I�[�o�[���̑J��
        if(m_hp.Die)
        {
            m_fade.FadeOut(m_setFadeTime,
                () => {
                    CS_AudioManager.Instance.MasterVolume = 0.0f;
                    CS_AudioManager.Instance.StopBGM();
                    SceneManager.LoadScene(m_gameoverSceneName);
                });
        }
    }

    public static void SetTutorial(bool on)
    {
        m_onTutorial = on;
    }
}
