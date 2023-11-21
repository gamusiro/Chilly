using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Shadow : MonoBehaviour
{
    #region インスペクタ用変数

    // 生成時のスケール
    [SerializeField, CustomLabel("初期スケール")]
    Vector3 m_originScale;

    // 最終的なスケール
    [SerializeField, CustomLabel("最終スケール")]
    Vector3 m_targetScale;

    // パーフェクトタイムの何秒前から
    [SerializeField, CustomLabel("何秒前から")]
    float m_befTime;

    #endregion

    #region 内部用変数

    // パーフェクトタイム
    float m_perfTime;

    #endregion

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        transform.localScale = m_originScale;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void FixedUpdate()
    {
        float tmp = Mathf.Clamp(CS_AudioManager.Instance.TimeBGM - (m_perfTime - m_befTime), 0.0f, m_befTime);
        tmp /= m_befTime;

        transform.localScale = Vector3.Lerp(m_originScale, m_targetScale, tmp);
    }

    /// <summary>
    /// パーフェクトタイムの設定
    /// </summary>
    /// <param name="time"></param>
    public void SetPerfectTime(float time)
    {
        m_perfTime = time;
    }
}
