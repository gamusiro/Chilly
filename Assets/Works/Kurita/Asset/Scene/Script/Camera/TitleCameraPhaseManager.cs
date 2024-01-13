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

        //�J�����̗D��x�����Z�b�g����
        foreach (var virtualCamera in _virtualCamera) { virtualCamera.Priority = 0; }
        _cameraIndex = 0;
        _virtualCamera[_cameraIndex].Priority = 1;

        //�J�����̑J�ڏ���

        while (true)
        {
            if (GetCurrentCameraIndex() == 0)
            {
                if (Input.GetKeyDown(KeyCode.K))//B�{�^���ɂ���@��
                {
                    NextCamera();
                    _canUpdate = true;
                }
            }
            else if (GetCurrentCameraIndex() == 1)
            {
                if (Input.GetKeyDown(KeyCode.K))//A�{�^���ɂ���@��
                {
                    NextCamera();
                    _canUpdate = false;
                }
            }

            await UniTask.WaitForFixedUpdate();
        } 
    }

    //�J������؂�ւ���
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