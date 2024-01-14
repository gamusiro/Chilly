using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class TitleCameraPhaseManager : LastCameraPhaseManager
{
    private bool _canUpdate;

    [SerializeField, CustomLabel("����")]
    PlayerInput _input;

    private async void Start()
    {
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        //�J�����̗D��x�����Z�b�g����
        foreach (var virtualCamera in _virtualCamera) { virtualCamera.Priority = 0; }
        _cameraIndex = 0;
        _virtualCamera[_cameraIndex].Priority = 1;

        //�J�����̑J�ڏ���

        //while (true)
        //{
            

        //    await UniTask.WaitForFixedUpdate();
        //} 
    }

    private void Update()
    {
        if (GetCurrentCameraIndex() == 0)
        {
            if (_input.currentActionMap["Commit"].triggered)//B�{�^���ɂ���@��
            {
                NextCamera();
                _canUpdate = true;
            }
        }
        else if (GetCurrentCameraIndex() == 1)
        {
            if (_input.currentActionMap["Cancel"].triggered)//A�{�^���ɂ���@��
            {
                NextCamera();
                _canUpdate = false;
            }
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