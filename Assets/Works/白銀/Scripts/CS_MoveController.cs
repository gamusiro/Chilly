using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_MoveController : CS_SingletonMonoBehaviour<CS_MoveController>
{
    #region インスペクタ用変数

    // プレイヤーの進む速度
    [SerializeField, CustomLabel("前進速度")]
    float m_moveVel;

    #endregion


    #region 内部用変数

    // 速度ベクトル
    Vector3 m_vecVel;

    // 子オブジェクト管理
    Dictionary<string, GameObject> m_children = new Dictionary<string, GameObject>();
    
    // 仮想カメラオブジェクト
    Dictionary<string, GameObject> m_cameraList = new Dictionary<string, GameObject>();

    #endregion

    /// <summary>
    /// 初期化処理
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        m_vecVel = Vector3.zero;

        // 子オブジェクト取得
        int count = gameObject.transform.childCount;

        for(int i = 0; i < count; ++i)
        {
            GameObject obj = gameObject.transform.GetChild(i).gameObject;
            m_children.Add(obj.name, obj);
        }

        // カメラから仮想カメラのゲームオブジェクトを取得する
        foreach (CinemachineVirtualCamera cam in m_children["Camera"].GetComponentsInChildren<CinemachineVirtualCamera>())
        {
            // 自分自身の場合は処理をスキップする
            if (cam.gameObject == gameObject) 
                continue;

            m_cameraList.Add(cam.gameObject.name, cam.gameObject);
        }
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    private void FixedUpdate()
    {
        transform.position = m_vecVel * CS_AudioManager.Instance.GetAudioSource("GameAudio").time;
    }

    /// <summary>
    /// 移動速度の取得
    /// </summary>
    /// <returns></returns>
    public float GetMoveVel()
    {
        return m_moveVel;
    }

    /// <summary>
    /// 子オブジェクトの取得
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject GetObject(string name)
    {
        if(!m_children.ContainsKey(name))
        {
            Debug.Log(name + "のオブジェクトは存在しません");
            return null;
        }

        return m_children[name];
    }

    /// <summary>
    /// 子オブジェクトの取得
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject GetVirtualCamera(string name)
    {
        if (!m_cameraList.ContainsKey(name))
        {
            Debug.Log(name + "のオブジェクトは存在しません");
            return null;
        }

        return m_cameraList[name];
    }

    /// <summary>
    /// 移動スタート
    /// </summary>
    public void MoveStart()
    {
        m_vecVel = new Vector3(0.0f, 0.0f, -m_moveVel);
    }
}
