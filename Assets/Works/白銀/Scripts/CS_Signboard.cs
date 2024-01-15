using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


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

    // 最期スケール
    [SerializeField, CustomLabel("最期スケール")]
    Vector3 m_endScale;

    // 破棄するまでの時間
    [SerializeField, CustomLabel("消える時間")]
    [Range(1.0f, 10.0f)]
    float m_workOffTime;

    // 上下に動く量
    [SerializeField, CustomLabel("移動量")]
    float m_amount;

    // 看板が大きくなるタイミング
    [SerializeField, CustomLabel("看板のスケール変更開始時間")]
    float m_setTime = 8.0f;

    #endregion

    #region 内部用変数

    // 遅延タイム
    const float c_delayTime = 1.0f;

    // ワーク変数
    float m_tmp;
    float m_rad;

    // 初期位置を取得
    Vector3 m_originLocalPosition;

    // ステート状態
    STATE m_state;
    bool m_startChange;

    #endregion


    /// <summary>
    /// 生成時
    /// </summary>
    private void Awake()
    {
        gameObject.SetActive(CS_GameManager.GetOnTutorial);
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        // ローカル座標を取得
        m_originLocalPosition = transform.localPosition;

        // スケールの取得・変更
        transform.localScale = m_startScale;

        // 補間変数
        m_rad = 0.0f;
        m_tmp = 0.0f;

        // 状態を呼び出すまで
        Invoke(nameof(SetStateExpand), m_setTime);
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
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
                    localScale.x = Mathf.Lerp(m_startScale.x, m_endScale.x, Mathf.Clamp01(m_tmp - c_delayTime));
                    localScale.y = Mathf.Lerp(m_startScale.y, m_endScale.y, Mathf.Clamp01(m_tmp));
                    localScale.z = Mathf.Lerp(m_startScale.z, m_endScale.z, Mathf.Clamp01(m_tmp - c_delayTime));

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
                    Vector3 perScale = m_endScale / m_workOffTime;

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
        addPosition.y *= Mathf.Cos(m_rad) * m_amount;

        transform.localPosition = m_originLocalPosition + addPosition;
    }

    /// <summary>
    /// 状態を切り替える
    /// </summary>
    void SetStateExpand()
    {
        m_tmp = 0.0f;
        m_state = STATE.EXPAND;
        m_startChange = true;

        // 縦に広がるのがわかるように少しだけ高さを与える
        m_startScale.x = 0.01f;
    }

    /// <summary>
    /// 状態を切り替える
    /// </summary>
    public void SetStateShrink()
    {
        m_tmp = 0.0f;
        m_state = STATE.SHRINK;
    }

    public bool StartTutorial()
    {
        return (m_state == STATE.NONE) && m_startChange;
    }
}
