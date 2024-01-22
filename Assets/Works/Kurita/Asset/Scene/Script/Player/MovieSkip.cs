using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Threading;

public class MovieSkip : MonoBehaviour
{
    [SerializeField] private string _nextStageName;
    [SerializeField] private TMPro.TextMeshProUGUI _text;
    private bool _showText = false;
    private bool _islooping = true;

    [SerializeField] private PlayerInput _playerInput;

    private async void Start()
    {
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        //色を薄くする
        {
            float alpha = 0.0f;
            float duration = 0.0f;
            _text
                .DOFade(alpha, duration)
                .SetLink(this.gameObject);
        }

        while (_islooping)
        {
            //プラスボタンが押されたとき
            await UniTask.WaitUntil(() => { return this ? _playerInput.currentActionMap["Skip"].triggered : false ;}, cancellationToken: token);

            if (!_showText)
            {
                _showText = true;

                //色を濃くする
                {
                    float alpha = 1.0f;
                    float duration = 0.5f;
                    _text
                        .DOFade(alpha, duration)
                        .OnComplete(() => HideText())
                        .SetLink(this.gameObject);
                }
            }
            else if(_showText)
            {
                //シーン遷移
                _islooping = false;
                CS_AudioManager.Instance.MasterVolume = 0.0f;
                CS_AudioManager.Instance.StopBGM();
                CS_AudioManager.Instance.StopAllSE();
                SceneManager.LoadScene(_nextStageName);
                return;
            }
        }
    }

    private async void HideText()
    {
        float timeSpan = 4.0f;
        await UniTask.Delay(System.TimeSpan.FromSeconds(timeSpan));

        //色を濃くする
        {
            float alpha = 0.0f;
            float duration = 0.5f;

            if (_text == null)
                return;

            _text.DOKill();
            _text
                .DOFade(alpha, duration)
                .OnComplete(() => _showText = false)
                .SetLink(this.gameObject);
        }
    }
}
