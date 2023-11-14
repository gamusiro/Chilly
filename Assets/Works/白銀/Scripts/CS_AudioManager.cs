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
    #region �C���X�y�N�^�p�ϐ�

    // BGM�f�[�^
    [SerializeField, Header("BGM�f�[�^")]
    List<AudioPack> m_bgmPack = new List<AudioPack>();

    // SE�f�[�^
    [SerializeField, Header("SE�f�[�^")]
    List<AudioPack> m_sePack = new List<AudioPack>();

    #endregion

    #region �����ϐ��p

    // BGM�p�̃I�[�f�B�I�\�[�X
    AudioSource m_bgmSource;

    // SE�p�I�[�f�B�I�\�[�X
    AudioSource[] m_seSources;

    const int c_sePlayNum = 5;
    int m_bgmCurrentIndex = 0;
    int m_seSourceIndex = 0;

    #endregion

    #region ���J�p�ϐ�

    // BGM�̎���(���ݎ���)
    private float m_currentlyTime;
    public float TimeBGM
    {
        get { return m_currentlyTime; }
    }

    // BGM�̒���
    private float m_audioLength;
    public float LengthBGM
    {
        get { return m_audioLength; }
    }

    /// <summary>
    /// �g�[�^���̃{�����[��
    /// </summary>
    private float m_masterVolume;
    public float MasterVolume
    {
        set 
        {
            m_masterVolume = Mathf.Clamp01(value);
            m_bgmSource.volume = m_bgmPack[m_bgmCurrentIndex].m_volume * m_masterVolume;
        }
        get { return m_masterVolume; }
    }

    #endregion

    /// <summary>
    /// ����������
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        // BGM�p�̃R���|�[�l���g����
        m_bgmSource = new AudioSource();
        m_bgmSource = gameObject.AddComponent<AudioSource>();

        // SE�p�̃R���|�[�l���g����
        m_seSources = new AudioSource[c_sePlayNum];
        for(int i = 0; i < c_sePlayNum; ++i)
            m_seSources[i] = gameObject.AddComponent<AudioSource>();
    }

    /// <summary>
    /// �X�V����
    /// </summary>
    protected void FixedUpdate()
    {
        m_currentlyTime = m_bgmSource.time;
    }

    /// <summary>
    /// �I�[�f�B�I���Đ�����
    /// </summary>
    /// <param name="index"></param>
    public void PlayAudio(string labelName, bool bgm = false)
    {
        AudioPack pack = null;

        if(bgm)
        {// BGM�̍Đ��ł����
            int index = GetBGMIndex(labelName);
            if (index < 0)
            {
                Debug.LogError(labelName + "��BGM�͑��݂��Ă��܂���");
                return;
            }

            m_bgmCurrentIndex = index;
            pack = m_bgmPack[index];

            // BGM�̒������擾
            m_audioLength = pack.m_clip.length;

            // BGM�f�[�^���\�[�X�ɃZ�b�g����
            m_bgmSource.playOnAwake = false;
            m_bgmSource.clip = pack.m_clip;
            m_bgmSource.volume = pack.m_volume * m_masterVolume;
            m_bgmSource.Play();
        }
        else
        {// SE�̍Đ��ł����
            int index = GetSEIndex(labelName);
            if (index < 0)
            {
                Debug.LogError(labelName + "��SE�͑��݂��Ă��܂���");
                return;
            }

            pack = m_sePack[index];

            // SE�f�[�^���\�[�X�ɃZ�b�g����
            m_seSources[m_seSourceIndex].playOnAwake = false;
            m_seSources[m_seSourceIndex].PlayOneShot(pack.m_clip, pack.m_volume * m_masterVolume);

            m_seSourceIndex = (m_seSourceIndex + 1) % c_sePlayNum;
        }
    }

    /// <summary>
    /// �C���f�b�N�X���擾����(BGM)
    /// </summary>
    /// <param name="labelName"></param>
    /// <returns></returns>
    private int GetBGMIndex(string labelName)
    {
        for (int i = 0;i < m_bgmPack.Count; ++i) 
        {
            if (m_bgmPack[i].m_label == labelName)
                return i;
        }

        return -1;
    }

    /// <summary>
    /// �C���f�b�N�X���擾����(SE)
    /// </summary>
    /// <param name="labelName"></param>
    /// <returns></returns>
    private int GetSEIndex(string labelName)
    {
        for (int i = 0; i < m_sePack.Count; ++i)
        {
            if (m_sePack[i].m_label == labelName)
                return i;
        }

        return -1;
    }
}
