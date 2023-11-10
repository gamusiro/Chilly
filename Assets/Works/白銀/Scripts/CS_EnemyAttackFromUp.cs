using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_EnemyAttackFromUp : CS_LoadNotesFile
{
    #region インスペクタ用変数

    // 生成するオブジェクト
    [SerializeField, CustomLabel("生成オブジェクト")]
    GameObject m_createObject;

    // 影オブジェクト
    [SerializeField, CustomLabel("影オブジェクト")]
    GameObject m_shadowObject;

    // オブジェクトの生成数
    [SerializeField, CustomLabel("オブジェクトの生成数")]
    [Range(20.0f, 100.0f)]
    int m_numMax = 20;

    // 地面に落ちてから、破棄するまでの秒数指定
    [SerializeField, CustomLabel("衝突後から破棄までの時間")]
    [Range(0.0f, 5.0f)]
    float m_graceTime = 0.0f;

    #endregion

    #region 内部用変数

    // ゲームオブジェクト管理用
    GameObject[] m_gameObjects;

    int m_createCount;

    AudioSource m_audioSource;

    #endregion


    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        m_gameObjects = new GameObject[m_numMax];
        m_audioSource = CS_AudioManager.Instance.GetAudioSource("GameAudio");

        m_createCount = 0;

        // 読み込み処理
        this.Load();
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        if (!m_audioSource.isPlaying)
            return;

        // 全てをループする
        for(int i = 0; i < m_numMax; ++i)
        {
            if (m_gameObjects[i] == null)
                CreateGameObject(i);
        }
    }

    /// <summary>
    /// ノーツオブジェクトの生成
    /// </summary>
    /// <param name="index"></param>
    void CreateGameObject(int index)
    {
        if (m_createCount >= m_perNoteInfos.Count)
            return;

        Vector3 createPos = Vector3.zero;

        float time = m_perNoteInfos[m_createCount].time - m_audioSource.time;

        createPos.x = -60.0f + m_perNoteInfos[m_createCount].lane * 30.0f;
        createPos.y = (9.81f / 2.0f) * Mathf.Pow(time, 2.0f);
        createPos.z = m_perNoteInfos[m_createCount].time * CS_MoveController.GetMoveVel() * -1.0f;

        GameObject obj = Instantiate(m_createObject, createPos, Quaternion.identity);
        m_gameObjects[index] = obj;
        m_createCount++;

        createPos.y = 0.1f;
        GameObject sdw = Instantiate(m_shadowObject, createPos, Quaternion.identity);

        Destroy(obj, time + m_graceTime);
        Destroy(sdw, time + m_graceTime);
    }
}
