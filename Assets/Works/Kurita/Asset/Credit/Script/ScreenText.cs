using System.Collections;
using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;

public class ScreenText : ScreenTextParent
{
    [SerializeField] private TextPanel _textPanel;
    [SerializeField] private TMPro.TextMeshProUGUI _contentTMP;//文字を表示するTMP
    [SerializeField] private List<ScreenTextInfo> _screenTextInfoList = new();
    private float _time = 0.0f;

    private void Start()
    {
        Text();
    }

    private void Text()
    {
        //ヌルチェック
        if (_screenTextInfoList.Count == 0)
        {
            Debug.LogWarning("クレジットリストが空です");
            return;
        }

        //初期化
        float alpha = 0.0f;
        foreach (var screenText in _screenTextInfoList){ _contentTMP.alpha = alpha; }

        //クレジット表示
        foreach (var screenText in _screenTextInfoList){ Display(screenText); }
    }

    private async void Display(ScreenTextInfo screenText)
    {
        float fadeTime = 0.5f;
        float alpha;

        //文字のフェードイン
        await UniTask.WaitUntil(() => _time > screenText.StartTime);
        _contentTMP.text = screenText.Content;//表示内容を更新
        alpha = 1.0f;//フェードの設定
        _contentTMP.DOFade(alpha, fadeTime)
            .SetLink(gameObject);
        _textPanel.Show();//テキストパネルの表示

        //文字のフェードアウト
        await UniTask.WaitUntil(() => _time > screenText.StartTime + screenText.DisplayTime);
        alpha = 0.0f;
        _contentTMP.DOFade(alpha, fadeTime)
           // .OnComplete(_textPanel.Hide)
            .SetLink(gameObject);
    }

    private void FixedUpdate()
    {
        _time += Time.deltaTime;
    }
}

//会話情報リスト
[Serializable]
public class ScreenTextInfo
{
    [CustomLabel("表示内容"), TextArea]
    public string Content;
    [CustomLabel("表示開始時間")]//(Instance生成から計測)
    public float StartTime;
    [CustomLabel("表示時間")]
    public float DisplayTime;
}

//インスタンシエイト