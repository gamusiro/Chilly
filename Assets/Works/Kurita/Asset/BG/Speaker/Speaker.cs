using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;

public class Speaker : MonoBehaviour
{
    [SerializeField] private SoundWave _soundWavePrefab;
    [SerializeField] private Transform circleTransformA;
    [SerializeField] private Transform circleTransformB;
    [SerializeField] private float _bpm = 0.0f;
    private bool _flag = true;


    private void Start()
    {
        SoundWave();
    }

    private async void SoundWave()
    {
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        //一拍ごとに音波を出す
        while (_flag)
        {
            Instantiate(_soundWavePrefab, circleTransformA.position, circleTransformB.rotation, this.transform);
            Instantiate(_soundWavePrefab, circleTransformB.position, circleTransformB.rotation, this.transform);

            float minutes = 60.0f;
            float fourBeats = 1.0f / 4.0f;
            float oneMeaser = Mathf.Floor(_bpm / minutes) - (_bpm / minutes - Mathf.Floor(_bpm / minutes));
            float timeSpan = oneMeaser * fourBeats;

            await UniTask.Delay(TimeSpan.FromSeconds(timeSpan), cancellationToken: token);
        }
    }

    private void OnDestroy()
    {
        _flag = false;
    }
}