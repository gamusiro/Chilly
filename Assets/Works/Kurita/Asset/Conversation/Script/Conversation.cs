using System.Collections;
using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine.UI;
using DG.Tweening;

public class Conversation : ScreenTextParent
{
    public enum IconList { Player, Friend, Enemy }

    [SerializeField] private TextPanel _textPanel;
    [SerializeField] private TMPro.TextMeshProUGUI _nameTMP;//名前を表示するTMP
    [SerializeField] private TMPro.TextMeshProUGUI _contentTMP;//会話分を表示するTMP
    [SerializeField] private Image _icon;
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
        this.HideIcon();

        //会話開始
        foreach (var conversationInfo in _conversationInfoList) { Display(conversationInfo); }
    }

    private async void Display(ConversationInfo conversationInfo)
    {
        //キャンセル
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        //指定した時間まで待機
        await UniTask.WaitUntil(() => _time > conversationInfo.StartTime, cancellationToken: token);

        //テキストパネルの表示
        _textPanel.Show();

        //名前の表示
        _nameTMP.text = conversationInfo.Name;

        //アイコンの表示位置を設定
        ShowIcon(conversationInfo, token);

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
        this.HideIcon();

        _textPanel.Show();//テキストパネルの非表示
    }

    private async void ShowIcon(ConversationInfo conversationInfo, CancellationToken token)
    {
        //アイコンが設定されていなければリターン
        if (conversationInfo.Icon == null)
        {
            this.HideIcon();
            return;
        }

        //アイコンの表示位置はTMPの情報をもとに設定する。
        //TMPの情報更新にはラグがあるので待機する。
        await UniTask.WaitUntil(() => _nameTMP.text.Length == _nameTMP.textInfo.characterCount);

        //スプライトを設定する
        _icon.sprite = conversationInfo.Icon;
        float alpha = 1.0f;
        float time = 0.0f;
        _icon.material.DOFade(alpha, time)
            .SetLink(this.gameObject);

        //座標を設定する
        float half = 0.5f;
        float correct = 50.0f;
        Vector3 position = Vector3.zero;
        position += _nameTMP.transform.position;
        position += _nameTMP.textInfo.characterInfo[0].topLeft * half;
        position += _nameTMP.textInfo.characterInfo[0].bottomRight * half;
        position.x -= correct;
        _icon.transform.position = position;
    }

    private void FixedUpdate()
    {
        _time += Time.deltaTime;
    }

    private void HideIcon()
    {
        float alpha = 0.0f;
        float time = 0.0f;
        _icon.material.DOFade(alpha, time)
            .SetLink(this.gameObject);
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
    [CustomLabel("アイコン")]
    public Sprite Icon;
}