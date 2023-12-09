using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using Cinemachine;
using System.Threading;

public class CameraPhaseManager : AbstractBasePhaseManager
{
    //バーチャルカメラの設定
    [SerializeField] protected List<CinemachineVirtualCamera> _virtualCamera = new List<CinemachineVirtualCamera>();
    protected int _cameraIndex;

    //初期化
    private async void Start()
    {
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        //カメラの優先度をリセットする
        foreach (var virtualCamera in _virtualCamera) { virtualCamera.Priority = 0; }
        _cameraIndex = 0;
        _virtualCamera[_cameraIndex].Priority = 1;

        //カメラの遷移処理
        while (_cameraIndex < _transTimeList.Count)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_transTimeList[_cameraIndex]), cancellationToken: token);
            NextCamera();
        }
    }

    //カメラを切り替える
    protected void NextCamera()
    {
        if (_cameraIndex + 1 >= _virtualCamera.Count) 
            return;

        _virtualCamera[_cameraIndex].Priority = 0;
        _cameraIndex++;
        _virtualCamera[_cameraIndex].Priority = 1;
    }

    public GameObject GetCurCamera()
    {
        return _virtualCamera[_cameraIndex].gameObject;
    }
}