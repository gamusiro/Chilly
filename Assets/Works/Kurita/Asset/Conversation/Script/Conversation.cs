using System.Collections;
using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

public class Conversation : ScreenTextParent
{
    [SerializeField] private TextPanel _textPanel;
    [SerializeField] private TMPro.TextMeshProUGUI _nameTMP;//名前を表示するTMP
    [SerializeField] private TMPro.TextMeshProUGUI _contentTMP;//会話分を表示するTMP
    [SerializeField] private List<ConversationInfo> _conversationInfoList = new();
    private float _time = 0.0f;

    private void Start()
    {
        Text();
    }

    private void Text()
    {
        //ヌルチェック
        if (_conversationInfoList.Count == 0)
        {
            Debug.LogWarning("会話情報リストが空です");
            return;
        }

        //初期化
        _nameTMP.text = null;
        _contentTMP.text = null;

        //会話開始
        foreach (var conversationInfo in _conversationInfoList) { Display(conversationInfo); }
    }

    private async void Display(ConversationInfo conversationInfo)
    {
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        //指定した時間まで待機
        await UniTask.WaitUntil(() => _time > conversationInfo.StartTime, cancellationToken: token);

        _textPanel.Show();//テキストパネルの表示

        _nameTMP.text = conversationInfo.Name;//名前の表示

        //会話文の表示
        float timeSpan = 0.5f;
        await UniTask.Delay(TimeSpan.FromSeconds(timeSpan), cancellationToken: token);
        string content = conversationInfo.Content;
        int len = 0;

        while (len <= content.Length)
        {
            _contentTMP.text = content.Substring(0, len);
            len++;

            //n秒ごとに文字が表示されていく
            timeSpan = 1.0f / content.Length;
            await UniTask.Delay(TimeSpan.FromSeconds(timeSpan), cancellationToken: token);
        }

        //表示の終了
        await UniTask.WaitUntil(()=> _time > conversationInfo.StartTime + conversationInfo.DisplayTime, cancellationToken: token);
        _nameTMP.text = null;
        _contentTMP.text = null;

        _textPanel.Show();//テキストパネルの非表示
    }

    private void FixedUpdate()
    {
        _time += Time.deltaTime;
    }
}

//会話情報リスト
[Serializable]
public class ConversationInfo 
{
    [CustomLabel("名前")]
    public string Name;
    [CustomLabel("会話内容")]
    public string Content;
    [CustomLabel("表示開始時間")]
    public float StartTime;
    [CustomLabel("表示時間")]
    public float DisplayTime;
}