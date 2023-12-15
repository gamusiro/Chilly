using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using System;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private Fade_K _fade;

    private async void Start()
    {
        //キャンセル
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        //表示が終わってフェードイン
        float timeSpan = 4.0f;
        await UniTask.Delay(TimeSpan.FromSeconds(timeSpan), cancellationToken : token);
        float time = 3.0f;
        _fade.FadeIn(time);

        //遷移処理
        timeSpan = 3.0f;
        await UniTask.Delay(TimeSpan.FromSeconds(timeSpan), cancellationToken: token);
        //ここ
    }
}
