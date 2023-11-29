using System.Collections;
using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using DG.Tweening;

public class Credit : MonoBehaviour
{
    [SerializeField] private List<CreditInfo> _creditInfoList = new();
    private float _time = 0.0f;

    private void Start()
    {
        Text();
    }

    private void Text()
    {
        //ヌルチェック
        if (_creditInfoList.Count == 0)
        {
            Debug.LogWarning("クレジットリストが空です");
            return;
        }

        //初期化
        float alpha = 0.0f;
        foreach (var creditInfo in _creditInfoList){ creditInfo.TMP.alpha = alpha; }

        //クレジット表示
        foreach (var creditInfo in _creditInfoList){ Draw(creditInfo); }
    }

    private async void Draw(CreditInfo creditInfo)
    {
        float fadeTime = 0.5f;
        float alpha;

        //文字のフェードイン
        await UniTask.WaitUntil(() => _time > creditInfo.StartTime);
        alpha = 1.0f;
        creditInfo.TMP.DOFade(alpha, fadeTime).SetLink(gameObject);

        //文字のフェードアウト
        await UniTask.WaitUntil(() => _time > creditInfo.StartTime + creditInfo.DisplayTime);
        alpha = 0.0f;
        creditInfo.TMP.DOFade(alpha, fadeTime).SetLink(gameObject);
    }

    private void FixedUpdate()
    {
        _time += Time.deltaTime;
    }
}

//会話情報リスト
[Serializable]
public class CreditInfo
{
    [CustomLabel("文字を表示するTMP")]
    public TMPro.TextMeshProUGUI TMP;
    [CustomLabel("表示開始時間")]//(Instance生成から計測)
    public float StartTime;
    [CustomLabel("表示時間")]
    public float DisplayTime;
}
