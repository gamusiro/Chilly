using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_EnemyAttackSideBar : MonoBehaviour
{
    #region 内部用変数

    // 指定位置ポジション
    Vector3 m_targetPosition;

    // タイミング
    public float m_perfTime;

    // 移動速度
    float m_vel;

    #endregion


    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        // 生成されたときの現在ポジションを取得
        
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    private void FixedUpdate()
    {
        Vector3 setPosition = m_targetPosition;
        setPosition.x = setPosition.x + (m_vel * (m_perfTime - CS_AudioManager.Instance.TimeBGM));
        transform.localPosition = setPosition;
    }

    public void SetLane(int lane, float offset = 0.0f, bool left = false)
    {
        const int laneNum = 5;

        m_targetPosition = Vector3.zero;
        m_targetPosition.x = -60.0f + 30.0f * (laneNum - lane);
        m_targetPosition.x += 30.0f * offset;

        m_vel = 200.0f;     // 右から流れてくる
        if (left)
            m_vel *= -1.0f; // 左から流れてくる
    }
}
