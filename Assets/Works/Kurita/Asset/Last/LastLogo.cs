using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;

public class LastLogo : MonoBehaviour
{
    private bool _isChangeAlpha = false;
    [SerializeField] private List<Renderer> _rendererList;

    private async void Start()
    {
        //ƒLƒƒƒ“ƒZƒ‹
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        //”ñ•\Ž¦
        float alpha = 0.0f;
        float time = 0.0f;
        ChangeAlpha(alpha, time);

        await UniTask.WaitUntil(() => _isChangeAlpha, cancellationToken: token);
        Debug.Log(_isChangeAlpha);

        //•\Ž¦‚·‚é
        alpha = 1.0f;
        time = 3.0f;
        ChangeAlpha(alpha, time);
    }

    public void Show()
    {
        _isChangeAlpha = true;
    }

    private void ChangeAlpha(float alpha, float time)
    {
        //”Z’W‚ð”Z‚­‚·‚é
        foreach (var renderer in _rendererList)
        {
            renderer.material.DOFade(alpha, time)
                .SetLink(this.gameObject);
        }
    }
}
