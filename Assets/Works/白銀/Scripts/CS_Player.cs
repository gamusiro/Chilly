using Cinemachine;
using DG.Tweening;
using DG.Tweening.Core;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CS_Player : MonoBehaviour
{
    #region インスペクタ用変数

    [Header("プレイヤーパラメータ")]

    // プレイヤーの影座標
    [SerializeField, CustomLabel("プレイヤーの影座標")]
    Transform m_shadowTransform;

    // プレイヤーの横移動速度
    [SerializeField, CustomLabel("横移動のスピード")]
    float m_side;

    // ジャンプ力
    [SerializeField, CustomLabel("ジャンプ力")]
    float m_jump;

    // 無敵時間
    [SerializeField, CustomLabel("無敵時間")]
    float m_invalidTime;

    // ダッシュ力
    [SerializeField, CustomLabel("ダッシュの力")]
    [Range(2.0f, 10.0f)]
    float m_dashStrength;

    // 疑似重力
    [SerializeField, Header("重力影響度")]
    [Range(1.0f, 1000.0f)]
    float m_gravity;

    // プレイヤーアニメーション
    [SerializeField, CustomLabel("ダメージ処理")]
    HP m_hp;

    // プレイヤーアニメーション
    [SerializeField, CustomLabel("アニメータ")]
    Animator m_animator;

    [SerializeField, Header("ブレインカメラ")]
    CinemachineBrain m_brain;

    //現在使用中のカメラの情報
    [SerializeField, CustomLabel("カメラマネージャー")] 
    CameraPhaseManager m_mainGameCameraManager;

    [Header("パーフェクトタイミング")]

    // 許容パーフェクトタイミング
    [SerializeField, CustomLabel("パーフェクトタイミング(秒)")]
    [Range(0.1f, 1.0f)]
    float m_perfectTimeRange;

    [Header("スピンエフェクト")]
    [SerializeField] private SpinEffect _spinEffectPrefab;

   // [SerializeField, CustomLabel("後ろから")]
   // CS_EnemyAttackFromEnemy m_enemyAttackFromEnemy;

    [Header("エフェクトオブジェクト")]
    [SerializeField, CustomLabel("パーフェクトタイミングエフェクト")]
    GameObject m_perfectEffectObject;

    [Header("プレイヤーモデル")]
    [SerializeField, CustomLabel("プレイヤーモデル")]
    private GameObject _playerModel;

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

    // 横幅定数
    const float c_sideMax = 69.0f;

    // ジャンプ中かどうかのフラグ
    public bool m_isFlying;

    // 衝突したか
    bool m_damaged;

    // 色
    float m_degree;

    // 最後にジャンプした時刻
    float m_timeLastJumped;

    //回転フラグ
    private bool _isRotate;

    #endregion

    #region 公開用変数

    // ジャンプ中かどうか
    public float timeLastJumped 
    { 
        get { return m_timeLastJumped; }
    }


    #endregion

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        m_inputAction = new IA_Player();
        m_inputAction.Enable();

        m_rigidBody = GetComponent<Rigidbody>();
        m_material = gameObject.transform.GetChild(0).Find("mesh_Character").GetComponent<Renderer>().material;

        m_mainVirtualCamera = m_mainGameCameraManager.GetCurCamera();
        m_isFlying = false;
        m_damaged = false;
        m_degree = 0.0f;
        _isRotate = false;
    }

    /// <summary>
    /// 更新処理(毎フレーム)
    /// </summary>
    void Update()
    {
        // 影の座標を更新
        Vector3 pos = transform.localPosition;
        pos.y = 0.0f;
        m_shadowTransform.localPosition = pos;

        // カメラの移動中の場合は操作させない
        if (m_brain.ActiveBlend != null)
            return;

        Vector2 direction = m_inputAction.Player.Move.ReadValue<Vector2>();

        // 移動処理
        Vector3 currentPos = transform.position;
        Vector3 move = new Vector3(direction.x * m_side, 0.0f, 0.0f);
        move.x *= m_mainVirtualCamera.transform.right.x;

        currentPos += move * Time.deltaTime;
        currentPos.x = Mathf.Clamp(currentPos.x, -c_sideMax, c_sideMax);

        //currentPos.y = Mathf.Max(0.0f, currentPos.y);
        transform.position = currentPos;

        // 向きを設定
        Vector3 rotate = Vector3.zero;
        rotate.y = -30.0f * direction.x * m_mainVirtualCamera.gameObject.transform.right.x;

        transform.localEulerAngles = rotate;

        //スピン
        if (Input.GetKeyDown(KeyCode.V))
        {
            _playerModel.transform.DOComplete();
            Vector3 modelRotate = Vector3.zero;
            modelRotate.y = 360.0f+180.0f;
            var tweener=_playerModel.transform.DOLocalRotate(modelRotate, 0.5f, RotateMode.FastBeyond360)
                .OnComplete(() => _isRotate = false)
                .SetLink(this.gameObject);
            _isRotate = true;

            //Instantiate(_spinEffectPrefab, this.transform.position, Quaternion.identity, this.transform);
        }

        // ジャンプ
        if (m_inputAction.Player.Jump.triggered && !m_isFlying)
        {
            // float subFromEnemy = m_enemyAttackFromEnemy.GetPerfectTime() - CS_AudioManager.Instance.TimeBGM;

            //// 後ろからのタイミング(perfectTiming)
            //if (subFromEnemy <= m_perfectTimeRange && subFromEnemy > 0.0f)
            //{
            //    GameObject obj = Instantiate(m_perfectEffectObject);
            //    obj.transform.parent = transform;

            //    Vector3 localPos = transform.localPosition;
            //    localPos.y = 0.0f;
            //    obj.transform.localPosition = Vector3.zero;
            //    obj.transform.localScale = new Vector3(3.0f, 2.0f, 3.0f);

            //    // エフェクトの再生
            //    foreach (ParticleSystem ps in obj.GetComponentsInChildren<ParticleSystem>())
            //        ps.Play();

            //    CS_AudioManager.Instance.PlayAudio("PerfectJump");

            //    Destroy(obj, 1.0f);
            //}
            //else
            //{
            //    // 通常ジャンプ
            //    CS_AudioManager.Instance.PlayAudio("Jump");
            //    m_animator.Play("Jumping", 0, 0.0f);        // 最初から流したいのでこんな感じの設定
            //}

            m_isFlying = true;
            m_rigidBody.constraints = RigidbodyConstraints.None;

            // 回転はしない | Zの値固定
            m_rigidBody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;


            m_rigidBody.AddForce(new Vector3(0, m_jump, 0), ForceMode.Impulse);
        }

        Vector3 currentVel = m_rigidBody.velocity;
        if (m_inputAction.Player.SlideL.triggered)
        {
            CS_AudioManager.Instance.PlayAudio("Dash");

            // 横移動の速度を止める
            currentVel.x = 0.0f;
            m_rigidBody.velocity = currentVel;

            m_rigidBody.AddForce(new Vector3(-m_side * m_mainVirtualCamera.transform.right.x * m_dashStrength, 0.0f, 0.0f), ForceMode.VelocityChange);
            m_animator.Play("DashR", 0, 0.0f);        // 最初から流したいのでこんな感じの設定
        }
        else if (m_inputAction.Player.SlideR.triggered)
        {
            CS_AudioManager.Instance.PlayAudio("Dash");

            // 横移動の速度を止める
            currentVel.x = 0.0f;
            m_rigidBody.velocity = currentVel;

            m_rigidBody.AddForce(new Vector3(m_side * m_mainVirtualCamera.transform.right.x * m_dashStrength, 0.0f, 0.0f), ForceMode.VelocityChange);
            m_animator.Play("DashL", 0, 0.0f);        // 最初から流したいのでこんな感じの設定
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
        string tag = collision.gameObject.tag;

        if (tag == "Field")
        {
            m_isFlying = false;
            m_rigidBody.constraints |= RigidbodyConstraints.FreezePositionY;
            m_animator.Play("Running", 0, 0.0f);    // 最初から
        }

        Debug.Log(tag);

        // ダメージを受け付ける状態か
        if (!m_damaged)
        {
            if (tag == "Enemy")
            {
                m_damaged = true;
                CS_AudioManager.Instance.PlayAudio("Damage");
                m_hp.Hit();
                m_degree = 0.0f;
                Invoke(nameof(UnlockInvincibility), m_invalidTime);
            }
        }
    }

    /// <summary>
    /// 吹き飛ばしオブジェクトとの接触
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Scattering")
        {
            if(IsDashing)
            {
                Vector3 dir = Vector3.zero;
                dir.x = m_rigidBody.velocity.x;

                var rb = other.gameObject.GetComponent<Rigidbody>();
                rb.AddForce(dir * 5.0f, ForceMode.Impulse);
            }
        }
    }

    /// <summary>
    /// カメラの設定
    /// </summary>
    public void SetUsingCamera()
    {
        if(m_mainGameCameraManager)
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

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool IsDashing
    {
        get { return Mathf.Abs(m_rigidBody.velocity.x) > 0.0f; }
    }

    public bool GetSpin()
    {
        return _isRotate;
    }
}
