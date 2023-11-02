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
    public string       m_label = "";    // 呼び出し名
    public AudioClip    m_clip = null;     // オーディオクリップ
    public float        m_volume = 1.0f;   // 個々のボリューム設定
}



public class CS_AudioManager : CS_SingletonMonoBehaviour<CS_AudioManager>
{
    [SerializeField, Header("音源データ")]
    List<AudioPack> m_listPack = new List<AudioPack>();

    Dictionary<string, AudioSource> m_dictAudio;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        m_dictAudio = new Dictionary<string, AudioSource>();
        //m_bgmSource = new AudioSource();
        //m_bgmSource = gameObject.AddComponent<AudioSource>();
    }


    /// <summary>
    /// オーディオデータのインデックスを取得する
    /// </summary>
    /// <param name="labelName">ラベル名</param>
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
    /// オーディオを再生する
    /// </summary>
    /// <param name="index"></param>
    public void PlayAudio(string labelName)
    {
        // オーディオデータの情報を取得
        int index = GetListIndex(labelName);

        // 無効な値
        if (index < 0 || index >= m_listPack.Count)
            return;

        // オーディオソースがあるかどうか
        if (!m_dictAudio.ContainsKey(labelName))
        {
            AudioSource source = new AudioSource();
            source = gameObject.AddComponent<AudioSource>();
            m_dictAudio.Add(labelName, source);
        }

        // クリップがなければ
        if(m_dictAudio[labelName].clip == null)
        {
            m_dictAudio[labelName].clip = m_listPack[index].m_clip;
            m_dictAudio[labelName].volume = m_listPack[index].m_volume;
        }

        // 再生されていなければ、再生する
        if (!m_dictAudio[labelName].isPlaying)
        {
            m_dictAudio[labelName].Play();
        }
    }

    /// <summary>
    /// オーディオデータの取得
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
