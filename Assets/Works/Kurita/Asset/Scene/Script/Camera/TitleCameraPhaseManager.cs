using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class TitleCameraPhaseManager : LastCameraPhaseManager
{
    private bool _canUpdate;

    [SerializeField, CustomLabel("入力")]
    PlayerInput _input;

    private async void Start()
    {
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        //カメラの優先度をリセットする
        foreach (var virtualCamera in _virtualCamera) { virtualCamera.Priority = 0; }
        _cameraIndex = 0;
        _virtualCamera[_cameraIndex].Priority = 1;

        //カメラの遷移処理

        //while (true)
        //{
            

        //    await UniTask.WaitForFixedUpdate();
        //} 
    }

    private void Update()
    {
        if (GetCurrentCameraIndex() == 0)
        {
            if (_input.currentActionMap["Commit"].triggered)//Bボタンにする　※
            {
                NextCamera();
                _canUpdate = true;
            }
        }
        else if (GetCurrentCameraIndex() == 1)
        {
            if (_input.currentActionMap["Cancel"].triggered)//Aボタンにする　※
            {
                NextCamera();
                _canUpdate = false;
            }
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