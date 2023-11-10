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
    #region インスペクタ用変数

    // 切り替えタイム
    [SerializeField]
    List<ChangeTimeData> m_list;

    #endregion

    #region 内部用変数

    // オーディオソース
    AudioSource m_audioSource;

    // 使用するインデックス
    int m_index;

    #endregion

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        m_audioSource = CS_AudioManager.Instance.GetAudioSource("GameAudio");

        // ゴール地点のデータを最後に入れる
        ChangeTimeData finish = new ChangeTimeData();
        finish.m_time = m_audioSource.clip.length;
        finish.m_name = "Back";
        m_list.Add(finish);

        m_index = 0;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        if (m_index < m_list.Count)
        {
            // 設定したデータ
            if (m_list[m_index].m_time <= m_audioSource.time)
            {
                CS_MoveController.GetObject("Player").GetComponent<CS_Player>().SetUsingCamera();
                m_index++;
            }
        }
    }
}
