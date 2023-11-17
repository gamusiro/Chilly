using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cysharp.Threading.Tasks;

public class LastPlayerPhaseManager : MonoBehaviour
{    
    //�v���C���[
    [SerializeField] private LastPlayeraa _playerCS;

    //�G�l�~�[
    [SerializeField] private Enemy _enemy;

    //���[�u�I�u�W�F�N�g
    [SerializeField] Transform _moveObjectTransform;

    //���g
    [SerializeField] private SoundWave _soundWave;

    private void Start()
    {

    }

    private async void FixedUpdate()
    {
        MoveObject();

        //�x���ɓ��������特�g���o��
        await UniTask.WaitUntil(() => _playerCS.OnBell);
       // Instantiate(_soundWave);
        _playerCS.OnBell = false;
    }

    private void MoveObject()
    {
        Vector3 position = _moveObjectTransform.transform.position;
        position.z -= 3.0f;
        _moveObjectTransform.transform.position = position;
    }
}
