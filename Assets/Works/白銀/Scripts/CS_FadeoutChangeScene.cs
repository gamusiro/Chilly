using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS_FadeoutChangeScene : MonoBehaviour
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

    // �V�[���ǂݍ��ݏ�
    AsyncOperation m_sceneLoadState;

    // �V�[���J�ڒ����ǂ���
    bool m_fadeRun = false;

    #endregion

    /// <summary>
    /// ����������
    /// </summary>
    void Start()
    {
        m_inputAction = new IA_Player();
        m_inputAction.Enable();

        m_sceneLoadState = SceneManager.LoadSceneAsync(m_sceneName);
        m_sceneLoadState.allowSceneActivation = false;
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    void Update()
    {
        if (m_inputAction.Title.ToGameScene.triggered)
        {
            if(!m_fadeRun)
                FadeStart();
        }
    }

    /// <summary>
    /// �t�F�[�h�J�n
    /// </summary>
    void FadeStart()
    {
        m_fadeRun = true;
        m_fade.FadeIn(m_setFadeTime);
        Invoke(nameof(ChangeScene), m_setFadeTime);
    }

    /// <summary>
    /// �V�[�����؂�ւ�
    /// </summary>
    private void ChangeScene()
    {
        m_sceneLoadState.allowSceneActivation = true;
    }


}
