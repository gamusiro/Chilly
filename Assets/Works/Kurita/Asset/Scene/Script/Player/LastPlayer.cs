using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class LastPlayer : MonoBehaviour
{
    // ジャンプ力
    [SerializeField, CustomLabel("ジャンプ力")]
    float _jump;
    [SerializeField] private Rigidbody _rigidbody;

    // 疑似重力
    [SerializeField]
    [Range(1.0f, 1000.0f)]
    float m_gravity;

    //呼び出し用
    private bool _onJumpRamp = false;
    public bool OnBell = false;

    private void FixedUpdate()
    {
        //ジャンプ
        Jump();

        //移動
        Move();
    }

    //移動
    private void Move()
    {
        Vector3 force = new Vector3(0.0f, -m_gravity, 0.0f);
        _rigidbody.AddForce(force);
    }

    //ジャンプ処理
    private async void Jump()
    {
        //ジャンプ判定がTrueになったらジャンプ
        await UniTask.WaitUntil(() => _onJumpRamp);
        _rigidbody.AddForce(new Vector3(0, _jump, 0), ForceMode.Impulse);
        _onJumpRamp = false;
    }

    //フラグ
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "JumpRamp")
            _onJumpRamp = true;

        if (other.tag == "Bell")
            OnBell = true;
    }
}
