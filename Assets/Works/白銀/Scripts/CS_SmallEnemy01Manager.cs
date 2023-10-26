using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_SmallEnemy01Manager : CS_LoadNotesFile
{
    [SerializeField, CustomLabel("生成する敵オブジェクト")]
    GameObject m_negativePieceObject;

    [SerializeField, CustomLabel("基準にするオーディオデータ")]
    AudioSource m_audioSource;

    [SerializeField, CustomLabel("オブジェクトの生成数")]
    [Range(5, 20)]
    int m_createCount;

    [SerializeField, CustomLabel("オフセット")]
    public float m_offset;

    List<GameObject> m_gameObjects = new List<GameObject>();

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
        for(int i = m_destroyCount; i < m_createCount; ++i)
            CreateGameObject(i);
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        for (int i = m_destroyCount; i < m_destroyCount + m_createCount; ++i)
        {
            if (i >= m_perNoteInfos.Count)
                break;

            // この条件が真であれば、ベクトルに力を加える
            PerNoteInfo noteInfo = m_perNoteInfos[i];
            if (noteInfo.time <= m_audioSource.time)
            {
                if (noteInfo.lane == 0) // 左(ー座標)
                    m_gameObjects[i].GetComponent<CS_NegativePiece>().SetVelocity(new Vector3( 50.0f, 0.0f, -m_frontMoveVel), 5.0f);
                else                                                                           
                    m_gameObjects[i].GetComponent<CS_NegativePiece>().SetVelocity(new Vector3(-50.0f, 0.0f, -m_frontMoveVel), 5.0f);

                // 破棄数を増やす
                m_destroyCount++;

                // 新たなオブジェクトの生成を行う
                if (m_perNoteInfos.Count > m_gameObjects.Count)
                    CreateGameObject(m_gameObjects.Count);
            }
        }
    }

    /// <summary>
    /// オブジェクトの生成処理
    /// </summary>
    /// <param name="index">生成するオブジェクトのインデックス</param>
    void CreateGameObject(int index)
    {
        Vector3 createPos = Vector3.zero;

        // ノーツデータのレーン番号の種類が2つだけ
        PerNoteInfo noteInfo = m_perNoteInfos[index];
        if (noteInfo.lane == 0) // 左(ー座標)
            createPos.x = -30.0f;
        else
            createPos.x =  30.0f;

        createPos.y = m_negativePieceObject.transform.position.y;
        createPos.z = (m_frontMoveVel * noteInfo.time + m_offset) * -1.0f;

        // オブジェクトの生成処理
        GameObject obj = Instantiate(m_negativePieceObject, createPos, Quaternion.identity);
        m_gameObjects.Add(obj);
    }
}
