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
    GameObject[] m_fallObjects;
    GameObject[] m_shadowObjects;

    int m_createCount;

    #endregion


    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        m_fallObjects = new GameObject[m_numMax];
        m_shadowObjects = new GameObject[m_numMax];

        m_createCount = 0;

        // 読み込み処理
        this.Load();
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void FixedUpdate()
    {
        if (!CS_MoveController.IsMoving())
            return;

        // 全てをループする
        for(int i = 0; i < m_numMax; ++i)
        {
            if (m_fallObjects[i] == null)
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

        // こころのかけらだったら無視する
        PerNoteInfo info = m_perNoteInfos[m_createCount];

        // 生成する座標を出す
        Vector3 createPos = Vector3.zero;
        float time = info.time - CS_AudioManager.Instance.TimeBGM;

        createPos.x = -60.0f + info.lane * 30.0f;
        createPos.y = (9.81f / 2.0f) * Mathf.Pow(time, 2.0f);    // 床が高くなったので offset 2.4f
        createPos.z = info.time * CS_MoveController.GetMoveVel() * -1.0f;

        // 落ちるオブジェクトの変数を生成
        GameObject fall = Instantiate(m_createObject, createPos, Quaternion.identity);
        
        // 影オブジェクトの影を生成
        createPos.y = 0.01f;
        GameObject sdw = Instantiate(m_shadowObject, createPos, Quaternion.identity);
        sdw.GetComponent<CS_Shadow>().SetPerfectTime(info.time);

        m_fallObjects[index] = fall;
        m_shadowObjects[index] = sdw;

        Destroy(fall, time + m_graceTime);
        Destroy(sdw, time + m_graceTime);

        m_createCount++;
    }
}
