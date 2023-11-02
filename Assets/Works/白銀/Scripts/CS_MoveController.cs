using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_MoveController : CS_SingletonMonoBehaviour<CS_MoveController>
{
    [SerializeField, CustomLabel("前進速度")]
    float m_moveVel;

    Vector3 m_vecVel;

    private GameObject m_player;
    private GameObject m_enemy;
    private GameObject m_camera;

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        m_vecVel = Vector3.zero;

        m_player    = gameObject.transform.GetChild(0).gameObject;
        m_enemy     = gameObject.transform.GetChild(1).gameObject;
        m_camera    = gameObject.transform.GetChild(2).gameObject;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        transform.position -= m_vecVel * Time.deltaTime;
    }

    /// <summary>
    /// 移動速度の取得
    /// </summary>
    /// <returns></returns>
    public float GetMoveVel()
    {
        return m_moveVel;
    }

    public GameObject GetPlayer()
    {
        return m_player;
    }

    public GameObject GetEnemy()
    {
        return m_enemy;
    }

    public GameObject GetCamera()
    {
        return m_camera;
    }

    public void MoveStart()
    {
        m_vecVel = new Vector3(0.0f, 0.0f, m_moveVel);
    }
}
