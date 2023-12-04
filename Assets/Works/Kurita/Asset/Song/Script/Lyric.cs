using System.Collections;
using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;

public class Lyric : ScreenTextParent
{
    [SerializeField] private Image _panelImage;//文字の後ろのパネル
    float _alpha = 0.0f;
    [SerializeField] private TMPro.TextMeshProUGUI _lyricTMP;//名前を表示するTMP
    [SerializeField] private List<LyricInfo> _conversationInfoList = new();
    
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
        _lyricTMP.text = null;
        _alpha = _panelImage.color.a;//濃くしたときの色
        float alpha = 0.0f;
        float time = 0.0f;
        _panelImage.DOFade(alpha, time) ;

        //会話開始
        foreach (var conversationInfo in _conversationInfoList) { Draw(conversationInfo); }
    }

    private async void Draw(LyricInfo luricInfo)
    {
        //文字の表示を開始
        await UniTask.WaitUntil(() => CS_AudioManager.Instance.TimeBGM >= luricInfo.StartTime);

        //歌詞の表示
        _lyricTMP.text = luricInfo.Lyric;

        //文字の後ろのパネルの表示開始
        if (luricInfo.Lyric != "")
        {
            float alpha = _alpha;
            float time = 0.0f;
            _panelImage.DOFade(alpha, time).SetLink(this.gameObject);
            TextPanel.Show();
        }
        else
        {
            float alpha = 0.0f;
            float time = 0.0f;
            _panelImage.DOFade(alpha, time).SetLink(this.gameObject);
            TextPanel.Show();
        }
    }
}

//会話情報リスト
[Serializable]
public class LyricInfo
{
    [CustomLabel("歌詞")]
    public string Lyric;
    [CustomLabel("表示開始時間")]
    public float StartTime;
}
