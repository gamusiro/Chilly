using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;
using System.Threading;

public class LastMovieManager : MonoBehaviour
{
    [SerializeField, CustomLabel("フェーズ1オブジェクト")]
    private GameObject _phase1;

    //プレイヤー
    [SerializeField] private LastPlayer _playerCS;

    //エネミー
    [SerializeField] private LastEnemy _enemy;

    //ムーブオブジェクト
    [SerializeField] Transform _moveObjectTransform;

    //音波
    [SerializeField] private SoundWave _soundWave;

    //ベル
    [SerializeField] private Bell _bell;

    [SerializeField, CustomLabel("フェーズ2オブジェクト")]
    private GameObject _phase2;

    //友達のプレハブ
    [SerializeField] private GameObject _friendPrefab;

    //フェード
    [SerializeField] private Fade_K _fade;
    [SerializeField] string _nextScene; 

    private async void Start()
    {
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        CS_AudioManager.Instance.PlayAudio("Explosion", true);
        CS_AudioManager.Instance.MasterVolume = 1.0f;
        //_fade.FadeOut(0.5f);

        //ベルがなを鳴らす
        await UniTask.WaitUntil(() => _playerCS.OnBell, cancellationToken: token);
        _bell.SetRing(true);
        Instantiate(_soundWave, _bell.GetPosition(), Quaternion.identity, _phase1.transform);
        _playerCS.OnBell = false;

        //敵がやられた判定になる
        await UniTask.Delay(TimeSpan.FromSeconds(4.0f), cancellationToken: token);
        _enemy.Disapper(_enemy.transform);
        //Destroy(_moveObjectTransform.gameObject);

        //爆発後
        await UniTask.Delay(TimeSpan.FromSeconds(5.0f), cancellationToken: token);
        _enemy.Explosion(_phase1.transform);

        //フェーズ1終了,フェーズ2開始
        await UniTask.Delay(TimeSpan.FromSeconds(5.0f), cancellationToken: token);
        _bell.SetRing(false);
        Destroy(_phase1);
        Instantiate(_phase2);

        _fade.FadeIn(0.0f);

        CS_AudioManager.Instance.MasterVolume = 0.0f;
        CS_AudioManager.Instance.StopBGM();
        CS_AudioManager.Instance.PlayAudio("Rescue", true);
        CS_AudioManager.Instance.MasterVolume = 1.0f;

        _fade.FadeOut(1.0f);

        //友達生成
        await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: token);
        Instantiate(_friendPrefab, new Vector3(0.0f, 500.0f, 30.0f), Quaternion.identity);

        await UniTask.Delay(TimeSpan.FromSeconds(22.0f), cancellationToken: token);
        CS_AudioManager.Instance.MasterVolume = 0.0f;
        CS_AudioManager.Instance.StopBGM();
        SceneManager.LoadScene(_nextScene);
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
       MoveObject();
    }

    //移動オブジェクト
    private  void MoveObject()
    {
      //  while (true)
        //{
            //ヌルチェック
            if (_moveObjectTransform == null)
                return;

            Vector3 position = _moveObjectTransform.localPosition;
            position.z -= 3.0f;
            _moveObjectTransform.localPosition = position;

       //     await UniTask.Delay(1);
        //}
    }
}
