using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cysharp.Threading.Tasks;
using System;

public class LastMovieManager : MonoBehaviour
{    
    //プレイヤー
    [SerializeField] private LastPlayeraa _playerCS;

    //エネミー
    [SerializeField] private Enemy _enemy;

    //ムーブオブジェクト
    [SerializeField] Transform _moveObjectTransform;

    //音波
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
        //移動オブジェクト
        MoveObject();
    }

    //移動オブジェクト
    private void MoveObject()
    {
        Vector3 position = _moveObjectTransform.transform.position;
        position.z -= 3.0f;
        _moveObjectTransform.transform.position = position;
    }

    //音波
    private void SoundWave()
    {
        //ベルに当たったら音波を出す
        Instantiate(_soundWave, _playerCS.transform.position, Quaternion.identity);
        _playerCS.OnBell = false;
    }
}
