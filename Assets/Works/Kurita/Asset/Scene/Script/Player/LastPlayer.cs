using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

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

    private void Start()
    {
        //ジャンプ
        Jump();

        //移動
        Move();
    }

    //移動
    private async void Move()
    {
        while (true)
        {
            Vector3 force = new Vector3(0.0f, -m_gravity, 0.0f);
            _rigidbody.AddForce(force);

            await UniTask.WaitForFixedUpdate();
        }
    }

    //ジャンプ処理
    private async void Jump()
    {
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        //ジャンプ判定がTrueになったらジャンプ
        await UniTask.WaitUntil(() => _onJumpRamp, cancellationToken: token);
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
