using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;
using System.Threading;
using DG.Tweening;

public class LastMovieManager : MovieManager
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

    //タイトルロゴ
    [SerializeField] private LastLogo _lastLogoFirstShow;
    [SerializeField] private LastLogo _lastLogoSecondShow;

    private async void Start()
    {
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        CS_AudioManager.Instance.PlayAudio("Last", true);
        CS_AudioManager.Instance.MasterVolume = 1.0f;

        //■ベルを鳴らす
        {
            await UniTask.WaitUntil(() => _playerCS.OnBell, cancellationToken: token);
            await UniTask.WaitUntil(() => this != null, cancellationToken: token);

            _bell.SetRing(true);
            Instantiate(_soundWave, _bell.GetPosition(), Quaternion.identity, _phase1.transform);
            _playerCS.OnBell = false;
        }

        //■敵がやられた判定になる
        {
            float time = 4.0f;
            await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: token);
            await UniTask.WaitUntil(() => this != null, cancellationToken: token);

            _enemy.Disapper(_enemy.transform);
        }

        //■爆発後
        {
            float time = 5.0f;
            await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: token);
            await UniTask.WaitUntil(() => this != null, cancellationToken: token);

            _enemy.Explosion(_phase1.transform, this.gameObject);
        }

        //■フェーズ1終了,フェーズ2開始
        {
            float time = 5.0f;
            await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: token);
            await UniTask.WaitUntil(() => this != null, cancellationToken: token);

            _bell.SetRing(false);

            Destroy(_phase1);
            Instantiate(_phase2);

            time = 0.0f;
            _fade.FadeIn(time);

            time = 1.0f;
            _fade.FadeOut(time);
        }

        //■友達生成
        {
            float time = 1.0f;
            await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: token);
            await UniTask.WaitUntil(() => this != null, cancellationToken: token);

            Vector3 spawnPosition = new Vector3(0.0f, 500.0f, 30.0f);
            Instantiate(_friendPrefab, spawnPosition, Quaternion.identity);
        }

        //最初のLogoの表示
        {
            float time = 18.5f;
            await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: token);
            await UniTask.WaitUntil(() => this != null, cancellationToken: token);
            _lastLogoFirstShow.Show();
        }

        //二つ目のLogoの表示
        {
            float time = 3.0f;
            await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: token);
            await UniTask.WaitUntil(() => this != null, cancellationToken: token);

            _lastLogoSecondShow.Show();
            CS_AudioManager.Instance.MasterVolume = 2.0f;
        }

        //■シーンの遷移
        {
            float time = 4.0f;
            await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: token);
            await UniTask.WaitUntil(() => this != null, cancellationToken: token);

            CS_AudioManager.Instance.MasterVolume = 0.0f;
            CS_AudioManager.Instance.StopBGM();
            SceneManager.LoadScene(_nextScene);
        }
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        MoveObject();
    }

    //移動オブジェクト
    private void MoveObject()
    {
        if (_moveObjectTransform == null)
            return;

        Vector3 position = _moveObjectTransform.localPosition;
        position.z -= 3.0f;
        _moveObjectTransform.localPosition = position;
    }
}
