using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CS_GameFinisher : MonoBehaviour
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

    // �V�[���ǂݍ��ݏ�
    AsyncOperation m_sceneLoadState;

    // �V�[���J�ڒ����ǂ���
    public bool m_fadeRun = false;

    #endregion

    AudioSource m_audioSource;

    /// <summary>
    /// ����������
    /// </summary>
    void Start()
    {
        m_audioSource = CS_AudioManager.Instance.GetAudioSource("GameAudio");
        m_fadeRun = false;
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    void Update()
    {
        if (m_audioSource.time >= m_audioSource.clip.length)
        {
            if (!m_fadeRun)
            {
                m_fadeRun = true;
                Invoke(nameof(SceneLoad), 4.0f);
            }
        }
    }

    /// <summary>
    /// �V�[���̓ǂݍ���
    /// </summary>
    private void SceneLoad()
    {
        m_sceneLoadState = SceneManager.LoadSceneAsync(m_sceneName);
        m_sceneLoadState.allowSceneActivation = false;

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


