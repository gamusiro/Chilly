using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

[Serializable]
public class AudioPack
{
    public string m_label = "";       // 呼び出し名
    public AudioClip m_clip = null;      // オーディオクリップ
    public float m_volume = 1.0f;    // 個々のボリューム設定
}



public class CS_AudioManager : CS_SingletonMonoBehaviour<CS_AudioManager>
{
    #region インスペクタ用変数

    // BGMデータ
    [SerializeField, Header("BGMデータ")]
    List<AudioPack> m_bgmPack = new List<AudioPack>();

    // SEデータ
    [SerializeField, Header("SEデータ")]
    List<AudioPack> m_sePack = new List<AudioPack>();

    #endregion

    #region 内部変数用

    // BGM用のオーディオソース
    AudioSource[] m_bgmSources;

    // SE用オーディオソース
    AudioSource[] m_seSources;

    // タイミングを保存
    Dictionary<string, float> m_times;

    const int c_sePlayNum = 5;
    const int c_mainBGMIndex = 0;
    const int c_fadeBGMIndex = 1;

    int m_bgmCurrentIndex = 0;
    int m_seSourceIndex = 0;

    // 再生開始時間と終了時間
    float m_loopStartTime;
    float m_loopEndTime;

    #endregion

    #region 公開用変数

    // BGMの時間(現在時間)
    private float m_currentlyTime;
    public float TimeBGM
    {
        get { return m_currentlyTime; }
    }

    // BGMの長さ
    private float m_audioLength;
    public float LengthBGM
    {
        get { return m_audioLength; }
    }

    /// <summary>
    /// トータルのボリューム
    /// </summary>
    private float m_masterVolume = 1.0f;
    public float MasterVolume
    {
        set 
        {
            m_masterVolume = Mathf.Clamp01(value);
            m_bgmSources[c_mainBGMIndex].volume = m_bgmPack[m_bgmCurrentIndex].m_volume * BGMVolume * m_masterVolume;
        }
        get { return m_masterVolume; }
    }

    /// <summary>
    /// BGMのボリューム
    /// </summary>
    private float m_bgmVolume = 0.1f;
    public float BGMVolume
    {
        set
        {
            m_bgmVolume = Mathf.Clamp01(value);
            m_bgmSources[c_mainBGMIndex].volume = m_bgmPack[m_bgmCurrentIndex].m_volume * m_bgmVolume * MasterVolume;
        }
        get { return m_bgmVolume; }
    }

    /// <summary>
    /// SEのボリューム
    /// </summary>
    private float m_seVolume = 0.1f;
    public float SEVolume
    {
        set
        {
            m_seVolume = Mathf.Clamp01(value);
        }
        get { return m_seVolume; }
    }

    #endregion

    /// <summary>
    /// 初期化処理
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        // BGM用のコンポーネント生成
        m_bgmSources = new AudioSource[2];
        m_bgmSources[c_mainBGMIndex] = gameObject.AddComponent<AudioSource>();
        m_bgmSources[c_fadeBGMIndex] = gameObject.AddComponent<AudioSource>();


        // SE用のコンポーネント生成
        m_seSources = new AudioSource[c_sePlayNum];
        for(int i = 0; i < c_sePlayNum; ++i)
            m_seSources[i] = gameObject.AddComponent<AudioSource>();

        m_times = new Dictionary<string, float>();
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    protected void Update()
    {
        m_currentlyTime = m_bgmSources[c_mainBGMIndex].time;

        // ループタイムが設定されていなければ
        if (m_loopEndTime < 0.0f)
            return;

        if(m_bgmSources[c_mainBGMIndex].time > m_loopEndTime)
        {
            m_bgmSources[c_mainBGMIndex].time = m_loopStartTime;
        }


        if (!m_bgmSources[c_fadeBGMIndex].isPlaying)
            return;

        m_bgmSources[c_fadeBGMIndex].volume = Mathf.Clamp01(m_bgmSources[c_fadeBGMIndex].volume - Time.deltaTime);
        if (m_bgmSources[c_fadeBGMIndex].volume <= 0.0f)
            m_bgmSources[c_fadeBGMIndex].Stop();
    }

    

    /// <summary>
    /// オーディオを再生する
    /// </summary>
    /// <param name="index"></param>
    public void PlayAudio(string labelName, bool bgm = false, float start = 0, float end = -1.0f)
    {
        AudioPack pack = null;

        if(bgm)
        {// BGMの再生であれば
            int index = GetBGMIndex(labelName);
            if (index < 0)
            {
                Debug.LogError(labelName + "のBGMは存在していません");
                return;
            }

            m_bgmCurrentIndex = index;
            pack = m_bgmPack[index];

            // ループエンドタイム
            m_loopEndTime = end;

            // BGMの長さを取得
            m_audioLength = pack.m_clip.length;

            // BGMデータをソースにセットする
            m_bgmSources[c_mainBGMIndex].clip = pack.m_clip;
            m_bgmSources[c_mainBGMIndex].volume = pack.m_volume * BGMVolume * MasterVolume;
            m_bgmSources[c_mainBGMIndex].time = start;


            m_bgmSources[c_mainBGMIndex].Play();
        }
        else
        {// SEの再生であれば
            int index = GetSEIndex(labelName);
            if (index < 0)
            {
                Debug.LogError(labelName + "のSEは存在していません");
                return;
            }

            pack = m_sePack[index];

            // SEデータをソースにセットする
            m_seSources[m_seSourceIndex].playOnAwake = false;
            m_seSources[m_seSourceIndex].PlayOneShot(pack.m_clip, pack.m_volume * SEVolume * MasterVolume);

            m_seSourceIndex = (m_seSourceIndex + 1) % c_sePlayNum;
        }
    }

    /// <summary>
    /// SE用で同じタイミングの音を重複再生しないようにするため
    /// </summary>
    /// <param name="label"></param>
    /// <param name="time"></param>
    public void PlayAudioMemoryTime(string label, float time)
    {
        // データがない場合は追加する
        if(!m_times.ContainsKey(label))
            m_times.Add(label, time);
        else
        {// データがある場合
            if (m_times[label] == time)
                return;
        }

        //Debug.Log(label + "再生: " + time);
        m_times[label] = time;
        PlayAudio(label, false);
    }

    public void PlayAudioAndFadeBeteenTime(string labelName, float start, float end)
    {
        if (m_bgmSources[c_mainBGMIndex] == null)
            return;

        m_bgmSources[c_fadeBGMIndex].clip = m_bgmSources[c_mainBGMIndex].clip;
        m_bgmSources[c_fadeBGMIndex].time = m_bgmSources[c_mainBGMIndex].time;
        m_bgmSources[c_fadeBGMIndex].volume = m_bgmSources[c_mainBGMIndex].volume;
        m_bgmSources[c_fadeBGMIndex].Play();
        PlayAudio(labelName, true, start, end);
    }

    

    /// <summary>
    /// オーディオを再生する
    /// </summary>
    /// <param name="index"></param>
    public void StopBGM()
    {
        m_bgmSources[0].Stop();
    }

    public void StopAllSE()
    {
        for (int i = 0; i < c_sePlayNum; ++i)
            m_seSources[i].Stop();
    }

    public void FadeVolume(float tmp)
    {
        float vol = Mathf.Lerp(0.0f, MasterVolume, 1.0f - tmp);
        m_bgmSources[c_mainBGMIndex].volume = m_bgmPack[m_bgmCurrentIndex].m_volume * BGMVolume * vol;
    }


    /// <summary>
    /// インデックスを取得する(BGM)
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
    /// インデックスを取得する(SE)
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
