using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[Serializable]
class SignBoardData
{
    public Material m_material;
    public float    m_changeTime;
}


public class CS_Signboard : MonoBehaviour
{
    // ステート状態
    enum STATE
    {
        NONE,
        EXPAND,
        SHRINK
    }

    #region インスペクタ用変数

    // 初期スケール
    [SerializeField, CustomLabel("初期スケール")]
    Vector3 m_startScale;

    // 破棄するまでの時間
    [SerializeField, CustomLabel("消える時間")]
    [Range(1.0f, 10.0f)]
    float m_workOffTime;

    // 看板用マテリアルデータ
    [SerializeField, CustomLabel("チュートリアル看板データ")]
    List<SignBoardData> m_boardDatasList;

    #endregion

    #region 内部用変数

    // 遅延タイム
    const float c_delayTime = 1.0f;

    // 使用したデータの数
    int m_usedCount;

    // ワーク変数
    float m_tmp;
    float m_rad;

    // 初期位置を取得
    Vector3 m_originLocalPosition;

    // 初期スケールを取得
    Vector3 m_baseScale;

    // ステート状態
    STATE m_state;

    // レンダラーの取得
    Renderer m_renderer;

    #endregion




    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        m_usedCount = 0;

        // 看板データがなければ即座に破棄
        if(m_boardDatasList.Count == 0)
        {
            Destroy(gameObject);
            return;
        }

        // レンダラーの取得
        m_renderer = transform.Find("Screen").gameObject.GetComponent<Renderer>();
        m_renderer.material = m_boardDatasList[0].m_material;

        // ローカル座標を取得
        m_originLocalPosition = transform.localPosition;

        // スケールの取得・変更
        m_baseScale = transform.localScale;
        transform.localScale = m_startScale;

        // 補間変数
        m_rad = 0.0f;
        m_tmp = 0.0f;

        // 状態を呼び出すまで
        Invoke(nameof(SetStateExpand), 8.0f);
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // 縮小中なら戻る
        if (m_state == STATE.SHRINK)
            return;

        // 現在のオーディオタイムを取得
        float currentlyAudioTime = CS_AudioManager.Instance.TimeBGM;

        // マテリアルを切り替えるタイミングであれば
        if(currentlyAudioTime >= m_boardDatasList[m_usedCount].m_changeTime)
        {
            // シュリンクを呼び出す
            if ((m_usedCount + 1) >= m_boardDatasList.Count)
            {
                SetStateShrink();
                return;
            }

            // マテリアルを変更
            m_renderer.material = m_boardDatasList[m_usedCount].m_material;
            m_usedCount++;
        }
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    private void FixedUpdate()
    {
        Idle();

        switch(m_state)
        {
            case STATE.EXPAND:
                {
                    // 時間の更新
                    m_tmp += Time.deltaTime;

                    // スケールの計算
                    Vector3 localScale = transform.localScale;
                    localScale.x = Mathf.Lerp(m_startScale.x, m_baseScale.x, Mathf.Clamp01(m_tmp - c_delayTime));
                    localScale.y = Mathf.Lerp(m_startScale.y, m_baseScale.y, Mathf.Clamp01(m_tmp));
                    localScale.z = Mathf.Lerp(m_startScale.z, m_baseScale.z, Mathf.Clamp01(m_tmp - c_delayTime));

                    // スケールの設定
                    transform.localScale = localScale;

                    // 線形補間の値 + 1.0f以上になったら状態を戻す
                    if (m_tmp >= c_delayTime + 1.0f)
                        m_state = STATE.NONE;
                }
                break;
            case STATE.SHRINK:
                {
                    // 時間の更新
                    m_tmp += Time.deltaTime;

                    // スケールの計算
                    Vector3 perScale = m_baseScale / m_workOffTime;

                    // スケールの設定
                    transform.localScale = perScale * (m_workOffTime - m_tmp);

                    // データを破棄する
                    if (m_tmp > m_workOffTime)
                        Destroy(gameObject);
                }
                break;
        }
    }

    /// <summary>
    /// 看板の上下動作
    /// </summary>
    void Idle()
    {
        m_rad += Time.deltaTime;
        Vector3 addPosition = Vector3.up;
        addPosition.y *= Mathf.Cos(m_rad) * 20.0f;

        transform.localPosition = m_originLocalPosition + addPosition;
    }

    /// <summary>
    /// 状態を切り替える
    /// </summary>
    void SetStateExpand()
    {
        m_tmp = 0.0f;
        m_state = STATE.EXPAND;
    }

    /// <summary>
    /// 状態を切り替える
    /// </summary>
    void SetStateShrink()
    {
        m_tmp = 0.0f;
        m_state = STATE.SHRINK;
    }
}
