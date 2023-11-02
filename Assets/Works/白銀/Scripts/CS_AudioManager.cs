using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;

[Serializable]
public class AudioPack
{
    public string       m_label = "";    // �Ăяo����
    public AudioClip    m_clip = null;     // �I�[�f�B�I�N���b�v
    public float        m_volume = 1.0f;   // �X�̃{�����[���ݒ�
}



public class CS_AudioManager : CS_SingletonMonoBehaviour<CS_AudioManager>
{
    [SerializeField, Header("�����f�[�^")]
    List<AudioPack> m_listPack = new List<AudioPack>();

    Dictionary<string, AudioSource> m_dictAudio;

    /// <summary>
    /// ����������
    /// </summary>
    private void Start()
    {
        m_dictAudio = new Dictionary<string, AudioSource>();
        //m_bgmSource = new AudioSource();
        //m_bgmSource = gameObject.AddComponent<AudioSource>();
    }


    /// <summary>
    /// �I�[�f�B�I�f�[�^�̃C���f�b�N�X���擾����
    /// </summary>
    /// <param name="labelName">���x����</param>
    /// <returns></returns>
    private int GetListIndex(string labelName)
    {
        for (int i = 0; i < m_listPack.Count; ++i)
        {
            if (m_listPack[i].m_label.Equals(labelName))
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// �I�[�f�B�I���Đ�����
    /// </summary>
    /// <param name="index"></param>
    public void PlayAudio(string labelName)
    {
        // �I�[�f�B�I�f�[�^�̏����擾
        int index = GetListIndex(labelName);

        // �����Ȓl
        if (index < 0 || index >= m_listPack.Count)
            return;

        // �I�[�f�B�I�\�[�X�����邩�ǂ���
        if (!m_dictAudio.ContainsKey(labelName))
        {
            AudioSource source = new AudioSource();
            source = gameObject.AddComponent<AudioSource>();
            m_dictAudio.Add(labelName, source);
        }

        // �N���b�v���Ȃ����
        if(m_dictAudio[labelName].clip == null)
        {
            m_dictAudio[labelName].clip = m_listPack[index].m_clip;
            m_dictAudio[labelName].volume = m_listPack[index].m_volume;
        }

        // �Đ�����Ă��Ȃ���΁A�Đ�����
        if (!m_dictAudio[labelName].isPlaying)
        {
            m_dictAudio[labelName].Play();
        }
    }

    /// <summary>
    /// �I�[�f�B�I�f�[�^�̎擾
    /// </summary>
    /// <param name="labelName"></param>
    /// <returns></returns>
    public AudioSource GetAudioSource(string labelName)
    {
        if (!m_dictAudio.ContainsKey(labelName))
        {
            AudioSource source = new AudioSource();
            source = gameObject.AddComponent<AudioSource>();
            m_dictAudio.Add(labelName, source);
        }

        return m_dictAudio[labelName];
    }
}
