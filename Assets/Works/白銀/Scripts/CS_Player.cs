using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CS_Player : MonoBehaviour
{
    [SerializeField, CustomLabel("横移動の速度")]
    float m_moveVel = 5.0f;

    [SerializeField, CustomLabel("ジャンプの強さ")]
    float m_jumpVel = 5.0f;

    IA_Player m_inputActions;   // 入力
    Rigidbody m_rigidbody;      // 剛体
    bool m_isFlying;

    // Start is called before the first frame update
    void Start()
    {
        m_inputActions = new IA_Player();
        m_inputActions.Enable();

        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerJump();
    }

    /// <summary>
    /// フィールドと接触すれば
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        m_isFlying = true;
    }

    /// <summary>
    /// プレイヤーの移動処理
    /// </summary>
    void PlayerMove()
    {
        // 入力値を取得
        Vector2 inputVec = m_inputActions.Player.Move.ReadValue<Vector2>();

        // 現在ポジションの取得
        Vector3 curPos = transform.position;

        // 次の移動位置計算 ※カメラの向きを反転してるため、横入力の値を反転させる
        Vector3 dirVec = new Vector3(inputVec.x * -1.0f, 0.0f, 0.0f);
        Vector3 nexPos = curPos + dirVec * m_moveVel * Time.deltaTime;

        transform.position = nexPos;
    }

    /// <summary>
    /// プレイヤーのジャンプ処理
    /// </summary>
    void PlayerJump()
    {
        if (m_inputActions.Player.Jump.triggered && m_isFlying)
        {
            m_rigidbody.AddForce(Vector3.up * m_jumpVel, ForceMode.Impulse);
            m_isFlying = false;
        }
    }
}
