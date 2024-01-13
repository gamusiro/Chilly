using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class TitleCameraPhaseManager : LastCameraPhaseManager
{
    private bool _canUpdate;

    private async void Start()
    {
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        //カメラの優先度をリセットする
        foreach (var virtualCamera in _virtualCamera) { virtualCamera.Priority = 0; }
        _cameraIndex = 0;
        _virtualCamera[_cameraIndex].Priority = 1;

        //カメラの遷移処理

        while (true)
        {
            if (GetCurrentCameraIndex() == 0)
            {
                if (Input.GetKeyDown(KeyCode.K))//Bボタンにする　※
                {
                    NextCamera();
                    _canUpdate = true;
                }
            }
            else if (GetCurrentCameraIndex() == 1)
            {
                if (Input.GetKeyDown(KeyCode.K))//Aボタンにする　※
                {
                    NextCamera();
                    _canUpdate = false;
                }
            }

            await UniTask.WaitForFixedUpdate();
        } 
    }

    //カメラを切り替える
    public new void NextCamera()
    {
        _virtualCamera[_cameraIndex].Priority = 0;
        _cameraIndex = 1 - _cameraIndex;
        _virtualCamera[_cameraIndex].Priority = 1;
    }

    public int GetCurrentCameraIndex()
    {
        return _cameraIndex;
    }

    public bool GetCanUpdate()
    {
        return _canUpdate;
    }
}