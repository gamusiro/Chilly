using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_MoveController : MonoBehaviour
{
    #region インスペクタ用変数

    // プレイヤーの進む速度
    [SerializeField, CustomLabel("前進速度")]
    float m_moveVel;

    #endregion


    #region 内部用変数

    static float m_getMoveVel;

    // 速度ベクトル
    static Vector3 m_vecVel;

    // 子オブジェクト管理
    static Dictionary<string, GameObject> m_children = new Dictionary<string, GameObject>();

    #endregion

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        m_getMoveVel = m_moveVel;

        // 子オブジェクト取得
        int count = gameObject.transform.childCount;

        for(int i = 0; i < count; ++i)
        {
            GameObject obj = gameObject.transform.GetChild(i).gameObject;
            m_children.Add(obj.name, obj);
        }
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    private void FixedUpdate()
    {
        transform.position = m_vecVel * CS_AudioManager.Instance.TimeBGM;
    }

    /// <summary>
    /// 移動速度の取得
    /// </summary>
    /// <returns></returns>
    static public float GetMoveVel()
    {
        return m_getMoveVel;
    }

    /// <summary>
    /// 子オブジェクトの取得
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    static public GameObject GetObject(string name)
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

    /// <summary>
    /// 移動スタート
    /// </summary>
    static public void MoveStart()
    {
        m_vecVel = new Vector3(0.0f, 0.0f, -m_getMoveVel);
    }
}
