using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_EnemyAttackFromEnemy : CS_LoadNotesFile
{
    #region インスペクタ用変数

    // 生成するオブジェクト
    [SerializeField, CustomLabel("生成オブジェクト")]
    GameObject m_createObject;

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

    // カメラのオブジェクト
    [SerializeField] private GameObject m_cameraObject;

    // 生成したデータのカウント
    int m_createCount;

    #endregion


    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        m_objects = new GameObject[m_numMax];
        

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
        createPos.x = -60.0f + info.lane * 30.0f;
        createPos.y = 2.5f;
        createPos.z = (info.time - m_offset) * CS_MoveController.GetMoveVel();

        GameObject obj = Instantiate(m_createObject, createPos, Quaternion.identity);
        obj.AddComponent<CS_EnemyAttackNotes>();
        obj.GetComponent<CS_EnemyAttackNotes>().m_perfTime = info.time;

        Destroy(obj, info.time - CS_AudioManager.Instance.TimeBGM + 0.5f);
        m_createCount++;

        m_objects[index] = obj;
    }
}
