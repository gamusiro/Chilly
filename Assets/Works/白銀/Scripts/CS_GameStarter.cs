using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_GameStarter : MonoBehaviour
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
        m_fade.FadeOut(m_setFadeTime);
        Invoke(nameof(GameStart), m_setFadeTime);
        //GameStart();
    }

    /// <summary>
    /// �Q�[���X�^�[�g
    /// </summary>
    void GameStart()
    {
        CS_AudioManager.Instance.PlayAudio("GameAudio");
        CS_MoveController.Instance.MoveStart();
        Destroy(gameObject);
    }
}
