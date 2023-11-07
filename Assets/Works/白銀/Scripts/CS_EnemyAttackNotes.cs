using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_EnemyAttackNotes : MonoBehaviour
{
    #region 内部用変数

    // 指定位置ポジション
    Vector3 m_targetPosition;

    // オーディオソース
    AudioSource m_audioSource;

    // タイミング
    public float m_perfTime;
    
    #endregion


    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        // 生成されたときの現在ポジションを取得
        m_targetPosition = transform.position;
        m_targetPosition.z *= -1.0f;

        // オーディオソースの取得
        m_audioSource = CS_AudioManager.Instance.GetAudioSource("GameAudio");
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    private void FixedUpdate()
    {
        Vector3 setPosition = m_targetPosition;
        setPosition.z += 400.0f * 1.0f * (m_perfTime - m_audioSource.time);

        transform.position = setPosition;
    }
}
