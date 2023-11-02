using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_WakeupObject : CS_LoadNotesFile
{
    [SerializeField, CustomLabel("生成する敵オブジェクト")]
    GameObject m_negativePieceObject;

    [SerializeField, CustomLabel("生成する影オブジェクト")]
    GameObject m_shadowObject;

    [SerializeField, CustomLabel("オブジェクトの生成数")]
    [Range(5, 20)]
    int m_createCount;

    [SerializeField, CustomLabel("オフセット")]
    public float m_offset;

    AudioSource m_audioSource;

    List<GameObject> m_gameObjects = new List<GameObject>();
    List<GameObject> m_shadowObjects = new List<GameObject>();

    float m_frontMoveVel;   // 前進速度
    int m_destroyCount;     // 破棄したデータの数


    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        m_frontMoveVel = gameObject.transform.parent.gameObject.GetComponent<CS_MoveController>().GetMoveVel();
        m_destroyCount = 0;

        // 音源データの取得
        m_audioSource = CS_AudioManager.Instance.GetAudioSource("MainBGM");

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
        for (int i = m_destroyCount; i < m_destroyCount + m_createCount; ++i)
        {
            if (i >= m_perNoteInfos.Count)
                break;

            // この条件が真であれば、ベクトルに力を加える
            PerNoteInfo noteInfo = m_perNoteInfos[i];
            if ((noteInfo.time - 1.0f)  <= m_audioSource.time)
            {
                m_gameObjects[i].GetComponent<CS_NegativePiece>().SetVelocity(new Vector3(0.0f, -(100.0f), 0.0f),2.0f);
                m_shadowObjects[i].GetComponent<CS_Shadow>().SetWork();

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

        PerNoteInfo noteInfo = m_perNoteInfos[index];

        const float stride = 30.0f;

        createPos.x = (2 - noteInfo.lane) * stride;
        createPos.y = 100.0f;
        createPos.z = (m_frontMoveVel * noteInfo.time + m_offset) * -1.0f;  // ノーツのタイミング

        // オブジェクトの生成処理
        GameObject obj = Instantiate(m_negativePieceObject, createPos, Quaternion.identity);
        m_gameObjects.Add(obj);

        // 影オブジェクト生成
        createPos.y = 0.01f;
        GameObject sdw = Instantiate(m_shadowObject, createPos, Quaternion.identity);
        m_shadowObjects.Add(sdw);
    }
}
