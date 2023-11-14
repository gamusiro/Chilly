using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Player : MonoBehaviour
{
    #region インスペクタ用変数
    //現在使用中のカメラの情報
    [SerializeField] private MainGameCameraManager m_mainGameCameraManager;

    // プレイヤーの横移動速度
    [SerializeField, CustomLabel("横移動のスピード")]
    float m_side;

    // ジャンプ力
    [SerializeField, CustomLabel("ジャンプ力")]
    float m_jump;

    // 無敵時間
    [SerializeField, CustomLabel("無敵時間")]
    float m_invalidTime;

    // 疑似重力
    [SerializeField, Header("重力影響度")]
    [Range(1.0f, 1000.0f)]
    float m_gravity;

    #endregion

    #region 内部用変数

    // InputSystem
    IA_Player m_inputAction;
    
    // リジッドボディ
    Rigidbody m_rigidBody;

    // マテリアル
    Material m_material;

    // 使用中のカメラオブジェクト
    GameObject m_mainVirtualCamera;

    const float c_sideMax = 69.0f;

    // ジャンプ中かどうかのフラグ
    bool m_isFlying;

    // 衝突したか
    bool m_damaged;

    // 色
    float m_degree;

    #endregion

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        m_inputAction = new IA_Player();
        m_inputAction.Enable();

        m_rigidBody = GetComponent<Rigidbody>();
        m_material = gameObject.transform.GetChild(0).GetComponent<Renderer>().material;

        m_mainVirtualCamera = m_mainGameCameraManager.GetCurCamera();
        m_isFlying = false;
        m_damaged = false;
        m_degree = 0.0f;
    }

    /// <summary>
    /// 更新処理(毎フレーム)
    /// </summary>
    void Update()
    {
        Vector2 direction = m_inputAction.Player.Move.ReadValue<Vector2>();

        // 移動処理
        Vector3 currentPos = transform.position;
        Vector3 move = new Vector3(direction.x * m_side, 0.0f, 0.0f);
        move.x *= m_mainVirtualCamera.transform.right.x;

        currentPos += move * Time.deltaTime;
        currentPos.x = Mathf.Clamp(currentPos.x, -c_sideMax, c_sideMax);

        transform.position = currentPos;

        // 向きを設定
        Vector3 rotate = Vector3.zero;
        rotate.y = -30.0f * direction.x * m_mainVirtualCamera.gameObject.transform.right.x;

        transform.localEulerAngles = rotate;

        // ジャンプ
        if (m_inputAction.Player.Jump.triggered && !m_isFlying)
        {
            CS_AudioManager.Instance.PlayAudio("Jump");

            m_isFlying = true;
            m_rigidBody.AddForce(new Vector3(0, m_jump, 0), ForceMode.Impulse);
        }

        // スライド操作
        if(m_inputAction.Player.SlideL.triggered)
        {
            m_rigidBody.AddForce(move * 2.0f, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// FPSごと
    /// </summary>
    private void FixedUpdate()
    {
        Vector3 force = new Vector3(0.0f, -m_gravity, 0.0f);
        m_rigidBody.AddForce(force);

        m_degree++;

        // ぶつかっていたら
        if(m_damaged)
        {
            float c = Mathf.Cos(m_degree);

            Color color = m_material.color;
            color.r = c; color.g = c; color.b = c;
            m_material.color = color;
        }
    }

    /// <summary>
    /// 当たり判定
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Field")
            m_isFlying = false;

        // ダメージを受け付ける状態か
        if (!m_damaged)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                m_damaged = true;
                m_degree = 0.0f;
                Invoke(nameof(UnlockInvincibility), m_invalidTime);
            }
        }
    }

    /// <summary>
    /// カメラの設定
    /// </summary>
    public void SetUsingCamera()
    {
        m_mainVirtualCamera =  m_mainGameCameraManager.GetCurCamera();
    }

    /// <summary>
    /// 無敵時間の無効化
    /// </summary>
    void UnlockInvincibility()
    {
        m_damaged = false;
        m_material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }
}
