using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;
using Cysharp.Threading.Tasks;
using System;
using UnityEditor;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private Fade_K _fade;
    [SerializeField] private string _gameoverScene;

    private async void Start()
    {
        //�L�����Z��
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        //�\�����I����ăt�F�[�h�C��
        float timeSpan = 4.0f;
        await UniTask.Delay(TimeSpan.FromSeconds(timeSpan), cancellationToken : token);
        float time = 3.0f;
        _fade.FadeIn(time);

        //�J�ڏ���
        timeSpan = 3.0f;
        await UniTask.Delay(TimeSpan.FromSeconds(timeSpan), cancellationToken: token);

        //����
        //await UniTask.WaitUntil()
        SceneManager.LoadScene(_gameoverScene);
    }
}
