using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LastPlayerPhaseManager : AbstructPlayerPhaseManager
{
    //�t�F�[�Y�̐ݒ�
    protected new enum Phase { Running,Max };
    protected new Phase _phaseIndex = Phase.Running;

    //�v���C���[
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
            case Phase.Running://�ړ�
                Run();
                if (_playerCS.GetOnJumpRamp())
                    _rigidbody.AddForce(new Vector3(0.0f,200.0f, 0.0f));
                break;
        }
    }

    //������
    protected new void Init()
    {
        //�t�F�[�Y������������
        _phaseIndex = 0;

        //�v���C���[
        _rigidbody = _player.GetComponent<Rigidbody>();
        if (!_rigidbody)
            Debug.LogWarning("�v���C���[��Rigidbody������܂���");

        _playerCS = _player.GetComponent<LastPlayer>();
        if (!_playerCS)
            Debug.LogWarning("�v���C���[��LastPlayer������܂���");

        _speed = 80.0f;
    }

    private void Run()
    {
        Vector3 velocity = _rigidbody.velocity; ;
        velocity.z = -_speed;
        _rigidbody.velocity = velocity;
    }
}
