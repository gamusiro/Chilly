using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cysharp.Threading.Tasks;
using System;

public class LastMovieManager : MonoBehaviour
{
    [SerializeField, CustomLabel("フェーズ1オブジェクト")]
    private GameObject _phase1;

    //プレイヤー
    [SerializeField] private LastPlayeraa _playerCS;

    //エネミー
    [SerializeField] private Enemy _enemy;

    //ムーブオブジェクト
    [SerializeField] Transform _moveObjectTransform;

    //音波
    [SerializeField] private SoundWave _soundWave;

    [SerializeField, CustomLabel("フェーズ2オブジェクト")]
    private GameObject _phase2;

    //友達のプレハブ
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

    //移動オブジェクト
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
