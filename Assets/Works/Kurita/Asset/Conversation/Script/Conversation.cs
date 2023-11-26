using System.Collections;
using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

public class Conversation : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _nameTMP;//名前を表示するTMP
    [SerializeField] private TMPro.TextMeshProUGUI _contentTMP;//会話分を表示するTMP
    [SerializeField] private List<ConversationInfo> _conversationInfoList = new();
    int _contentIndex = 0;

    private void Start()
    {
        Content();
    }

    private async void Content()
    {
        //ヌルチェック
        if (_conversationInfoList.Count == 0)
        {
            Debug.LogWarning("会話情報リストが空です");
            return;
        }    

        //名前の表示
        _nameTMP.text = _conversationInfoList[_contentIndex].Name;

        //会話文の表示
        string content = _conversationInfoList[_contentIndex].Content;
        int len = 0;
        while (len <= content.Length)
        {
            //文字を表示する
            _contentTMP.text = content.Substring(0, len);
            len++;

            //n秒ごとに文字が表示されていく
            float timeSpan = 0.1f;
            await UniTask.Delay(TimeSpan.FromSeconds(timeSpan));
        }
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
    [CustomLabel("次の会話に遷るまでの秒数")]
    public float transTime;
}
