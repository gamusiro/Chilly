using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_StaticObject : CS_LoadNotesFile
{
    [SerializeField, CustomLabel("生成する敵オブジェクト")]
    GameObject m_negativePieceObject;

    [SerializeField, CustomLabel("基準にするオーディオデータ")]
    AudioSource m_audioSource;

    [SerializeField, CustomLabel("オブジェクトの生成数")]
    [Range(5, 20)]
    int m_createCount;

    float m_frontMoveVel;   // 前進速度
    int m_destroyCount;     // 破棄したデータの数

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        m_frontMoveVel = gameObject.transform.parent.gameObject.GetComponent<CS_MoveController>().GetMoveVel();
        m_destroyCount = 0;

        // ノーツデータの読み込み
        this.Load();

        // オブジェクトの生成処理
        for (int i = m_destroyCount; i < m_createCount; ++i)
            CreateGameObject(i);
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        for (int i = m_destroyCount; i < m_perNoteInfos.Count; ++i)
        {
            if (i >= m_perNoteInfos.Count)
                break;

            // 一秒前に生成し、プレイヤーのいる位置でタイミングが重なるようにする
            PerNoteInfo noteInfo = m_perNoteInfos[i];
            if ((noteInfo.time - 1.0f) <= m_audioSource.time)
            {
                CreateGameObject(i);

                // 破棄データを増やす
                m_destroyCount++;
            }
            else
                break;
        }
    }

    /// <summary>
    /// オブジェクトの生成処理
    /// </summary>
    /// <param name="index">生成するオブジェクトのインデックス</param>
    void CreateGameObject(int index)
    {
        Vector3 createPos = Vector3.zero;
        PerNoteInfo noteInfo = m_perNoteInfos[index];

        const float stride = 30.0f;

        createPos.x = (2 - noteInfo.lane) * stride;
        createPos.y = m_negativePieceObject.transform.position.y;
        createPos.z = (m_frontMoveVel * noteInfo.time) * -1.0f;

        // オブジェクトの生成処理
        GameObject obj = Instantiate(m_negativePieceObject, createPos, Quaternion.identity);
        obj.GetComponent<CS_NegativePiece>().SetVelocity(Vector3.zero, 10.0f);
    }
}
