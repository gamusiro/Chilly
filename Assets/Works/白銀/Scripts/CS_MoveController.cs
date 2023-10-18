using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_MoveController : MonoBehaviour
{
    [SerializeField, CustomLabel("前進速度")]
    float m_moveVel;

    Vector3 m_vecVel;

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        m_vecVel = new Vector3(0.0f, 0.0f, m_moveVel);
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
}
