using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LastPlayerPhaseManager : AbstructPlayerPhaseManager
{
    //フェーズの設定
    protected new enum Phase { Running,Max };
    protected new Phase _phaseIndex = Phase.Running;

    //プレイヤー
    [SerializeField] private GameObject _player;
    [SerializeField] private LastPlayer _playerCS;
    private Rigidbody _rigidbody;
    private float _speed;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        switch (_phaseIndex)
        {
            case Phase.Running://移動
                Run();
                if (_playerCS.GetOnJumpRamp())
                    _rigidbody.AddForce(new Vector3(0.0f,200.0f, 0.0f));
                break;
        }
    }

    //初期化
    protected new void Init()
    {
        //フェーズを初期化する
        _phaseIndex = 0;

        //プレイヤー
        _rigidbody = _player.GetComponent<Rigidbody>();
        if (!_rigidbody)
            Debug.LogWarning("プレイヤーにRigidbodyがありません");

        _playerCS = _player.GetComponent<LastPlayer>();
        if (!_playerCS)
            Debug.LogWarning("プレイヤーにLastPlayerがありません");

        _speed = 80.0f;
    }

    private void Run()
    {
        Vector3 velocity = _rigidbody.velocity; ;
        velocity.z = -_speed;
        _rigidbody.velocity = velocity;
    }
}
