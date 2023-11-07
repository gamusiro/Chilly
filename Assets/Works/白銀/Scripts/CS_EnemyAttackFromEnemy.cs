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
    [Range(5.0f, 20.0f)]
    int m_numMax = 5;

    // ジャンプタイミングに合わせてオフセットを持たせる
    [SerializeField, CustomLabel("オフセット")]
    [Range(0.0f, 2.0f)]
    float m_offset = 0.0f;

    #endregion

    #region 内部用変数

    // ゲームオブジェクト管理用
    List<GameObject> m_gameObjects;

    // カメラのオブジェクト
    GameObject m_cameraObject;

    // 破棄したデータのカウント
    int m_numDestroy;

    #endregion


    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Start()
    {
        m_gameObjects = new List<GameObject>();
        m_numDestroy = 0;

        // 読み込み処理
        this.Load();

        m_cameraObject = CS_MoveController.Instance.GetVirtualCamera("Front");

        // テスト用オブジェクト生成
        for (int i = 0; i < m_numMax; ++i)
            CreateGameObject(i);
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void FixedUpdate()
    {
        // 作業用変数
        int numDestroy = m_numDestroy;

        for (int i = numDestroy; i < numDestroy + m_numMax; ++i)
        {
            // ノーツカウントよりデータが多ければ
            if (i >= m_perNoteInfos.Count)
                break;                          // 探索終了

            if (m_gameObjects[i] == null)
                continue;

            // ゲームオブジェクトのポジションがムーブオブジェクトの座標より大きくなったら
            if (m_gameObjects[i].transform.position.z < m_cameraObject.gameObject.transform.position.z)
            {
                // 対象のオブジェクトを破棄する
                Destroy(m_gameObjects[i]);

                // 破棄数を増やす
                m_numDestroy++;

                // 次のオブジェクトの生成
                if (m_gameObjects.Count < m_perNoteInfos.Count)
                    CreateGameObject(m_gameObjects.Count);
            }

        }
    }

    /// <summary>
    /// ノーツオブジェクトの生成
    /// </summary>
    /// <param name="index"></param>
    void CreateGameObject(int index)
    {
        Vector3 createPos = Vector3.zero;
        createPos.x = -60.0f + m_perNoteInfos[index].lane * 30.0f;
        createPos.y = 2.5f;
        createPos.z = (m_perNoteInfos[index].time - m_offset) * CS_MoveController.Instance.GetMoveVel();

        GameObject obj = Instantiate(m_createObject, createPos, Quaternion.identity);
        obj.AddComponent<CS_EnemyAttackNotes>();
        obj.GetComponent<CS_EnemyAttackNotes>().m_perfTime = m_perNoteInfos[index].time;

        m_gameObjects.Add(obj);
    }
}
