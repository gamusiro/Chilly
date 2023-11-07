using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AudioPack
{
    public string m_label = "";       // �Ăяo����
    public AudioClip m_clip = null;      // �I�[�f�B�I�N���b�v
    public float m_volume = 1.0f;    // �X�̃{�����[���ݒ�
}



public class CS_AudioManager : CS_SingletonMonoBehaviour<CS_AudioManager>
{
    [SerializeField, Header("�����f�[�^")]
    List<AudioPack> m_listPack = new List<AudioPack>();

    private Dictionary<string, AudioSource> m_dictAudioSource = new Dictionary<string, AudioSource>();


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
                return i;
        }

        return -1;
    }

    /// <summary>
    /// �I�[�f�B�I���Đ�����
    /// </summary>
    /// <param name="index"></param>
    public void PlayAudio(string labelName)
    {
        // �f�[�^���Ȃ����
        if (!m_dictAudioSource.ContainsKey(labelName))
            RegisterDictionary(labelName);

        // �Đ�����
        m_dictAudioSource[labelName].Play();
    }

    /// <summary>
    /// �I�[�f�B�I�\�[�X�̎擾
    /// </summary>
    /// <param name="labelName"></param>
    /// <returns></returns>
    public AudioSource GetAudioSource(string labelName)
    {
        if (!m_dictAudioSource.ContainsKey(labelName))
            RegisterDictionary(labelName);

        return m_dictAudioSource[labelName];
    }

    /// <summary>
    /// �f�B�N�V���i���ɓo�^���鏈��
    /// </summary>
    void RegisterDictionary(string labelName)
    {
        int index = GetListIndex(labelName);

        // �s���Ȓl
        if (index < 0)
            return;

        AudioSource source  = new AudioSource();
        source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake  = false;
        source.clip         = m_listPack[index].m_clip;
        source.volume       = m_listPack[index].m_volume;

        m_dictAudioSource.Add(labelName, source);
    }
}
