using JetBrains.Rider.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_ControlGravity : MonoBehaviour
{
    [SerializeField, CustomLabel("重力の強さ")]
    [Range(1.0f, 1000.0f)]
    float m_strength;

    Rigidbody m_rigidBody;
    Vector3 m_gravity;

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_gravity = new Vector3(0.0f, -m_strength, 0.0f);
    }

    /// <summary>
    /// インスペクターからの値変更
    /// </summary>
    private void OnValidate()
    {
        m_gravity = new Vector3(0.0f, -m_strength, 0.0f);
    }

    /// <summary>
    /// 毎フレーム更新
    /// </summary>
    private void FixedUpdate()
    {
        m_rigidBody.AddForce(m_gravity, ForceMode.Acceleration);
    }
}
