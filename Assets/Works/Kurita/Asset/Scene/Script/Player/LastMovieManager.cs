using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cysharp.Threading.Tasks;
using System;

public class LastMovieManager : MonoBehaviour
{
    [SerializeField, CustomLabel("�t�F�[�Y1�I�u�W�F�N�g")]
    private GameObject _phase1;

    //�v���C���[
    [SerializeField] private LastPlayeraa _playerCS;

    //�G�l�~�[
    [SerializeField] private Enemy _enemy;

    //���[�u�I�u�W�F�N�g
    [SerializeField] Transform _moveObjectTransform;

    //���g
    [SerializeField] private SoundWave _soundWave;

    [SerializeField, CustomLabel("�t�F�[�Y2�I�u�W�F�N�g")]
    private GameObject _phase2;

    //�F�B�̃v���n�u
    [SerializeField] private GameObject _friendPrefab;

    private async void Start()
    {
        MoveObject();

        await UniTask.WaitUntil(() => _playerCS.OnBell);
        Instantiate(_soundWave, _playerCS.transform.position, Quaternion.identity, _phase1.transform);
        _playerCS.OnBell = false;

        await UniTask.Delay(TimeSpan.FromSeconds(4.0f));
        _enemy.Disapper(_phase1.transform);

        await UniTask.Delay(TimeSpan.FromSeconds(4.0f));
        _enemy.Explosion(_phase1.transform);

        await UniTask.Delay(TimeSpan.FromSeconds(5.0f));
        Destroy(_phase1);
        Instantiate(_phase2);

        await UniTask.Delay(TimeSpan.FromSeconds(1.0f));
        Instantiate(_friendPrefab, new Vector3(0.0f, 450.0f, 30.0f), Quaternion.identity);
    }

    private void FixedUpdate()
    {

     


    }

    //�ړ��I�u�W�F�N�g
    private async void MoveObject()
    {
        while (true)
        {
            if (_moveObjectTransform == null)
                return;

            Vector3 position = _moveObjectTransform.transform.position;
            position.z -= 3.0f;
            _moveObjectTransform.transform.position = position;
            await UniTask.DelayFrame(1);
        }
    }
}
