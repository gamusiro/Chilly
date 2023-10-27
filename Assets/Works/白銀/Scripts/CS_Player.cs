using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Player : MonoBehaviour
{
    [SerializeField, CustomLabel("横移動のスピード")]
    float m_side;

    [SerializeField, CustomLabel("ジャンプ力")]
    float m_jump;

    [SerializeField, Header("重力影響度")]
    [Range(1.0f, 1000.0f)]
    float m_gravity;

    IA_Player m_inputAction;
    Rigidbody m_rigidBody;
    bool m_isFlying;

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        m_inputAction = new IA_Player();
        m_inputAction.Enable();

        m_rigidBody = GetComponent<Rigidbody>();
        m_isFlying = false;
    }

    /// <summary>
    /// 更新処理(毎フレーム)
    /// </summary>
    void Update()
    {
        Vector2 direction = m_inputAction.Player.Move.ReadValue<Vector2>();

        // 移動処理
        Vector3 move = new Vector3(direction.x * m_side, 0.0f, 0.0f);
        transform.Translate(move * Time.deltaTime);

        // ジャンプ
        if (m_inputAction.Player.Jump.triggered && !m_isFlying)
        {
            m_isFlying = true;
            m_rigidBody.AddForce(new Vector3(0, m_jump, 0), ForceMode.Impulse);
        }
    }

    /// <summary>
    /// FPSごと
    /// </summary>
    private void FixedUpdate()
    {
        Vector3 force = new Vector3(0.0f, -m_gravity, 0.0f);
        m_rigidBody.AddForce(force);
    }

    /// <summary>
    /// 当たり判定
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        m_isFlying = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
        }
    }
}
