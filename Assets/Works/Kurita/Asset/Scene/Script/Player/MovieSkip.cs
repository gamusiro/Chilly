using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class MovieSkip : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _text;
    private bool _showText = false;

    private async void Start()
    {
        //色を薄くする
        {
            float alpha = 0.0f;
            float duration = 0.0f;
            _text
                .DOFade(alpha, duration)
                .SetLink(this.gameObject);
        }

        while (true)
        {
            //プラスボタンが押されたとき　※
            await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.K));

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
                //シーン遷移　※
                Debug.Log("シーンが遷移しました。");
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
            _text.DOKill();
            _text
                .DOFade(alpha, duration)
                .OnComplete(() => _showText = false)
                .SetLink(this.gameObject);
        }
    }
}
