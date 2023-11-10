using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
class ChangeTimeData
{
    public string m_name;
    public float m_time;
}


public class CS_ChangeCameraTimes : MonoBehaviour
{
    #region �C���X�y�N�^�p�ϐ�

    // �؂�ւ��^�C��
    [SerializeField]
    List<ChangeTimeData> m_list;

    #endregion

    #region �����p�ϐ�

    // �I�[�f�B�I�\�[�X
    AudioSource m_audioSource;

    // �g�p����C���f�b�N�X
    int m_index;

    #endregion

    /// <summary>
    /// ����������
    /// </summary>
    void Start()
    {
        m_audioSource = CS_AudioManager.Instance.GetAudioSource("GameAudio");

        // �S�[���n�_�̃f�[�^���Ō�ɓ����
        ChangeTimeData finish = new ChangeTimeData();
        finish.m_time = m_audioSource.clip.length;
        finish.m_name = "Back";
        m_list.Add(finish);

        m_index = 0;
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    void Update()
    {
        if (m_index < m_list.Count)
        {
            // �ݒ肵���f�[�^
            if (m_list[m_index].m_time <= m_audioSource.time)
            {
                CS_MoveController.GetObject("Player").GetComponent<CS_Player>().SetUsingCamera();
                m_index++;
            }
        }
    }
}
