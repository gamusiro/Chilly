using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cysharp.Threading.Tasks;
using System;

public class LastMovieManager : MonoBehaviour
{    
    //�v���C���[
    [SerializeField] private LastPlayeraa _playerCS;

    //�G�l�~�[
    [SerializeField] private Enemy _enemy;

    //���[�u�I�u�W�F�N�g
    [SerializeField] Transform _moveObjectTransform;

    //���g
    [SerializeField] private SoundWave _soundWave;

    private async void Start()
    {
        await UniTask.WaitUntil(() => _playerCS.OnBell);
        this.SoundWave();
       

        await UniTask.Delay(TimeSpan.FromSeconds(4.0f));
        _enemy.Disapper();

        await UniTask.Delay(TimeSpan.FromSeconds(4.0f));
        _enemy.Explosion();

    }

    private void FixedUpdate()
    {
        //�ړ��I�u�W�F�N�g
        MoveObject();
    }

    //�ړ��I�u�W�F�N�g
    private void MoveObject()
    {
        Vector3 position = _moveObjectTransform.transform.position;
        position.z -= 3.0f;
        _moveObjectTransform.transform.position = position;
    }

    //���g
    private void SoundWave()
    {
        //�x���ɓ��������特�g���o��
        Instantiate(_soundWave, _playerCS.transform.position, Quaternion.identity);
        _playerCS.OnBell = false;
    }
}
