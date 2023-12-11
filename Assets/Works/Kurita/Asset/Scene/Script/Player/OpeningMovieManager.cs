using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;
using System.Threading;

public class OpeningMovieManager : MonoBehaviour
{
    //ムーブオブジェクト
    [SerializeField] Transform _moveObjectTransform;

    //エネミー
    [SerializeField] private ParticleSystem _ateParticle;

    //友達
    [SerializeField] private OpeningFriend _friend;

    //吸い込まれる処理(線形補完)
    [SerializeField] private Transform _friendStartTransform;
    [SerializeField] private Transform _enemyMouthTransform;

    //フェード
    [SerializeField] private Fade_K _fade;
    [SerializeField] string _sceneName;

    private async void Start()
    {
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        CS_AudioManager.Instance.MasterVolume = 1.0f;
        CS_AudioManager.Instance.PlayAudio("Opening", true);

        //初期化
        _ateParticle.Stop();
   
        MoveObject();

        //吸い込み開始
        await UniTask.Delay(TimeSpan.FromSeconds(18.0f), cancellationToken: token);
        _ateParticle.Play();
        await UniTask.Delay(TimeSpan.FromSeconds(2.0f), cancellationToken: token);
        _friend.Ate(_friend.transform, _enemyMouthTransform);
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: token);
        _fade.FadeIn(0.5f);
        await UniTask.Delay(TimeSpan.FromSeconds(3.0f), cancellationToken: token);
        _friend.SetDestroy();
        _fade.FadeOut(2.0f);
        _ateParticle.Stop();
        await UniTask.Delay(TimeSpan.FromSeconds(6.0f), cancellationToken: token);
        _fade.FadeIn(2.0f);
        await UniTask.Delay(TimeSpan.FromSeconds(2.0f), cancellationToken: token);

        //シーン遷移
        CS_AudioManager.Instance.StopBGM();
        SceneManager.LoadScene(_sceneName);
    }


    //移動オブジェクト
    private async void MoveObject()
    {
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        while (true)
        {
            //ヌルチェック
            if (_moveObjectTransform == null)
                return;

            Vector3 position = _moveObjectTransform.localPosition;
            position.z -= 3.0f;
            _moveObjectTransform.localPosition = position;

            await UniTask.WaitForFixedUpdate(cancellationToken: token);
        }
    }
}
