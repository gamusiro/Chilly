using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_EnemyAttackFromEnemy : CS_LoadNotesFile
{
    #region インスペクタ用変数

    // 生成するオブジェクト
    [SerializeField, CustomLabel("生成オブジェクト")]
    GameObject m_createObject;

    // 生成するオブジェクト
    [SerializeField, CustomLabel("ジャンプラインオブジェクト")]
    GameObject m_jumpLineObject;

    // オブジェクトの生成数
    [SerializeField, CustomLabel("オブジェクトの生成数")]
    [Range(20.0f, 60.0f)]
    int m_numMax = 20;

    // ジャンプタイミングに合わせてオフセットを持たせる
    [SerializeField, CustomLabel("オフセット")]
    [Range(0.0f, 2.0f)]
    float m_offset = 0.0f;

    #endregion

    #region 内部用変数

    // ゲームオブジェクト管理用
    GameObject[] m_objects;

    // 生成したデータのカウント
    int m_createCount;

    // 参照するノーツデータのインデックスを管理
    int m_refInfoIndex;

    #endregion


    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        m_objects = new GameObject[m_numMax];
        m_refInfoIndex = 0;

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

        if (m_perNoteInfos[m_refInfoIndex].time <= CS_AudioManager.Instance.TimeBGM)
        {
            m_refInfoIndex++;
            m_refInfoIndex = Mathf.Min(m_refInfoIndex, m_perNoteInfos.Count - 1);
        }

        // 全てをループする
        for (int i = 0; i < m_numMax; ++i)
        {
            if (m_objects[i] == null)
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

        PerNoteInfo info = m_perNoteInfos[m_createCount];

        // 生成ポジションの指定
        Vector3 createPos = Vector3.zero;
        createPos.x = 0.0f;
        createPos.y = 2.5f;
        createPos.z = (info.time - m_offset) * CS_MoveController.GetMoveVel();

        GameObject obj = Instantiate(m_createObject, createPos, Quaternion.identity);
        obj.AddComponent<CS_EnemyAttackNotes>();
        obj.GetComponent<CS_EnemyAttackNotes>().m_perfTime = info.time;

        // ジャンプタイミング用の線
        createPos.x = 0.0f;
        createPos.y = 2.41f;
        createPos.z = info.time * CS_MoveController.GetMoveVel() * -1.0f;
        GameObject lin = Instantiate(m_jumpLineObject, createPos, Quaternion.identity);

        Destroy(obj, info.time - CS_AudioManager.Instance.TimeBGM + 0.5f);
        Destroy(lin, info.time - CS_AudioManager.Instance.TimeBGM + 0.5f);
        m_createCount++;

        m_objects[index] = obj;
    }

    /// <summary>
    /// データセット
    /// </summary>
    /// <returns></returns>
    public float GetPerfectTime()
    {
        return m_perNoteInfos[m_refInfoIndex].time;
    }
}
