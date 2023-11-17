using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cysharp.Threading.Tasks;

public class LastPlayerPhaseManager : MonoBehaviour
{    
    //プレイヤー
    [SerializeField] private LastPlayeraa _playerCS;

    //エネミー
    [SerializeField] private Enemy _enemy;

    //ムーブオブジェクト
    [SerializeField] Transform _moveObjectTransform;

    //音波
    [SerializeField] private SoundWave _soundWave;

    private void Start()
    {

    }

    private async void FixedUpdate()
    {
        MoveObject();

        //ベルに当たったら音波を出す
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
