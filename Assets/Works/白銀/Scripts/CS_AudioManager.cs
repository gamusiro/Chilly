using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AudioPack
{
    public string m_label = "";       // 呼び出し名
    public AudioClip m_clip = null;      // オーディオクリップ
    public float m_volume = 1.0f;    // 個々のボリューム設定
}



public class CS_AudioManager : CS_SingletonMonoBehaviour<CS_AudioManager>
{
    [SerializeField, Header("音源データ")]
    List<AudioPack> m_listPack = new List<AudioPack>();

    private Dictionary<string, AudioSource> m_dictAudioSource = new Dictionary<string, AudioSource>();


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
                return i;
        }

        return -1;
    }

    /// <summary>
    /// オーディオを再生する
    /// </summary>
    /// <param name="index"></param>
    public void PlayAudio(string labelName)
    {
        // データがなければ
        if (!m_dictAudioSource.ContainsKey(labelName))
            RegisterDictionary(labelName);

        // 再生処理
        m_dictAudioSource[labelName].Play();
    }

    /// <summary>
    /// オーディオソースの取得
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
    /// ディクショナリに登録する処理
    /// </summary>
    void RegisterDictionary(string labelName)
    {
        int index = GetListIndex(labelName);

        // 不当な値
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
