using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using DG.Tweening;

public class PressBButton : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;

    void Start()
    {
        Blinking();
    }

    //�_��
    private async void Blinking()
    {
        //�L�����Z��
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        //�_��
        bool next = false;
        float alpha = 0.0f;
        float timeSpan = 1.0f;
        while (true)
        {
            _renderer.material.DOFade(alpha, timeSpan)
                .OnComplete(()=> 
                {
                    alpha = 0.5f - alpha;
                    next = true;
                })
                .SetLink(this.gameObject);

            await UniTask.WaitUntil(() => next);
            next = false;
        }
    }
}
