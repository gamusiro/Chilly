using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

public class Transition : MonoBehaviour
{
    [SerializeField] private float _transTime;//148.0f;
    [SerializeField] private GameObject GameInstance;
    [SerializeField] private GameObject LastPrefab;

    private async void Start()
    {
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        await UniTask.WaitUntil(() => CS_AudioManager.Instance.TimeBGM >= _transTime, cancellationToken: token);
        Destroy(GameInstance);//ゲームシーンを削除
        Instantiate(LastPrefab, Vector3.zero, Quaternion.identity);//ラストシーンを生成
    }
}
