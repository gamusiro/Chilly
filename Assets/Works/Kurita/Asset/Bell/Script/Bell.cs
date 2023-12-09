using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;

public class Bell : MonoBehaviour
{
    [SerializeField] private GameObject _root;
    private bool RingFlag = false;
    private float radian = 0.0f;
    [SerializeField] private AudioClip SE;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        Ring();
    }

    private async void Ring()
    {
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        await UniTask.WaitUntil(() => RingFlag, cancellationToken: token);
        _audioSource.PlayOneShot(SE);
        while (true)
        {
            //ベルを揺らす
            radian += Time.deltaTime;
            float range = 40.0f;
            Vector3 rotate = _root.transform.eulerAngles;
            rotate.z = Mathf.Sin(Mathf.PI * 6.0f / 6.0f + radian) * range;
            _root.transform.eulerAngles = rotate;

            int timeSpan = 1;
            await UniTask.Delay(timeSpan, cancellationToken: token);

            //脱出
            if (RingFlag)
                continue;

            rotate = _root.transform.eulerAngles;
            rotate.z = 0.0f;
            _root.transform.eulerAngles = rotate;
            return;
        }
    }

    public void SetRing(bool flag)
    {
        RingFlag = flag;
    }

    public Vector3 GetPosition()
    {
        return _root.transform.position;
    }
}
