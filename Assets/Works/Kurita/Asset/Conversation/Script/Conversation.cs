using System.Collections;
using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

public class Conversation : ScreenTextParent
{
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
        foreach (var conversationInfo in _conversationInfoList) { Draw(conversationInfo); }
    }

    private async void Draw(ConversationInfo conversationInfo)
    {
        ////経過時間
        //await UniTask.WaitForFixedUpdate();
        //_time += Time.deltaTime;

        //文字の表示を開始
        await UniTask.WaitUntil(() => _time > conversationInfo.StartTime);

        //名前の表示
        _nameTMP.text = conversationInfo.Name;


        //会話文の表示
        float timeSpan = 0.5f;
        await UniTask.Delay(TimeSpan.FromSeconds(timeSpan));
        string content = conversationInfo.Content;
        int len = 0;

        //文字の表示
        while (len <= content.Length)
        {
            //文字を表示する
            _contentTMP.text = content.Substring(0, len);
            len++;

            //n秒ごとに文字が表示されていく
            timeSpan = 1.0f / content.Length;
            await UniTask.Delay(TimeSpan.FromSeconds(timeSpan));

            Debug.Log("aa");
        }

        //これあまりよろしくない。
        while (true)
        {
            //文字を表示し終わる
            if (_time > conversationInfo.StartTime + conversationInfo.DisplayTime)
            {
                _nameTMP.text = null;
                _contentTMP.text = null;
                return;
            }
            await UniTask.WaitForFixedUpdate();
        }
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
