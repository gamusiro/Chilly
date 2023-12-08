using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;

public class OpeningMovieManager : MonoBehaviour
{
    //ムーブオブジェクト
    [SerializeField] Transform _moveObjectTransform;

    //エネミー
    [SerializeField] private ParticleSystem _ateParticle;

    //友達のプレハブ
    [SerializeField] private OpeningFriend _friend;

    //吸い込まれる処理(線形補完)
    [SerializeField] private Transform _friendStartTransform;
    [SerializeField] private Transform _enemyMouthTransform;

    //フェード
    [SerializeField] private Fade _fade;
    [SerializeField] string _sceneName;

    private async void Start()
    {
        CS_AudioManager.Instance.MasterVolume = 1.0f;
        CS_AudioManager.Instance.PlayAudio("Opening", true);

        //初期化
        _ateParticle.Stop();
   
        MoveObject();

        //吸い込み開始
        await UniTask.Delay(TimeSpan.FromSeconds(18.0f));
        _ateParticle.Play();
        await UniTask.Delay(TimeSpan.FromSeconds(2.0f));
        _friend.Ate(_friend.transform, _enemyMouthTransform);
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        _fade.FadeIn(0.5f);
        await UniTask.Delay(TimeSpan.FromSeconds(3.0f));
        _friend.SetDestroy();
        _fade.FadeOut(2.0f);
        _ateParticle.Stop();
        await UniTask.Delay(TimeSpan.FromSeconds(6.0f));
        _fade.FadeIn(2.0f);
        await UniTask.Delay(TimeSpan.FromSeconds(2.0f));

        //シーン遷移
        CS_AudioManager.Instance.StopBGM();
        SceneManager.LoadScene(_sceneName);

        ////ベルがなを鳴らす
        //await UniTask.WaitUntil(() => _playerCS.OnBell);
        //_bell.SetRing(true);
        //Instantiate(_soundWave, _bell.GetPosition(), Quaternion.identity, _phase1.transform);
        //_playerCS.OnBell = false;

        ////敵がやられた判定になる
        //await UniTask.Delay(TimeSpan.FromSeconds(4.0f));
        //_enemy.Disapper(_phase1.transform);
        ////Destroy(_moveObjectTransform.gameObject);

        ////爆発後
        //await UniTask.Delay(TimeSpan.FromSeconds(5.0f));
        //_enemy.Explosion(_phase1.transform);

        ////フェーズ1終了,フェーズ2開始
        //await UniTask.Delay(TimeSpan.FromSeconds(5.0f));
        //_bell.SetRing(false);
        //Destroy(_phase1);
        //Instantiate(_phase2);
        //_fade.FadeIn(0.0f);
        //_fade.FadeOut(1.0f);

        ////友達生成
        //await UniTask.Delay(TimeSpan.FromSeconds(1.0f));
        //Instantiate(_friendPrefab, new Vector3(0.0f, 500.0f, 30.0f), Quaternion.identity);

        //  await UniTask.Delay(TimeSpan.FromSeconds(1.0f));
        // _fade.FadeIn(3.0f);
    }

    private void FixedUpdate()
    {
       //MoveObject();
    }

    //移動オブジェクト
    private async void MoveObject()
    {
        while (true)
        {
            //ヌルチェック
            if (_moveObjectTransform == null)
                return;

            Vector3 position = _moveObjectTransform.localPosition;
            position.z -= 3.0f;
            _moveObjectTransform.localPosition = position;

            await UniTask.WaitForFixedUpdate();
        }
    }
}
