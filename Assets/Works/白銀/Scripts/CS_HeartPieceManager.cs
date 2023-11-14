using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CS_HeartPieceManager : CS_LoadNotesFile
{
    #region インスペクタ用変数

    // 生成するオブジェクト
    [SerializeField, CustomLabel("生成オブジェクト")]
    GameObject m_createObject;

    // オブジェクトの生成数
    [SerializeField, CustomLabel("オブジェクトの生成数")]
    [Range(20.0f, 100.0f)]
    int m_numMax = 20;

    #endregion

    #region 内部用変数

    // ゲームオブジェクト管理用
    GameObject[] m_objects;

    // 生成数
    int m_createCount = 0;

    #endregion


    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        m_objects = new GameObject[m_numMax];
        m_createCount = 0;

        this.Load();
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void FixedUpdate()
    {
        if (!CS_MoveController.IsMoving())
            return;

        for(int i = 0; i < m_numMax; ++i)
            CreateGameObject(i);
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
        if (info.type == 1)
        {
            m_createCount++;
            return;
        }

        // 生成ポジションの指定
        Vector3 createPos = Vector3.zero;
        createPos.x = -60.0f + info.lane * 30.0f;
        createPos.y = 2.5f;
        createPos.z = info.time * CS_MoveController.GetMoveVel();

        GameObject obj = Instantiate(m_createObject, createPos, Quaternion.identity);
        obj.AddComponent<CS_EnemyAttackNotes>();
        obj.GetComponent<CS_EnemyAttackNotes>().m_perfTime = info.time;

        m_objects[index] = obj;
    }
}
