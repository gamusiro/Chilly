using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class TitleCameraPhaseManager : LastCameraPhaseManager
{
    private float _elapsedTime = 0.0f;

    private async void Start()
    {
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        //�J�����̗D��x�����Z�b�g����
        foreach (var virtualCamera in _virtualCamera) { virtualCamera.Priority = 0; }
        _cameraIndex = 0;
        _virtualCamera[_cameraIndex].Priority = 1;

        //�J�����̑J�ڏ���

        await UniTask.Delay(TimeSpan.FromSeconds(2.0f));
        NextCamera();
    }

    //�J������؂�ւ���
    protected new void NextCamera()
    {
        _virtualCamera[_cameraIndex].Priority = 0;
        _cameraIndex++;
        _virtualCamera[_cameraIndex].Priority = 1;
    }
}